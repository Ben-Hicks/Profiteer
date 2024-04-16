using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCopperOre : Item {

    private ItemCopperOre() { }

    public static Item Create() {
        return new ItemCopperOre() {
            sName = "Copper Ore",
            itemtype = ItemType.CopperOre,
            nBaseValue = 10
        };
    }
}

public class ItemIronOre : Item {

    private ItemIronOre() { }

    public static Item Create() {
        return new ItemIronOre() {
            sName = "Iron Ore",
            itemtype = ItemType.IronOre,
            nBaseValue = 15
        };
    }
}

public class ItemGoldOre : Item {

    private ItemGoldOre() { }

    public static Item Create() {
        return new ItemGoldOre() {
            sName = "Gold Ore",
            itemtype = ItemType.GoldOre,
            nBaseValue = 50
        };
    }
}
