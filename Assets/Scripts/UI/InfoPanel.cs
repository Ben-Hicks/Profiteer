using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class InfoPanel : Singleton<InfoPanel> {

    public Text txtInfo;


    public void SetInfo(string sInfo) {
        txtInfo.text = sInfo;
    }

    public void SetInfo(TileTerrain tile) {
        if(tile == null) {
            ClearInfo();
            return;
        }

        string sInfo = string.Format("({0}, {1})", tile.x, tile.y);

        sInfo += string.Format("\nRegion: {0}", tile.region.sName);
            
        sInfo += string.Format("\nType: {0}", Biomes.arsBiomeNames[(int)tile.tileinfo.biometype]);

        sInfo += string.Format("\nFeature: {0}", tile.tileinfo.feature != null ? tile.tileinfo.feature.sName : "None");

        for(int i=0; i<(int)TileInfoProperties.LENGTH; i++) {
            sInfo += string.Format("\n{0}: {1}", TileInfo.arsPropertyNames[i], tile.tileinfo.arfPropertyValues[i]);
        }

        /*
        sInfo += "\nBiomes:";
        for(int i=0; i<(int)BiomeType.LENGTH; i++) {
            sInfo += string.Format("\n{0}: {1}{2}", Biomes.arsBiomeNames[i], tile.tileinfo.arfBiomeScores[i],
                i == (int)tile.tileinfo.biometype ? "***" : "");
        }
        */

        txtInfo.text = sInfo;
    }

    public void SetInfo(Entity ent) {
        if(ent == null) {
            ClearInfo();
            return;
        }

        string sInfo = string.Format("Entity: {0}", ent);

        txtInfo.text = sInfo;
    }

    public void SetInfo(Ability abil) {
        if(abil == null) {
            ClearInfo();
            return;
        }

        string sInfo = string.Format("Ability: {0}\nDesc: {1}", abil.sName, abil.GetDescription());
    }

    public void ClearInfo() {
        txtInfo.text = "";
    }

    public override void Init() {
        MapInput.Get().subEntHoverChange.Subscribe(cbRefreshDisplay);
        MapInput.Get().subTileHoverChange.Subscribe(cbRefreshDisplay);
    }

    public void cbRefreshDisplay(Object obj, params object[] args) {

        if(MapInput.Get().tileFocused != null) {
            SetInfo(MapInput.Get().tileFocused);
        }else if(MapInput.Get().entHovering != null) {
            SetInfo(MapInput.Get().entHovering);
        } else if (MapInput.Get().tileHovering != null) {
            SetInfo(MapInput.Get().tileHovering);
        } else {
            ClearInfo();
        }
    }

}
