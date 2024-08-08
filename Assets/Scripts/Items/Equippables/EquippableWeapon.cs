using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquippableWeapon : Equippable {

    public Ability abil1;
    public Ability abil2;

    public EquippableWeapon(int _nCount) : base(_nCount) {
        InitAbilities();
    }

    public abstract void InitAbilities();

    public override void InitEquipmentType() {
        equipmenttype = EquipmentType.Weapon;
    }

    

}
