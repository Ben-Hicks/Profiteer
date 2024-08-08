using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatterManual : Combatter {

    public override void SetTeam() {
        team = Team.Player;
    }

    public override IEnumerator TakeTurn() {

        combat.abilitiespanel.SetCombatterShowing(this);

        while (true) {

            while (combat.abilChosen == null) {
                Debug.Log("Waiting to choose an ability");
                yield return new WaitForSeconds(combat.fDelaySpinOnChooseAbility);

            }


            while (combat.abilChosen != null && combat.combatterTarget == null) {
                Debug.LogFormat("Waiting to choose a target for {0}", combat.abilChosen);
                yield return new WaitForSeconds(combat.fDelaySpinOnChooseAbility);
                
            }

            //Do one final check to make sure our selections can be legally carried out
            if(combat.HasLegalSelectionsStored() == false) {
                //Then we need to reset and try again
                Debug.LogFormat("{0} using {1} to target {2} is not a legal selection", combat.combatterCurActing, combat.abilChosen, combat.combatterTarget);
                combat.abilChosen = null;
                combat.combatterTarget = null;
                continue;
            } else {
                Debug.LogFormat("{0} using {1} to target {2} is legal", combat.combatterCurActing, combat.abilChosen, combat.combatterTarget);
                yield return combat.ExecuteStoredSelection();
                break;
            }

        }


        combat.abilitiespanel.SetCombatterShowing(null);

    }

}
