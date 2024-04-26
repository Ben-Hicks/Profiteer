using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pathing {

    private class PathDetails {
        public float f, g, h;
        public TileTerrain tile;
        public PathDetails parent;

        public override string ToString() {
            return string.Format("f: {0}, g: {1}, h: {2}, tile: {3}, parent: {4}", f, g, h, tile, 
                parent == null ? null : parent.tile);
        }
    }

    public static List<TileTerrain> FindPath(TileTerrain tileStart, TileTerrain tileEnd) {

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

        PathDetails detailsEnd = null;

        while(heapToVisit.IsEmpty() == false) {

            (float, PathDetails) detailsToExplore = heapToVisit.PopMin();

            Debug.LogFormat("Exploring {0}", detailsToExplore);

            TileTerrain tileExploring = detailsToExplore.Item2.tile;

            if(detailsToExplore.Item2.f > dictBestTileScores[tileExploring]) {
                //Don't need to explore this since we've already explored it through faster paths
                Debug.LogFormat("Skipping {0}", detailsToExplore);
                continue;
            }

            if(tileExploring == tileEnd) {
                detailsEnd = detailsToExplore.Item2;
                break;
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

        if(detailsEnd != null) {
            //Then we found a path, so let's back track through our PathDetails to find it

            Stack<TileTerrain> stackPath = new Stack<TileTerrain>();
            while(detailsEnd.parent != null) {
                stackPath.Push(detailsEnd.tile);
                detailsEnd = detailsEnd.parent;
            }

            string sPath = "Path: ";
            List<TileTerrain> lstPath = new List<TileTerrain>();
            while(stackPath.Count > 0) {
                TileTerrain tileNext = stackPath.Pop();
                lstPath.Add(tileNext);
                sPath += string.Format(" {0} ", tileNext);
            }

            Debug.LogFormat("Found path of {0}", sPath);
            return lstPath;

        } else {
            //No path found
            Debug.LogFormat("No path from {0} to {1}", tileStart, tileEnd);
            return null;
        }
    }


}
