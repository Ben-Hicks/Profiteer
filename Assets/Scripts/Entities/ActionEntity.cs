using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionEntity {

    public enum ActionResult { NotExecuted, ActionCompleted, MovedInRange, MovedNotInRange };
    public ActionResult result;

    public TileTerrain tileTarget;
    public Entity ent;

    public int nRange;
    public int nEnergyCost;

    public ActionEntity(Entity _ent, TileTerrain _tileTarget, int _nEnergyCost, int _nRange = 0) {
        ent = _ent;
        tileTarget = _tileTarget;
        nEnergyCost = _nEnergyCost;
        nRange = _nRange;

        result = ActionResult.NotExecuted;
    }

    public abstract string GetPrintableString();
    public abstract IEnumerator ActionEffect();

    public void SetResult(ActionResult _result) {
        result = _result;
    }

    public ActionResult GetResult() {
        return result;
    }

    public override string ToString() {
        return GetPrintableString();
    }

    public IEnumerator Execute() {
        Debug.LogFormat("Executing an ActionEntity with ent={0}, tileTarget={1}, nRange={2}", ent, tileTarget, nRange);

        if (TileTerrain.Dist(ent.tile, tileTarget) > nRange) {
            //Then we need to move closer to our target

            yield return MovementController.Get().MoveEntToTile(ent, tileTarget, nRange);

            Debug.LogFormat("Finished moving toward our target - we're now at {0}", ent.tile);
        }

        //After moving, we should check if we got close enough, and if we have enough energy to perform our effect
        if (TileTerrain.Dist(ent.tile, tileTarget) > nRange) {
            Debug.LogFormat("We moved as much as we could, but are still {0} away", TileTerrain.Dist(ent.tile, tileTarget));
            SetResult(ActionResult.MovedNotInRange);
            yield break;
        } else if (ent.entinfo.CanPayEnergy(nEnergyCost) == false) {
            Debug.LogFormat("We moved close enough, but we only have {0} energy, but this action costs {1}", ent.entinfo.nCurEnergy, nEnergyCost);
            SetResult(ActionResult.MovedInRange);
            yield break;
        }


        //Then we can perform our effect
        //So pay for the action, then execute it
        ent.entinfo.PayEnergy(nEnergyCost);
        Debug.LogFormat("{0} paid {1} energy to do their action, so now they're at {2}", ent, nEnergyCost, ent.entinfo.nCurEnergy);
        yield return ActionEffect();

        Debug.Log("Finished doing our ActionEffect");
        SetResult(ActionResult.ActionCompleted);
    }
}
