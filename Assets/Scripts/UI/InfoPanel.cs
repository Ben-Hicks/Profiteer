﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : Singleton<InfoPanel> {

    public Text txtInfo;



    public void SetInfo(string sInfo) {
        txtInfo.text = sInfo;
    }

    public void SetInfo(Tile tile) {
        string sInfo = string.Format("Type:XXX");

        for(int i=0; i<(int)TileInfoProperties.LENGTH; i++) {
            sInfo += string.Format("\n{0}: {1}", TileInfo.arsPropertyNames[i], tile.tileinfo.arnPropertyValues[i]);
        }

        sInfo += "\nBiomes:";
        for(int i=0; i<(int)BiomeType.LENGTH; i++) {
            sInfo += string.Format("\n{0}: {1}{2}", Biome.arsBiomeNames[i], tile.tileinfo.arfBiomeScores[i],
                i == (int)tile.tileinfo.biometype ? "***" : "");
        }

        txtInfo.text = sInfo;
    }

    public void ClearInfo() {
        txtInfo.text = "";
    }

    public override void Init() {
        
    }
}
