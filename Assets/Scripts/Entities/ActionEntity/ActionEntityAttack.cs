using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEntityAttack : ActionEntity {

    public ActionEntityAttack(Entity _ent, TileTerrain _tileTarget) : base(_ent, _tileTarget, 30, 1) {

    }

    public override string GetPrintableString() {
        return string.Format("{0}  Attacking {1}", ent, tileTarget.ent);
    }

    public override IEnumerator ActionEffect() {

        //TODO (combat stuff)

        ent.txtDebug.text = string.Format("Attacking {0}", ent.entinfo.nCurTurnBeforeResting, ent.entinfo.nMaxTurnsBeforeResting);

        Debug.LogFormat("Finished attacking {0}", tileTarget.ent);

        return null;
    }

}
