using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityInput : MonoBehaviour {

    public Entity ent;
    public EntityInfo entinfo;
    

    public EntityInput(Entity _ent) {
        ent = _ent;
    }



    public void OnAfterExecute(ActionEntity actExecuted) {
        //We finished executing an action, but we need to check if we actually completed
        // the action or if we ran out of energy while moving to perform the action;

        if (actExecuted.GetResult() == ActionEntity.ActionResult.ActionCompleted) {
            //If we successfully completed the action
            OnCompletedAction();
        } else {
            //If we partially completed our action, but didn't have enough energy to fully complete it
            OnDepletedEnergy();
        }
    }

    public abstract void InitEntityInput();
    public abstract void OnBeginTurn();
    public abstract void RequestEntityAction();
    
    public abstract void OnCompletedAction();
    public abstract void OnDepletedEnergy();
    public abstract void OnEndTurn();


}
