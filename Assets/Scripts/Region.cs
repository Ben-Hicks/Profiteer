using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region {
    public string sName;

    public TileTerrain tileOrigin;
    public BiomeType biometype;

    public List<TileTerrain> lstTiles;
    public HashSet<Region> hashsetRegionAdj;

    public void LinkAdjRegion(Region regionAdj) {
        hashsetRegionAdj.Add(regionAdj);
        regionAdj.hashsetRegionAdj.Add(this);
    }

    public  Region(TileTerrain _tileOrigin) {
        tileOrigin = _tileOrigin;
        biometype = tileOrigin.tileinfo.biometype;
        lstTiles = new List<TileTerrain>();
        hashsetRegionAdj = new HashSet<Region>();

        Queue<TileTerrain> queueTilesToExplore = new Queue<TileTerrain>();
        queueTilesToExplore.Enqueue(tileOrigin);

        while(queueTilesToExplore.Count > 0) {
            TileTerrain tileToExplore = queueTilesToExplore.Dequeue();

            if (tileToExplore.region == null) {
                if (tileToExplore.tileinfo.biometype == biometype) {
                    //Then we have an unclaimed tile with the same type as us - let's claim it

                    tileToExplore.region = this;
                    lstTiles.Add(tileToExplore);

                    Map.Get().FoldHex1<int>(tileToExplore, 0, (TileTerrain tileAdj, int nBase) => {
                        if (tileAdj == null) return 0;
                        queueTilesToExplore.Enqueue(tileAdj);
                        return 0;
                    });
                } else {
                    //Then this is an unclaimed tile belonging to another region
                    //We'll leave it along for now - once it's claimed, it can link itself to our region
                }
            } else {
                //Then we have a claimed tile
                if(tileToExplore.region != null && tileToExplore.region != this) {
                    //If it belongs to another region, then we can mark these regions as adjacent
                    this.LinkAdjRegion(tileToExplore.region);
                }
            }

        }
    }

}
