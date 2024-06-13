using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualInput : EntityInput {


    public ManualInput(Entity _ent) : base(_ent) {

    }

    

    public override void InitEntityInput() {
        Debug.LogFormat("Init Manual Input");
    }

    public override void OnBeginTurn() {
        Debug.LogFormat("Beginning Manual Turn");
    }

    public override void RequestEntityAction() {
        //Send out any signals to any input methods that may be equipped to fulfil Action-requests
        TurnController.Get().subOpenManualInput.NotifyObs(this);
    }

    //If we're specifically moved to and completed an action, then we can react to it
    public override void OnCompletedAction() {
        Debug.LogFormat("Finished Manual Action");
    }

    public override void OnDepletedEnergy() {
        Debug.LogFormat("Depleted Manual Energy");
    }

    public override void OnEndTurn() {
        //Send out a signal that we're done our turn, so we can stop accepting input commands
        TurnController.Get().subCloseManualInput.NotifyObs(this);
    }
}
