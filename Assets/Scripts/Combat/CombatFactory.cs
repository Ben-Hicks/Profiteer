using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFactory : Singleton<CombatFactory> {

    public GameObject pfCombat;

    public Combat curCombat;

    public void CreateCombat(List<Entity> lstPlayerEntities, List<Entity> lstEnemyEntities) {
        if(curCombat != null) {
            Debug.LogFormat("Can't create a new combat, since we're already in the middle of a combat");
            return;
        }

        GameObject goNewCombat = Instantiate(pfCombat, this.transform);
        curCombat = goNewCombat.GetComponent<Combat>();

        curCombat.InitCombat(lstPlayerEntities, lstEnemyEntities);

    }


    public override void Init() {
        
    }
}
