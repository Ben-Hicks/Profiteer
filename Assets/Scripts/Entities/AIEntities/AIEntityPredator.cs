using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntityPredator : AIEntity {

    public TileTerrain tilePreyChasing;

    public AIEntityPredator(Entity _ent) : base(_ent) {

    }

    public override void OnBeginTurn() {
        base.OnBeginTurn();

    }

    public override void UpdateDesires() {
        //We'll first check if we have any herbivores in our range that we can hunt
        TileTerrain tileNearestHerbivore = LibAI.FindClosestTaggedEntity(ent, new List<string>() { "Herbavore" });

        if(tileNearestHerbivore != null) {
            tilePreyChasing = tileNearestHerbivore;
            Debug.LogFormat("Found a herbivore on {0}", tilePreyChasing);
        } else {
            tileInterested = LibAI.GetRandomWanderTile(ent);
            Debug.LogFormat("No Herbivore in range - We've decided to roam to a random nearby tile = {0}", tileInterested);
        }

        
    }

    public override ActionEntity DecideNextAction() {

        if(ent.tile == tileHome && entinfo.nCurTurnBeforeResting < entinfo.nMaxTurnsBeforeResting) {
            Debug.LogFormat("We're at our home and resting and currently have {0}/{1} active turns", entinfo.nCurTurnBeforeResting, entinfo.nMaxTurnsBeforeResting);
            return new ActionEntitySleep(ent, tileHome);
        }

        if(tilePreyChasing != null) {
            Debug.LogFormat("Want to attack the herbivore we found on {0}", tilePreyChasing);
            return new ActionEntityAttack(ent, tilePreyChasing);
        }

        if (entinfo.nCurTurnBeforeResting < 0) {
            Debug.LogFormat("We have {0}/{1} turns that we can use before resting, so we need to rest", entinfo.nCurTurnBeforeResting, entinfo.nMaxTurnsBeforeResting);
            return new ActionEntitySleep(ent, tileHome);
        }

        Debug.LogFormat("We have {0} energy, so we'll progress toward doing our action", ent.entinfo.nCurEnergy);
        return new ActionEntityMove(ent, tileInterested);
       
    }
}
