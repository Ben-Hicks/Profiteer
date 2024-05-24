using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEntityMove : ActionEntity {

    public ActionEntityMove(Entity _ent, TileTerrain _tileTarget) : base(_ent, _tileTarget, 0, 0){

    }

    public override string GetPrintableString() {
        return string.Format("{0} moving to {1}", ent, tileTarget);
    }

    public override IEnumerator ActionEffect() {


        ent.txtDebug.text = string.Format("Moving with energy {0}/{1}", ent.entinfo.nCurEnergy, ent.entinfo.nMaxEnergy);

        Debug.LogFormat("Finished moving to {0}", tileTarget);

        return null;
    }

}
