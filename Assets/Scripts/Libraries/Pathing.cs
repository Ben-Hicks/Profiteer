using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pathing {

    public enum PathResultType {
        Success, 
        Unreachable,
        Closest
    }

    private class PathDetails {
        public float f, g, h;
        public TileTerrain tile;
        public PathDetails parent;

        public override string ToString() {
            return string.Format("f: {0}, g: {1}, h: {2}, tile: {3}, parent: {4}", f, g, h, tile, 
                parent == null ? null : parent.tile);
        }
    }

    //Will return an exact path if one exists with cost <= fMaxCost, otherwise we'll return a path to the closest tile we found
    // to the destination within that fMaxCost
    public static (PathResultType, List<TileTerrain>) FindPath(TileTerrain tileStart, TileTerrain tileEnd, float fMaxCost) {

        if (tileEnd.tileinfo.IsPassable() == false || tileEnd.ent != null) {
            return (PathResultType.Unreachable, null);
        }

        HashSet<TileTerrain> setVisited = new HashSet<TileTerrain>();

        Dictionary<TileTerrain, float> dictBestTileScores = new Dictionary<TileTerrain, float>();
        Heap<PathDetails> heapToVisit = new Heap<PathDetails>();

        PathDetails detailsStart = new PathDetails {
            f = 0f,
            g = 0f,
            h = 0f,
            tile = tileStart,
            parent = null
        };

        heapToVisit.Add((0f, detailsStart));
        dictBestTileScores.Add(tileStart, 0f);

        PathDetails detailsBestFound = detailsStart;

        while(heapToVisit.IsEmpty() == false) {

            (float, PathDetails) detailsToExplore = heapToVisit.PopMin();
            
            TileTerrain tileExploring = detailsToExplore.Item2.tile;

            if (tileExploring == tileEnd || detailsToExplore.Item2.g > fMaxCost) {
                detailsBestFound = detailsToExplore.Item2;
                break;
            }

            if (detailsToExplore.Item2.g < detailsBestFound.g) detailsBestFound = detailsToExplore.Item2;

            Debug.LogFormat("Exploring {0}", detailsToExplore);

            if(detailsToExplore.Item2.f > dictBestTileScores[tileExploring]) {
                //Don't need to explore this since we've already explored it through faster paths
                Debug.LogFormat("Skipping {0}", detailsToExplore);
                continue;
            }


            setVisited.Add(tileExploring);

            Map.Get().FoldHex1(tileExploring, 0, (TileTerrain tile, float rec) => {
                if(tile != tileExploring) {
                    //If we haven't yet visited this tile and it can be passed through
                    if(setVisited.Contains(tile) == false && tile.tileinfo.IsPassable()) {

                        //Calculate our scores to reach this tile
                        float g = detailsToExplore.Item2.g + tile.tileinfo.GetMovementCost();
                        float h = TileTerrain.Dist(tile, tileEnd) * TileInfo.NMINMOVEMENTCOST;
                        float f = g + h;

                        if(dictBestTileScores.ContainsKey(tile) == false || f < dictBestTileScores[tile]) {
                            //Then we've found a best path to this tile
                            PathDetails detailsNewNeighbour = new PathDetails {
                                f = f,
                                g = g,
                                h = h,
                                tile = tile,
                                parent = detailsToExplore.Item2
                            };
                            heapToVisit.Add((f, detailsNewNeighbour));
                            dictBestTileScores[tile] = f;
                        }
                    }
                }
                return rec;
            });
        }

        string sPath;
        PathResultType resulttype = PathResultType.Success;

        if(detailsBestFound.tile != tileEnd) {
            Debug.LogFormat("Couldn't find a path to {0}, so returning a closest path to {1} instead", tileEnd, detailsBestFound.tile);
            sPath = "Closest Path: ";
            resulttype = PathResultType.Closest;
        } else {
            sPath = "Path: ";
        }

        Stack<TileTerrain> stackPath = new Stack<TileTerrain>();
        while (detailsBestFound.parent != null) {
            stackPath.Push(detailsBestFound.tile);
            detailsBestFound = detailsBestFound.parent;
        }
        
        List<TileTerrain> lstPath = new List<TileTerrain>();
        lstPath.Add(tileStart);
        while (stackPath.Count > 0) {
            TileTerrain tileNext = stackPath.Pop();
            lstPath.Add(tileNext);
            sPath += string.Format(" {0} ", tileNext);
        }

        Debug.LogFormat("Found {0}", sPath);
        return (resulttype, lstPath);

    }


    public static HashSet<TileTerrain> GetTilesInMovementRange(TileTerrain tileStart, int nMovementBudget) {
        HashSet<TileTerrain> setTilesReachable = new HashSet<TileTerrain>();

        Queue<(int, TileTerrain)> queueToExplore = new Queue<(int, TileTerrain)>();
        queueToExplore.Enqueue((nMovementBudget, tileStart));

        while(queueToExplore.Count > 0) {
            (int, TileTerrain) tileToExplore = queueToExplore.Dequeue();
            if (setTilesReachable.Contains(tileToExplore.Item2) || tileToExplore.Item1 < 0) continue;
            
            setTilesReachable.Add(tileToExplore.Item2);
            Map.Get().FoldHex1(tileToExplore.Item2, 0, (TileTerrain t, int rec) => {
                if(setTilesReachable.Contains(t) == false && t.tileinfo.IsPassable()) {
                    queueToExplore.Enqueue((tileToExplore.Item1 - t.tileinfo.GetMovementCost(), t));
                }
                return rec;
            });
        }

        return setTilesReachable;
    }

}
