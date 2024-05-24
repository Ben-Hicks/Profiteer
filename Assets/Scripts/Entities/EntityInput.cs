using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityInput : MonoBehaviour {

    public Entity ent;
    public EntityInfo entinfo;
    

    public EntityInput(Entity _ent) {
        ent = _ent;
    }

    public abstract void InitEntityInput();
    public abstract void OnBeginTurn();
    public abstract void RequestEntityAction();
    public abstract void OnAfterExecute(ActionEntity actExecuted);
    public abstract void OnCompletedAction();
    public abstract void OnEndTurn();


}
