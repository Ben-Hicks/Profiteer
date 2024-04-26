using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class InfoPanel : Singleton<InfoPanel> {

    public Text txtInfo;

    public Map map;

    public Vector3 v3OldMousePos;
    public Vector3Int v3CurTilePosHovering;

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

    public void ClearInfo() {
        txtInfo.text = "";
    }

    public override void Init() {
        map = Map.Get();
    }

    public void Update() {
        if(v3OldMousePos != Input.mousePosition) {
            Vector3 v3MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int v3NewTilePosHovering = map.tilemapTerrain.WorldToCell(v3MouseWorldPos);
            if(v3CurTilePosHovering != v3NewTilePosHovering) {
                v3CurTilePosHovering = v3NewTilePosHovering;
                SetInfo(map.GetTile(v3CurTilePosHovering.y, v3CurTilePosHovering.x));
            }

            v3OldMousePos = Input.mousePosition;
        }
    }
}
