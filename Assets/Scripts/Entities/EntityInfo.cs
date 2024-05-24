using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityTag { Herbavore, Predator};

public class EntityInfo : MonoBehaviour {

    public Entity ent;
    public string sName;

    public HashSet<EntityTag> setTags;

    public int nMaxEnergy;
    public int nCurEnergy;

    public int nCurTurnBeforeResting;
    public int nMaxTurnsBeforeResting;

    public int nSightRange;

    public bool bAlive;

    public bool CanPayEnergy(int nEnergyCost) {
        return nCurEnergy >= nEnergyCost;
    }

    public void PayEnergy(int nEnergyCost) {
        if (CanPayEnergy(nEnergyCost) == false) {
            Debug.LogErrorFormat("Can't pay {0} energy since {1} only has {2}", nEnergyCost, sName, nCurEnergy);
            return;
        }
        nCurEnergy -= nEnergyCost;
    }

    public void ReplenishEnergy() {
        nCurEnergy = nMaxEnergy;
    }

    public void Init() {
        ReplenishEnergy();
        bAlive = true;
    }
}
