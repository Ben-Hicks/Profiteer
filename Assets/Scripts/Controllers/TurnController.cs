using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : Singleton<TurnController> {

    private int iCurEntityTurn;
    public Entity entCurActing {
        get { return EntityController.Get().lstAllEntities[iCurEntityTurn]; }
    }
    private ActionEntity actToExecute;
    private bool bFinishedTurn;

    public int nRound;
    private bool bNewRoundFlag;

    public Subject subOpenManualInput = new Subject();
    public Subject subCloseManualInput = new Subject();

    public Entity ProgressToNextActingEntity() {

        iCurEntityTurn++;

        while (iCurEntityTurn < EntityController.Get().lstAllEntities.Count) {

            if (entCurActing.entinfo.bAlive) return entCurActing;
            iCurEntityTurn++;
        }
        //Then we've gone through all of the list of entities so we can reset to the beginning of our list
        iCurEntityTurn = -1;
        bNewRoundFlag = true;
        return ProgressToNextActingEntity();
    }

    public void SubmitChosenAction(ActionEntity act) {
        if(act.ent != EntityController.Get().lstAllEntities[iCurEntityTurn]) {
            Debug.LogErrorFormat("Recieved an action input from {0}, but we're expecting {1} to act next", act.ent, EntityController.Get().lstAllEntities[iCurEntityTurn]);
            return;
        }

        actToExecute = act;
    }

    public void SubmitFinishTurn(Entity ent) {
        if (ent != EntityController.Get().lstAllEntities[iCurEntityTurn]) {
            Debug.LogErrorFormat("Recieved an action input from {0}, but we're expecting {1} to act next", ent, EntityController.Get().lstAllEntities[iCurEntityTurn]);
            return;
        }

        bFinishedTurn = true;
    }

    public void StartRound() {
        nRound++;
        Debug.LogFormat("Starting round {0}", nRound);
        bNewRoundFlag = false;
    }

    public IEnumerator TurnLoop() {

        while (true) {

            if (EntityController.Get().lstAllEntities == null || EntityController.Get().lstAllEntities.Count == 0) {
                Debug.Log("We have no entities, so no need do any turn loop right now");
                yield return new WaitForSeconds(5);
                continue;
            }

            ProgressToNextActingEntity();

            if(bNewRoundFlag) {
                //Then we've looped around to a new round
                StartRound();
            }

            bFinishedTurn = false;
            entCurActing.entinfo.ReplenishEnergy();
            entCurActing.entinput.OnBeginTurn();
            Debug.LogFormat("Starting turn for {0}", entCurActing);

            while (true) {

                //Send a request to the acting entity to see which action they want to perform
                actToExecute = null;
                Debug.LogFormat("Requesting action for {0}", entCurActing);
                entCurActing.entinput.RequestEntityAction();

                //spin while waiting for a response to perform from that entity
                while (bFinishedTurn == false && actToExecute == null) {
                    yield return new WaitForSeconds(0.1f);
                }

                if(actToExecute != null) {
                    //If we have an action to execute, then let's do it
                    Debug.LogFormat("About to execute action {0}", actToExecute);
                    yield return actToExecute.Execute();

                    entCurActing.entinput.OnAfterExecute(actToExecute);
                }

                //If we recieved a FinishedTurn signal (either instead of an action to execute, or as part of
                //  finishing executing the recieved action), then we can end this entity's turn
                if(bFinishedTurn == true)  break;
                

                //We can then loop again to see if we have any further actions to take
                yield return new WaitForSeconds(3f);
            }

            entCurActing.entinput.OnEndTurn();
            Debug.LogFormat("Ending turn for {0}", entCurActing);

            yield return new WaitForSeconds(5f);
        }

    }


    public override void Init() {
        iCurEntityTurn = -1;
        actToExecute = null;
        bFinishedTurn = true;
        bNewRoundFlag = true;

        StartCoroutine(TurnLoop());
    }
}
