using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : Singleton<StatsPanel> {

    public Text txtName;
    public GameObject goStatsContainer;

    public GameObject pfStatEntry;
    public GameObject pfStatSeperator;

    public Entity ent;

    public void SetEntity(Entity _ent) {
        ent = _ent;

        ClearStats();

        CreateStats();

        Show();
    }

    public void Hide() {
        Debug.Log("Hiding");
        GetComponent<CanvasRenderer>().SetAlpha(0f);
    }

    public void Show() {
        Debug.Log("Showing");
        GetComponent<CanvasRenderer>().SetAlpha(1f);
    }

    public void AddStat(string sLabel, StatEntry.GetValueString getvaluestring, params Subject[] subToUpdateOnChange) {
        GameObject goNewStatEntry = Instantiate(pfStatEntry, goStatsContainer.transform);

        StatEntry statentry = goNewStatEntry.GetComponent<StatEntry>();
        if(statentry == null) {
            Debug.LogError("ERROR! - No StatEntry on the stat prefab");
            return;
        }

        statentry.SetEntity(ent);
        Debug.LogFormat("Entity was set to {0}", ent);
        statentry.SetLabel(sLabel);
        statentry.SetGetValueString(getvaluestring);
        statentry.SetSubUpdateOnChange(subToUpdateOnChange);

        //Simulate an update to get an initial display;
        statentry.cbUpdateValueString(null);
        
    }

    public void AddSeperator(string sLabel) {
        GameObject goNewSeperator = Instantiate(pfStatSeperator, goStatsContainer.transform);

        goNewSeperator.GetComponentInChildren<Text>().text = sLabel;
    }

    public void CreateStats() {

        AddSeperator("Level");
        AddStat("Level:", (Entity ent) => { return ent.entinfo.nLevel.Get().ToString(); },
            ent.entinfo.nLevel.subValChanged);
        AddStat("XP:", (Entity ent) => { return string.Format("{0}/{1}", ent.entinfo.nCurXP.Get(), ent.entinfo.nMaxXP.Get()); },
            ent.entinfo.nCurXP.subValChanged, ent.entinfo.nMaxXP.subValChanged);

        AddSeperator("Combat");
        AddStat("Health:", (Entity ent) => { return string.Format("{0}/{1}", ent.entinfo.nCurHP.Get(), ent.entinfo.nMaxHP.Get()); },
            ent.entinfo.nCurHP.subValChanged, ent.entinfo.nMaxHP.subValChanged);

        AddSeperator("Energy");
        AddStat("Energy:", (Entity ent) => { return string.Format("{0}/{1}", ent.entinfo.nCurEnergy.Get(), ent.entinfo.nMaxEnergy.Get()); },
            ent.entinfo.nCurEnergy.subValChanged, ent.entinfo.nMaxEnergy.subValChanged);

        AddSeperator("Core Stats");
        AddStat("Perception", (Entity ent) => { return ent.entinfo.nPerception.Get().ToString(); },
            ent.entinfo.nPerception.subValChanged);
        AddStat("Swiftness", (Entity ent) => { return ent.entinfo.nSwiftness.Get().ToString(); },
            ent.entinfo.nSwiftness.subValChanged);
        AddStat("Cunning", (Entity ent) => { return ent.entinfo.nCunning.Get().ToString(); },
            ent.entinfo.nCunning.subValChanged);
        AddStat("Bartering", (Entity ent) => { return ent.entinfo.nBartering.Get().ToString(); },
            ent.entinfo.nBartering.subValChanged);
        AddStat("Charisma", (Entity ent) => { return ent.entinfo.nCharisma.Get().ToString(); },
            ent.entinfo.nCharisma.subValChanged);
        AddStat("Tradesmanship", (Entity ent) => { return ent.entinfo.nTradesmanship.Get().ToString(); },
            ent.entinfo.nTradesmanship.subValChanged);

        AddSeperator("Investigation Stats");
        AddStat("Sight Range", (Entity ent) => { return ent.entinfo.nSightRange.Get().ToString(); },
            ent.entinfo.nSightRange.subValChanged);
        AddStat("Investigation", (Entity ent) => { return ent.entinfo.nInvestigation.Get().ToString(); },
            ent.entinfo.nInvestigation.subValChanged);

}

    public void ClearStats() {
        foreach (Transform transStatEntry in goStatsContainer.transform) {
            Destroy(transStatEntry.gameObject);
        }
    }

    public override void Init() {
        Hide();
        txtName.text = "NAME";
    }
    
}
