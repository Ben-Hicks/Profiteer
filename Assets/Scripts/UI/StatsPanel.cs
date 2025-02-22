﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : Singleton<StatsPanel> {

    public Text txtName;
    public GameObject goStatsContainer;

    public List<StatEntry> lstStatEntry = new List<StatEntry>();

    public GameObject pfStatEntry;
    public GameObject pfSeperator;

    public Entity ent;

    public HideablePanel panelContent;

    public void SetEntity(Entity _ent) {
        ent = _ent;

        ClearStats();

        CreateStats();

        panelContent.Show();
    }

    public void AddStat(string sLabel, StatEntry.GetValueString getvaluestring, params Subject[] subToUpdateOnChange) {
        GameObject goNewStatEntry = Instantiate(pfStatEntry, goStatsContainer.transform);

        StatEntry statentry = goNewStatEntry.GetComponent<StatEntry>();
        if(statentry == null) {
            Debug.LogError("ERROR! - No StatEntry on the stat prefab");
            return;
        }

        statentry.SetEntity(ent);
        statentry.SetLabel(sLabel);
        statentry.SetGetValueString(getvaluestring);
        statentry.SetSubUpdateOnChange(subToUpdateOnChange);

        //Simulate an update to get an initial display;
        statentry.cbUpdateValueString(null);

        lstStatEntry.Add(statentry);
    }

    public void AddSeperator(string sLabel) {
        GameObject goNewSeperator = Instantiate(pfSeperator, goStatsContainer.transform);

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
        for(int i=0; i<lstStatEntry.Count; i++) {
            lstStatEntry[i].Destroy();
        }

        lstStatEntry = new List<StatEntry>();
    }

    public override void Init() {
        panelContent.Hide();
        txtName.text = "NAME";
    }
    
}
