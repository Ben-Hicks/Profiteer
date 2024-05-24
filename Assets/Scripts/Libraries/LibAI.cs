using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LibAI {

    public static readonly int NWANDERRANGE = 4;
    
   

    //Find the closest entity within sight that has all the required tags
    public static TileTerrain FindClosestTaggedEntity(Entity ent, List<EntityTag> lstTagsRequired) {

        List<TileTerrain> lstCandidates = new List<TileTerrain>();

        //For now, we're just looping over all Entities - this could be further refined if we eventually have too many entities to make this practical
        foreach(Entity e in Map.Get().lstEntities) {
            bool bHasAllTags = true;
            foreach(EntityTag t in lstTagsRequired) {
                if (e.entinfo.setTags.Contains(t) == false) {
                    bHasAllTags = false;
                    break;
                }
            }

            if (bHasAllTags) {
                lstCandidates.Add(e.tile);
            }
        }

        if(lstCandidates.Count == 0) {
            return null;
        }

        TileTerrain tileClosest = TileTerrain.ClosestToTile(ent.tile, lstCandidates);

        if(tileClosest != null && TileTerrain.Dist(ent.tile, tileClosest) <= ent.entinfo.nSightRange) {
            return tileClosest;
        } else {
            return null;
        }
    }

    public static TileTerrain GetRandomWanderTile(Entity ent) {
        int nRandomWanderX = Random.Range(-NWANDERRANGE, NWANDERRANGE);
        int nRandomWanderY = Random.Range(-NWANDERRANGE, NWANDERRANGE);

        if (nRandomWanderX == 0 && nRandomWanderY == 0) return GetRandomWanderTile(ent);

        TileTerrain tileRandom = Map.Get().GetTile(ent.tile.x + nRandomWanderX, ent.tile.y + nRandomWanderY);

        if (tileRandom == null) return GetRandomWanderTile(ent);
        else return tileRandom;
    }

}
