using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCopperOre : Item {

    public ItemCopperOre(int _nCount) : base(_nCount) {
        sName = "Copper Ore";
        itemtype = ItemType.CopperOre;
        nBaseValue = 10;
    }

}

public class ItemIronOre : Item {

    public ItemIronOre(int _nCount) : base(_nCount) {
        sName = "Iron Ore";
        itemtype = ItemType.IronOre;
        nBaseValue = 15;
    }

}

public class ItemGoldOre : Item {

    public ItemGoldOre(int _nCount) : base(_nCount) {
        sName = "Gold Ore";
        itemtype = ItemType.GoldOre;
        nBaseValue = 50;
    }

}
