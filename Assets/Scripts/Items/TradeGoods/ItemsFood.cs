using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWheat : Item {

    public ItemWheat(int _nCount) : base(_nCount) {
        sName = "Wheat";
        itemtype = ItemType.Wheat;
        nBaseValue = 2;
    }

}

