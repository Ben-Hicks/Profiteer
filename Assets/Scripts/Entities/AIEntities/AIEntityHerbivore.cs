using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntityHerbivore : AIEntity {


    public AIEntityHerbivore(Entity _ent) : base(_ent) {
        
    }

    public override void OnBeginTurn() {
        base.OnBeginTurn();
        
    }

    public override void UpdateDesires() {
        //We'll just pick a random nearby tile to wander toward

        tileInterested = LibAI.GetRandomWanderTile(ent);

        Debug.LogFormat("We're interested in wandering to a random nearby tile = {0}", tileInterested);
    }

    public override ActionEntity DecideNextAction() {
        if (ent.tile == tileHome && entinfo.nCurTurnBeforeResting < entinfo.nMaxTurnsBeforeResting) {
            Debug.LogFormat("We're at our home and resting and currently have {0}/{1} active turns", entinfo.nCurTurnBeforeResting, entinfo.nMaxTurnsBeforeResting);
            return new ActionEntitySleep(ent, tileHome);
        }
        
        if (entinfo.nCurTurnBeforeResting < 0) {
            Debug.LogFormat("We have {0}/{1} turns that we can use before resting, so we need to rest", entinfo.nCurTurnBeforeResting, entinfo.nMaxTurnsBeforeResting);
            return new ActionEntitySleep(ent, tileHome);
        }

        Debug.LogFormat("We have {0} energy, so we'll progress toward doing our action", ent.entinfo.nCurEnergy);
        return new ActionEntityMove(ent, tileInterested);

    }
}
