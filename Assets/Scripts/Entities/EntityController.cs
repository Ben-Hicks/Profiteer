using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : Singleton<EntityController> {

    public Entity entPlayer;

    public void RegisterEntPlayer(Entity _entPlayer) {
        if(entPlayer != null) {
            Debug.LogError("Trying to set EntPlayer when we already have one!");
            return;
        }
        entPlayer = _entPlayer;
    }

    public override void Init() {
        
    }
}
