using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquippableArmour : Equippable{

    public Ability abil;

    public EquippableArmour(int _nCount) : base(_nCount) {
        InitAbility();
    }

    public abstract void InitAbility();

    public override void InitEquipmentType() {
        equipmenttype = EquipmentType.Armour;
    }
}
