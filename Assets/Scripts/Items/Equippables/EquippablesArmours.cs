using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmourPlatemail : EquippableArmour{

    public class AbilBrace : Ability {

        public AbilBrace() : base("Brace", "{0} is bracing {1}") {

        }

        public override void Execute(Combatter combatterSource, Combatter combatterTarget) {
            Debug.LogFormat(sBaseDescription, combatterSource, combatterTarget);
        }

        public override Targetter.FnTarget InitCanTarget() {
            return Targetter.IsSelf;
        }
    }

    public ArmourPlatemail(int _nCount) : base(_nCount) {
        sName = "Platemail";
        itemtype = ItemType.Platemail;
        nBaseValue = 55;
    }

    public override void InitAbility() {
        abil = new AbilBrace();
    }

    public override void OnEquip() {
        Debug.LogFormat("Equipped Platemail");
    }

    public override void OnUnequip() {
        Debug.LogFormat("Unequipped Platemail");
    }
}


public class ArmourWolfPelt : EquippableArmour {

    public class AbilLurk : Ability {

        public AbilLurk() : base("Lurk", "{0} is lurking") {

        }

        public override void Execute(Combatter combatterSource, Combatter combatterTarget) {
            Debug.LogFormat(sBaseDescription, combatterSource, combatterTarget);
        }

        public override Targetter.FnTarget InitCanTarget() {
            return Targetter.IsSelf;
        }
    }

    public ArmourWolfPelt(int _nCount) : base(_nCount) {
        sName = "Wolf Pelt";
        itemtype = ItemType.WolfPelt;
        nBaseValue = 15;
    }

    public override void InitAbility() {
        abil = new AbilLurk();
    }

    public override void OnEquip() {
        Debug.LogFormat("Equipped Wolf Pelt");
    }

    public override void OnUnequip() {
        Debug.LogFormat("Unequipped Wolf Pelt");
    }
}
