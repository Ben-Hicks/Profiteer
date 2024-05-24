using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntityWanderer : AIEntity {

    public AIEntityWanderer(Entity _ent) : base(_ent) {

    }
    
    public override void UpdateDesires() {
        //We'll just pick a random nearby tile to wander toward

        tileInterested = LibAI.GetRandomWanderTile(ent);

        Debug.LogFormat("We've decided to roam to a random nearby tile = {0}", tileInterested);
    }

    public override ActionEntity DecideNextAction() {

        Debug.LogFormat("We'll progress toward wandering", ent.entinfo.nCurEnergy);
        return new ActionEntityMove(ent, tileInterested);
     
    }
}
