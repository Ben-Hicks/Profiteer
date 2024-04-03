using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;

//[System.Serializable]
public class TileTerrain {
    public Map map;

    public Vector3Int v3Coords;
    public Vector3 v3WorldPosition;

    public Tile tmtile;
    public TileInfo tileinfo;
    public Entity ent;

    public TileTerrain[] arAdj;
    public TileTerrain U {
        get { return arAdj[(int)Dir.U]; }
        set { arAdj[(int)Dir.U] = value; }
    }
    public TileTerrain UR {
        get { return arAdj[(int)Dir.UR]; }
        set { arAdj[(int)Dir.UR] = value; }
    }
    public TileTerrain DR {
        get { return arAdj[(int)Dir.DR]; }
        set { arAdj[(int)Dir.DR] = value; }
    }
    public TileTerrain D {
        get { return arAdj[(int)Dir.D]; }
        set { arAdj[(int)Dir.D] = value; }
    }
    public TileTerrain DL {
        get { return arAdj[(int)Dir.DL]; }
        set { arAdj[(int)Dir.DL] = value; }
    }
    public TileTerrain UL {
        get { return arAdj[(int)Dir.UL]; }
        set { arAdj[(int)Dir.UL] = value; }
    }

    public TileTerrain(Map _map, int x, int y) {
        map = _map;
        v3Coords = new Vector3Int(y, x, 0);
        arAdj = new TileTerrain[6];
        tileinfo = new TileInfo(this);
        v3WorldPosition = map.tilemapTerrain.CellToWorld(v3Coords);
    }
    
    public override string ToString() {
        return string.Format("({0},{1})", v3Coords.x, v3Coords.y);
    }

    public void UpdateTileVisuals() {
        DisplayBiome();
        DisplayElevation();
    }

    public void DisplayBiome() {
        map.tilemapTerrain.SetTile(v3Coords, map.lsttmtileTerrain[(int)tileinfo.biometype]);
        map.tilemapTerrain.SetTileFlags(v3Coords, TileFlags.None);
        map.tilemapTerrain.SetColor(v3Coords, Color.white);
    }

    public void DisplayElevation() {
        map.tilemapElevation.SetTile(v3Coords, map.lsttmtileElevation[(int)tileinfo.elevationtype]);
        map.tilemapElevation.SetTileFlags(v3Coords, TileFlags.None);
        map.tilemapElevation.SetColor(v3Coords, Color.white);
    }

    public void DisplayProperty(TileInfoProperties property) {
        if (tileinfo.arnPropertyValues == null) return;

        //Debug.LogFormat("Setting colour {0}", MapGenerator.Get().GetPropertyColour(property, tileinfo.arnPropertyValues[(int)property]));

        map.tilemapTerrain.SetTile(v3Coords, map.lsttmtileTerrain[0]);
        map.tilemapTerrain.SetTileFlags(v3Coords, TileFlags.None);
        map.tilemapTerrain.SetColor(v3Coords, MapGenerator.Get().GetPropertyColour(property, tileinfo.arnPropertyValues[(int)property]));
    }

    
}
