using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableHealingPotion : Consumable {

    public ConsumableHealingPotion(int _nCount) : base(_nCount) {
        sName = "Healing Potion";
        itemtype = ItemType.HealingPotion;
        nBaseValue = 25;
    }

    public override void OnConsume() {
        Debug.LogFormat("Consumed a healing potion");
    }
}
