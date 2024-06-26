using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInfo : MonoBehaviour {

    public Entity ent;
    public string sName;

    //Level stats
    public SubValue<int> nLevel;
    public SubValue<int> nCurXP;
    public SubValue<int> nMaxXP;

    //Combat stats
    public SubValue<int> nCurHP;
    public SubValue<int> nMaxHP;

    //Energy stats
    public SubValue<int> nMaxEnergy;
    public SubValue<int> nCurEnergy;

    //Core stats
    public SubValue<int> nPerception;
    public SubValue<int> nSwiftness;
    public SubValue<int> nCunning;
    public SubValue<int> nBartering;
    public SubValue<int> nCharisma;
    public SubValue<int> nTradesmanship;

    //Should these be moved into an AI extension of EntityInfo?
    public int nMaxTurnsBeforeResting;
    public int nCurTurnBeforeResting;

    //Perception Stats
    public SubValue<int> nSightRange;
    public SubValue<int> nInvestigation;



    public bool bAlive;

    public DictTags dictTags;

    public void SetCombatStats(int _nMaxHP) {
        nMaxHP = new SubValue<int>(_nMaxHP);
        nCurHP = new SubValue<int>(_nMaxHP);
    }

    public void SetLevelStats(int _nLevel, int _nMaxXP) {
        nLevel = new SubValue<int>(_nLevel);
        nMaxXP = new SubValue<int>(_nMaxXP);
        nCurXP = new SubValue<int>(0);
    }

    public void SetEnergyStats(int _nMaxEnergy) {
        nMaxEnergy = new SubValue<int>(_nMaxEnergy);
        nCurEnergy = new SubValue<int>(_nMaxEnergy);
    }

    public void SetCoreStats(int _nPerception, int _nSwiftness, int _nCunning, int _nBartering, 
        int _nCharisma, int _nTradesmanship) {
        nPerception = new SubValue<int>(_nPerception);
        nSwiftness = new SubValue<int>(_nSwiftness);
        nCunning = new SubValue<int>(_nCunning);
        nBartering = new SubValue<int>(_nBartering);
        nCharisma = new SubValue<int>(_nCharisma);
        nTradesmanship = new SubValue<int>(_nTradesmanship);
    }

    public void SetPerceptionStats(int nSightRangeBonus, int nInvestigationBonus) {
        //Can have some base scalings depending on your Perception that we can add each bonus to

        nSightRange = new SubValue<int>(nSightRangeBonus);
        nInvestigation = new SubValue<int>(nInvestigationBonus);
    }

    public bool CanPayEnergy(int nEnergyCost) {
        return nCurEnergy.Get() >= nEnergyCost;
    }

    public void PayEnergy(int nEnergyCost) {
        if (CanPayEnergy(nEnergyCost) == false) {
            Debug.LogErrorFormat("Can't pay {0} energy since {1} only has {2}", nEnergyCost, sName, nCurEnergy.Get());
            return;
        }
        nCurEnergy.Set(nCurEnergy.Get() - nEnergyCost);
    }

    public void ReplenishEnergy() {
        nCurEnergy.Set(nMaxEnergy.Get());
    }

    public void InitEntityInfo() {

        ReplenishEnergy();
        bAlive = true;
    }
}
