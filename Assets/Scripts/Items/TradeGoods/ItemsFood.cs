using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWheat : Item {

    private ItemWheat() {}

    public static Item Create() {
        return new ItemWheat() {
            sName = "Wheat",
            itemtype = ItemType.Wheat,
            nBaseValue = 2
        };
    }
}

