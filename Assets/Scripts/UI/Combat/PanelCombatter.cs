using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCombatter : MonoBehaviour {
    public Combatter combatter;

    public Image imgHeadshot;

    public Vector2 v2FullHealthbarSize;
    public RectTransform rectHealthbar;
    public Text txtHealth;

    public Vector2 v2FullEnergybarSize;
    public RectTransform rectEnergybar;
    public Text txtEnergy;

    public void cbUpdateHealthbar(Object obj, params object[] args) {
        rectHealthbar.sizeDelta = new Vector2(v2FullHealthbarSize.x * combatter.ent.entinfo.nCurHP.Get() / combatter.ent.entinfo.nMaxHP.Get(), v2FullHealthbarSize.y);
    }

    public void cbUpdateEnergybar(Object obj, params object[] args) {
        rectEnergybar.sizeDelta = new Vector2(v2FullEnergybarSize.x * combatter.ent.entinfo.nCurEnergy.Get() / combatter.ent.entinfo.nMaxEnergy.Get(), v2FullEnergybarSize.y);
    }

    public virtual void Init() {

        rectHealthbar.anchorMax = new Vector2(0f, 0f);
        v2FullHealthbarSize = rectHealthbar.sizeDelta;

        rectEnergybar.anchorMax = new Vector2(0f, 0f);
        v2FullEnergybarSize = rectEnergybar.sizeDelta;

        combatter.ent.entinfo.nCurHP.Subscribe(cbUpdateHealthbar);
        combatter.ent.entinfo.nMaxHP.Subscribe(cbUpdateHealthbar);

        combatter.ent.entinfo.nCurEnergy.Subscribe(cbUpdateEnergybar);
        combatter.ent.entinfo.nMaxEnergy.Subscribe(cbUpdateEnergybar);


    }
}
