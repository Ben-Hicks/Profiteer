﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippableShortsword : Equippable {

    public EquippableShortsword(int _nCount) : base(_nCount) {
        sName = "Shortsword";
        itemtype = ItemType.Shortsword;
        nBaseValue = 35;
    }

    public override void OnEquip(Entity ent) {
        Debug.LogFormat("Equipped a Shortsword");
    }

    public override void OnUnequip(Entity ent) {
        Debug.LogFormat("Unequipped a Shortsword");
    }
}
