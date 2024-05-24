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
    // We'll return a path to the first tile we can find that is within nMaxDistFromEnd from tileEnd
    public static (PathResultType, List<TileTerrain>, int) FindPath(TileTerrain tileStart, TileTerrain tileEnd, float fMaxCost, int nMaxDistFromEnd = 0) {

        //Do some quick checks to see if we can reach the tileEnd at all
        bool bUnreachable = false;
        if(nMaxDistFromEnd == 0) {
            bUnreachable = (tileEnd.tileinfo.IsPassable() == false || tileEnd.ent != null);
            
        }else if(nMaxDistFromEnd == 1) {
            bUnreachable = Map.Get().FoldHex1(tileEnd, true, (TileTerrain t, bool rec) => {
            return rec && (t.tileinfo.IsPassable() == false || t.ent != null);
            });
        } else if (nMaxDistFromEnd == 2) {
            bUnreachable = Map.Get().FoldHex2(tileEnd, true, (TileTerrain t, bool rec) => {
                return rec && (t.tileinfo.IsPassable() == false || t.ent != null);
            });
        } else {
            Debug.LogErrorFormat("Currently no implementation for Unreachability of nMaxDistFromEnd = {0}", nMaxDistFromEnd);
        }

        if (bUnreachable) {
            return (PathResultType.Unreachable, null, 0);
        }
        

        HashSet<TileTerrain> setVisited = new HashSet<TileTerrain>();

        Dictionary<TileTerrain, float> dictBestTileScores = new Dictionary<TileTerrain, float>();
        Heap<PathDetails> heapToVisit = new Heap<PathDetails>();

        float fInitialh = TileTerrain.Dist(tileStart, tileEnd) * TileInfo.NMINMOVEMENTCOST;

        PathDetails detailsStart = new PathDetails {
            f = fInitialh,
            g = 0f,
            h = fInitialh,
            tile = tileStart,
            parent = null
        };

        heapToVisit.Add((0f, detailsStart));
        dictBestTileScores.Add(tileStart, fInitialh);

        PathDetails detailsBestFound = detailsStart;
        PathResultType resulttype = PathResultType.Closest;
        string sPath = "";

        while (heapToVisit.IsEmpty() == false) {

            (float, PathDetails) detailsToExplore = heapToVisit.PopMin();

            Debug.LogFormat("Inspecting {0}", detailsToExplore.Item2);
            
            TileTerrain tileExploring = detailsToExplore.Item2.tile;

            if (detailsToExplore.Item2.g > fMaxCost) {
                //If the closest we could get to the goal costs g and that's more than our maximum cost, 
                //  then we should just return the best path we previously found.
                sPath = "Closest Path: ";
                Debug.LogFormat("This path we just popped has .g={0}, but our max cost is {1}.  Falling back to our best path with cost {2}",
                    detailsToExplore.Item2.g, fMaxCost, detailsBestFound.g);
                break;
            }else if (TileTerrain.Dist(tileExploring, tileEnd) <= nMaxDistFromEnd) {
                //Then we've reached close enough to our target
                resulttype = PathResultType.Success;
                detailsBestFound = detailsToExplore.Item2;
                sPath = "Path: ";
                break;

            }

            Debug.LogFormat("Comparing ToExplore.Item2.h of {0} vs detailsBestFound.h of {1}", detailsToExplore.Item2.h, detailsBestFound.h);

            if (detailsToExplore.Item2.h < detailsBestFound.h) {
                Debug.LogFormat("Updating best found to have g={0}, h={1}, f={2}", detailsToExplore.Item2.g, detailsToExplore.Item2.h, detailsToExplore.Item2.f);
                detailsBestFound = detailsToExplore.Item2;
            }

            //Debug.LogFormat("Exploring {0}", detailsToExplore);

            if(detailsToExplore.Item2.f > dictBestTileScores[tileExploring]) {
                //Don't need to explore this since we've already explored it through faster paths
                //Debug.LogFormat("Skipping {0}", detailsToExplore);
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


        Stack<TileTerrain> stackPath = new Stack<TileTerrain>();
        while (detailsBestFound.parent != null) {
            stackPath.Push(detailsBestFound.tile);
            detailsBestFound = detailsBestFound.parent;
        }
        
        List<TileTerrain> lstPath = new List<TileTerrain>();
        int nEnergyCost = 0;
        lstPath.Add(tileStart);
        while (stackPath.Count > 0) {
            TileTerrain tileNext = stackPath.Pop();
            lstPath.Add(tileNext);
            nEnergyCost += tileNext.tileinfo.GetMovementCost();
            sPath += string.Format(" {0} ", tileNext);
        }

        Debug.LogFormat("Found {0}", sPath);
        return (resulttype, lstPath, nEnergyCost);

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
