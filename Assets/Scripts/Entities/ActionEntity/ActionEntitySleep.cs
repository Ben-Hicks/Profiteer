using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEntitySleep : ActionEntity {

    public ActionEntitySleep(Entity _ent, TileTerrain _tileTarget) : base(_ent, _tileTarget, 0, 0) {

    }

    public override string GetPrintableString() {
        return string.Format("{0} Sleeping on {1}", ent, tileTarget);
    }

    public override IEnumerator ActionEffect() {


        ent.entinfo.nCurTurnBeforeResting += 1;

        ent.txtDebug.text = string.Format("Sleep {0}/{1}", ent.entinfo.nCurTurnBeforeResting, ent.entinfo.nMaxTurnsBeforeResting);

        Debug.LogFormat("Finished Sleeping on {0}", tileTarget);

        return null;
    }

}
