using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability  {
    
    public string sName;
    public string sBaseDescription;
    public Targetter.FnTarget fnCanTarget;

    public abstract void Execute(Combatter combatterSource, Combatter combatterTarget);
    public abstract Targetter.FnTarget InitCanTarget();

    public string GetDescription() {
        return GetDescription("this character", "an enemy");
    }


    public string GetDescription(Entity entSource, Entity entTarget) {
        return GetDescription(entSource.entinfo.sName, entTarget.entinfo.sName);
    }

    public string GetDescription(string sSource, string sTarget) {
        string sDescription = string.Format(sBaseDescription, "this character", "an enemy");

        sDescription = char.ToUpper(sDescription[0]) + sDescription.Substring(1);

        return sDescription;
    }

    public Ability(string _sName, string _sBaseDescription) {
        sName = _sName;
        sBaseDescription = _sBaseDescription;
        fnCanTarget = InitCanTarget();
    }
    
}

public class AbilityRest : Ability {

    public AbilityRest() : base("Rest", "{0} is resting") {

    }

    public override Targetter.FnTarget InitCanTarget() {
        return Targetter.IsSelf;
    }

    public override void Execute(Combatter combatterSource, Combatter combatterTarget) {
        Debug.LogFormat(sBaseDescription, combatterSource, combatterTarget);
    }

}
