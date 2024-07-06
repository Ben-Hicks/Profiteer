using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBearSkin : Item {

    public ItemBearSkin(int _nCount) : base(_nCount) {
        sName = "Bear Skin";
        itemtype = ItemType.Bearskin;
        nBaseValue = 10;
    }

}

