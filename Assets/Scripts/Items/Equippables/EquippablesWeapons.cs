using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponShortsword : EquippableWeapon {

    public class AbilShortswordSlash : Ability {

        public AbilShortswordSlash() : base("Slash", "{0} is slashing {1}") {

        }

        public override void Execute(Combatter combatterSource, Combatter combatterTarget) {
            Debug.LogFormat(sBaseDescription, combatterSource, combatterTarget);
        }

        public override Targetter.FnTarget InitCanTarget() {
            return Targetter.IsEnemy;
        }
    }

    public class AbilShortswordStab : Ability {

        public AbilShortswordStab() : base("Stab", "{0} is stabbing {1}") {

        }

        public override void Execute(Combatter combatterSource, Combatter combatterTarget) {
            Debug.LogFormat(sBaseDescription, combatterSource, combatterTarget);
        }

        public override Targetter.FnTarget InitCanTarget() {
            return Targetter.IsEnemy;
        }
    }

    public WeaponShortsword(int _nCount) : base(_nCount) {
        sName = "Shortsword";
        itemtype = ItemType.Shortsword;
        nBaseValue = 35;
    }

    public override void InitAbilities() {
        abil1 = new AbilShortswordStab();
        abil2 = new AbilShortswordSlash();
    }

    public override void OnEquip() {
        Debug.LogFormat("Equipped a Shortsword");
    }

    public override void OnUnequip() {
        Debug.LogFormat("Unequipped a Shortsword");
    }
}


public class WeaponWolfClaws : EquippableWeapon {

    public class AbilWolfClawsSwipe : Ability {

        public AbilWolfClawsSwipe() : base("Swipe", "{0} is swiping {1}") {

        }

        public override void Execute(Combatter combatterSource, Combatter combatterTarget) {
            Debug.LogFormat(sBaseDescription, combatterSource, combatterTarget);
        }

        public override Targetter.FnTarget InitCanTarget() {
            return Targetter.IsEnemy;
        }
    }

    public class AbilWolfClawsRake : Ability {

        public AbilWolfClawsRake() : base("Rake", "{0} is raking {1}") {

        }

        public override void Execute(Combatter combatterSource, Combatter combatterTarget) {
            Debug.LogFormat(sBaseDescription, combatterSource, combatterTarget);
        }

        public override Targetter.FnTarget InitCanTarget() {
            return Targetter.IsEnemy;
        }
    }

    public WeaponWolfClaws(int _nCount) : base(_nCount) {
        sName = "Wolf Calws";
        itemtype = ItemType.Shortsword;
        nBaseValue = 10;
    }

    public override void InitAbilities() {
        abil1 = new AbilWolfClawsSwipe();
        abil2 = new AbilWolfClawsRake();
    }

    public override void OnEquip() {
        Debug.LogFormat("Equipped Wolf's Claws");
    }

    public override void OnUnequip() {
        Debug.LogFormat("Unequipped Wolf's Claws");
    }
}
