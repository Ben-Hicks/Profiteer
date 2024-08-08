using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equippable : Item {

    public Entity ent;

    public enum EquipmentType { Armour, Weapon };
    public EquipmentType equipmenttype;

    public Equippable(int _nCount) : base(_nCount) {
        InitEquipmentType();
    }

    public abstract void InitEquipmentType();

    public void Equip(Entity _ent) {
        ent = _ent;

        OnEquip();
    }

    public abstract void OnEquip();

    public void Unequip() {
        ent = null;

        OnEquip();
    }

    public abstract void OnUnequip();
}
