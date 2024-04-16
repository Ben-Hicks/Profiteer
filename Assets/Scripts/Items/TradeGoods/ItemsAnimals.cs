using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBearSkin : Item {

    private ItemBearSkin() {}

    public static Item Create() {
        return new ItemBearSkin() {
            sName = "Bear Skin",
            itemtype = ItemType.Bearskin,
            nBaseValue = 10
        };
    }
}

