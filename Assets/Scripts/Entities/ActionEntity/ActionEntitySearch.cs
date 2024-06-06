using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEntitySearch : ActionEntity {

    public ActionEntitySearch(Entity _ent, TileTerrain _tileTarget) : base(_ent, _tileTarget, 15, 0) {

    }

    public override string GetPrintableString() {
        return string.Format("{0} searching tile  {1}", ent, tileTarget.ent);
    }

    public override IEnumerator ActionEffect() {

        ent.txtDebug.text = string.Format("Searching {0}", tileTarget);

        tileTarget.tileSearchables.PerformSearch(ent);

        Debug.LogFormat("Finished attacking {0}", tileTarget.ent);

        return null;
    }

}
