using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equippable : Item {

    public abstract void OnEquip(Entity ent);
    public abstract void OnUnequip(Entity ent);
}
