using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIEntity : EntityInput {

    public TileTerrain tileInterested;
    public TileTerrain tileHome;

    public AIEntity(Entity _ent) : base(_ent) {

        entinfo.nCurTurnBeforeResting = entinfo.nMaxTurnsBeforeResting;
    }

    //Decide what goals this entity wants to achieve when taking its action
    public abstract void UpdateDesires();

    public abstract ActionEntity DecideNextAction();

    public override void InitEntityInput() {
        tileHome = ent.tile;
    }

    public override void OnBeginTurn() {
        Debug.LogFormat("AI {0} beginning turn", ent);

        entinfo.nCurTurnBeforeResting--;

        UpdateDesires();
    }

    public override void OnEndTurn() {
        Debug.LogFormat("AI {0} ending turn", ent);
    }

    public override void RequestEntityAction() {

        ActionEntity actDecided = DecideNextAction();

        if (actDecided == null) {
            TurnController.Get().SubmitFinishTurn(ent);
        } else {
            actDecided.ent = ent;
            TurnController.Get().SubmitChosenAction(actDecided);
        }
    }
    
    public override void OnCompletedAction() {
        //Since we finished doing what we wanted to do, we can re-evaluate our desires
        UpdateDesires();
    }
    public override void OnDepletedEnergy() {
        //If we used up all our energy, then we can just end our turn
        TurnController.Get().SubmitFinishTurn(ent);
    }

}
