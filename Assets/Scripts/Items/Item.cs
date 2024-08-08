using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Wheat,
    CopperOre, IronOre, GoldOre,
    Bearskin,

    WolfsClaws, WolfPelt,

    Shortsword,

    Platemail,

    HealingPotion,

    LENGTH
}

public abstract class Item {

    public string sName;
    public ItemType itemtype;
    public SubValue<int> nCount;
    public int nBaseValue;

    public Item(int _nCount){
        nCount = new SubValue<int>(_nCount);
        }

    public virtual int GetValue() {
        return nBaseValue;
    }

    public override string ToString() {
        return string.Format("{0} ({1})", sName, nCount.Get());
    }
}
