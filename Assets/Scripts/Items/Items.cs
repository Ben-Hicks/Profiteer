using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public enum ItemType {
    Wheat,
    CopperOre, IronOre, GoldOre,
    Bearskin,

    Shortsword,

    HealingPotion,

    LENGTH
}

public static class Items {

    public static Item[] arItems = new Item[(int)ItemType.LENGTH - 1];

    public delegate Item ItemConstructor();

    public static System.Func<Item>[] arItemConstructors = {
        ItemWheat.Create,

        ItemCopperOre.Create,
        ItemIronOre.Create,
        ItemGoldOre.Create,

        ItemBearSkin.Create,


        EquippableShortsword.Create,


        ConsumableHealingPotion.Create,
    };

    public static Item Get(ItemType itemtype){

        int i = (int)itemtype;
        if (arItems[i] == null) arItems[i] = arItemConstructors[i]();

        return arItems[i];
    }
}
