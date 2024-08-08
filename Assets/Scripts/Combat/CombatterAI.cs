using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatterAI : Combatter {

    public const int nMaxAttempts = 5;

    public override void SetTeam() {
        team = Team.Enemy;
    }

    public override IEnumerator TakeTurn() {

        int nAttempts = 0;

        while(nAttempts < nMaxAttempts) {
            nAttempts++;

            combat.abilChosen = combat.GetRandomAbility(combat.combatterCurActing);
            combat.combatterTarget = combat.GetRandomTarget(combat.combatterCurActing, combat.abilChosen);

            if (combat.HasLegalSelectionsStored()) {
                yield return combat.ExecuteStoredSelection();
                break;
            } else {
                combat.abilChosen = null;
                combat.combatterTarget = null;
            }
            
        }


    }

}
