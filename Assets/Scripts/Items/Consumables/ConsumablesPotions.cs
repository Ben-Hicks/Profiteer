using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableHealingPotion : Consumable {

    private ConsumableHealingPotion() { }

    public static Item Create() {
        return new ConsumableHealingPotion() {
            sName = "Healing Potion",
            itemtype = ItemType.HealingPotion,
            nBaseValue = 25
        };
    }

    public override void OnConsume() {
        Debug.LogFormat("Consumed a healing potion");
    }
}
