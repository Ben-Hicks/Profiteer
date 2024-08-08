using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LibAI {

    public static readonly int NWANDERRANGE = 4;
    
   

    //Find the closest entity within sight that has all the required tags
    public static TileTerrain FindClosestTaggedEntity(Entity ent, List<string> lstTagsRequired) {

        List<TileTerrain> lstCandidates = new List<TileTerrain>();

        //For now, we're just looping over all Entities - this could be further refined if we eventually have too many entities to make this practical
        foreach(Entity e in EntityController.Get().lstAllEntities) {

            //Skip over this entity if it's the one making the request
            if (e == ent) continue;

            bool bHasAllTags = true;
            foreach(string sTag in lstTagsRequired) {
                bool bDummy = false;
                if (e.entinfo.dictTags.FetchFeatureValue(sTag, out bDummy) == false) {
                    bHasAllTags = false;
                    break;
                }
            }

            if (bHasAllTags) {
                lstCandidates.Add(e.tile);
            }
        }

        if(lstCandidates.Count == 0) {
            Debug.Log("No candidates found");
            return null;
        }

        TileTerrain tileClosest = TileTerrain.ClosestToTile(ent.tile, lstCandidates);

        Debug.LogFormat("Closest tile we found is {0} at a dist of {1} and our sight range is {2}", tileClosest, TileTerrain.Dist(ent.tile, tileClosest), ent.entinfo.nSightRange);

        if(tileClosest != null && TileTerrain.Dist(ent.tile, tileClosest) <= ent.entinfo.nSightRange.Get()) {
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
