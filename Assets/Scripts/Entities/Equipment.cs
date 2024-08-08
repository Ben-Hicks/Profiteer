using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment {

    public Entity ent;

    public AbilityRest abilRest;
    public EquippableWeapon equippableLeft;
    public EquippableWeapon equippableRight;
    public EquippableArmour equippableArmour;

    public Equipment(Entity _ent) {
        ent = _ent;
        abilRest = new AbilityRest();
    }

    public void EquipLeft(EquippableWeapon _equippableLeft) {
        if(equippableLeft != null) {
            equippableLeft.Unequip();
        }

        equippableLeft = _equippableLeft;

        if(equippableLeft != null) {
            equippableLeft.Equip(this.ent);
        }
    }

    public void EquipRight(EquippableWeapon _equippableRight) {
        if (equippableRight != null) {
            equippableRight.Unequip();
        }

        equippableRight = _equippableRight;

        if (equippableRight != null) {
            equippableRight.Equip(this.ent);
        }
    }

    public void EquipArmour(EquippableArmour _equippableArmour) {
        if (equippableArmour != null) {
            equippableArmour.Unequip();
        }

        equippableArmour = _equippableArmour;

        if (equippableArmour != null) {
            equippableArmour.Equip(this.ent);
        }
    }
}
