using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

    public float fDelayStartRound;
    public float fDelayEndRound;
    public float fDelaySpinOnChooseAbility;
    public float fDelayExecuteAbility;

    public GameObject pfCombatter;

    public GameObject goContainerPlayer;
    public GameObject goContainerEnemy;

    public AbilitiesPanel abilitiespanel;
    
    public Combatter combatterPlayer;
    public List<Combatter> lstCombatterEnemy;

    public bool bCombatWon;
    public bool bCombatLost;


    public Combatter combatterCurActing;

    public Ability abilChosen;
    public Combatter combatterTarget;

    public void StartCombat() {
        StartCoroutine(CombatLoop());
    }


    public void EndCombat() {

        CombatFactory.Get().curCombat = null;
        GameObject.Destroy(this);

        if (bCombatLost) {
            OnCombatLost();
        } else if (bCombatWon) {
            OnCombatWon();
        } else {
            Debug.LogError("Combat shouldn't be over if we neither won nor lost");
            return;
        }

    }

    public void OnCombatWon() {
        Debug.Log("We won combat");
    }

    public void OnCombatLost() {
        Debug.Log("We lost combat");
    }

    public void StartRound() {
        Debug.Log("Starting Round");

    }

    public void EndRound() {
        Debug.Log("Ending Round");

    }

    public bool IsCombatOver() {
        if (bCombatWon || bCombatLost) return true;
        return false;
    }

    public void ClearStoredSelections() {
        combatterCurActing = null;
        abilChosen = null;
        combatterTarget = null;
    }

    public void StartWaitingForInput(Combatter _combatterCurActing) {
        ClearStoredSelections();
        combatterCurActing = _combatterCurActing;

        Debug.LogFormat("Starting to wait for input from {0}", combatterCurActing);
    }

    public Ability GetRandomAbility(Combatter combatter) {
        List<Ability> lstAbilityUniverse = new List<Ability>();

        if(combatter.ent.equip.equippableArmour != null) {
            if(combatter.ent.equip.equippableArmour.abil != null) {
                lstAbilityUniverse.Add(combatter.ent.equip.equippableArmour.abil);
            }
        }

        if (combatter.ent.equip.equippableLeft != null) {
            if (combatter.ent.equip.equippableLeft.abil1 != null) {
                lstAbilityUniverse.Add(combatter.ent.equip.equippableLeft.abil1);
            }
            if (combatter.ent.equip.equippableLeft.abil2 != null) {
                lstAbilityUniverse.Add(combatter.ent.equip.equippableLeft.abil2);
            }
        }

        if (combatter.ent.equip.equippableRight != null) {
            if (combatter.ent.equip.equippableRight.abil1 != null) {
                lstAbilityUniverse.Add(combatter.ent.equip.equippableRight.abil1);
            }
            if (combatter.ent.equip.equippableRight.abil2 != null) {
                lstAbilityUniverse.Add(combatter.ent.equip.equippableRight.abil2);
            }
        }

        lstAbilityUniverse.Add(combatter.ent.equip.abilRest);

        return lstAbilityUniverse[Random.Range(0, lstAbilityUniverse.Count)];
    }

    public Combatter GetRandomTarget(Combatter combatterSource, Ability abilChosen) {
        List<Combatter> lstTargetUniverse = new List<Combatter>();

        if (abilChosen.fnCanTarget(combatterSource, combatterPlayer)) lstTargetUniverse.Add(combatterPlayer);

        foreach(Combatter combatterEnemy in lstCombatterEnemy) {
            if (abilChosen.fnCanTarget(combatterSource, combatterEnemy)) lstTargetUniverse.Add(combatterEnemy);
        }

        if (lstTargetUniverse.Count == 0) return null;

        return lstTargetUniverse[Random.Range(0, lstTargetUniverse.Count)];
    }

    public bool HasLegalSelectionsStored() {
        if (combatterCurActing == null) {
            return false;
        }

        if (abilChosen == null) {
            return false;
        }

        if(combatterTarget == null || abilChosen.fnCanTarget(combatterCurActing, combatterTarget) == false) {
            return false;
        }

        return true;
    }

    public IEnumerator ExecuteStoredSelection() {
        abilChosen.Execute(combatterCurActing, combatterTarget);
        yield return new WaitForSeconds(fDelayExecuteAbility);
        ClearStoredSelections();
    }

    public IEnumerator CombatLoop() {

        while (true) {
            yield return new WaitForSeconds(fDelayStartRound);
            StartRound();
            if (IsCombatOver()) break;
            
            StartWaitingForInput(combatterPlayer);
            yield return combatterPlayer.TakeTurn();
            if (IsCombatOver()) break;

            for (int i = 0; i < lstCombatterEnemy.Count; i++) {

                //Need to do some check for if the character is still alive
                StartWaitingForInput(lstCombatterEnemy[i]);
                yield return lstCombatterEnemy[i].TakeTurn();
                if (IsCombatOver()) break;

            }
            if (IsCombatOver()) break;

            yield return new WaitForSeconds(fDelayEndRound);
            EndRound();
            if (IsCombatOver()) break;
        }

        EndCombat();
    }

    public void ChooseAbilityToExecute(Ability _abilChosen, Combatter _combatterCurActing) {
        if(_combatterCurActing != combatterCurActing) {
            Debug.LogErrorFormat("Received input for {0}, but we're waiting for {1} to act", _combatterCurActing, combatterCurActing);
            return;
        }

        abilChosen = _abilChosen;
    }

    public void ChooseEntityTarget(Combatter _combatterTarget) {
        if(combatterCurActing == null) {
            Debug.LogErrorFormat("Tried to click {0} to target them, but we have no acting entity", _combatterTarget);
            return;
        }

        if(abilChosen == null) {
            Debug.LogErrorFormat("Tried to click {0} to target them, but we have no selected ability yet", _combatterTarget);
            return;
        }

        if (abilChosen.fnCanTarget(combatterCurActing, _combatterTarget) == false) {
            Debug.LogErrorFormat("Tried to click {0} to target them, but {1} can't legally target them", _combatterTarget, abilChosen);
            return;
        }

        combatterTarget = _combatterTarget;
    }

    public void InitCombat(List<Entity> lstPlayerEntities, List<Entity> lstEnemyEntities) {

        foreach(Entity entPlayer in lstPlayerEntities) {
            GameObject goNewCombatter = Instantiate(pfCombatter, goContainerPlayer.transform);
            combatterPlayer = goNewCombatter.AddComponent<CombatterManual>();
            combatterPlayer.Init(this, entPlayer);
        }


        lstCombatterEnemy = new List<Combatter>();
        foreach (Entity entEnemy in lstEnemyEntities) {
            GameObject goNewCombatter = Instantiate(pfCombatter, goContainerEnemy.transform);
            Combatter combatterNewEnemy = goNewCombatter.AddComponent<CombatterAI>();
            lstCombatterEnemy.Add(combatterNewEnemy);
            combatterNewEnemy.Init(this, entEnemy);
        }

        bCombatWon = false;
        bCombatLost = false;

        StartCombat();
    }

}
