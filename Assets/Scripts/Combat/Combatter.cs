using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public abstract class Combatter : MonoBehaviour, IPointerClickHandler {
    
    public Combat combat;
    public Entity ent;

    public enum Team { Player, Enemy };
    public Team team;

    public void Init(Combat _combat, Entity _ent) {
        combat = _combat;
        ent = _ent;
        SetTeam();
    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        Debug.LogFormat("Clicked on {0}", ent);
        combat.ChooseEntityTarget(this);
    }

    public abstract void SetTeam();
    public abstract IEnumerator TakeTurn();

}
