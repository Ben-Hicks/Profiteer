using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileInfoProperties { Elevation, Wetness, Temperature, Life, Goodness, Rarity, LENGTH };

public class TileInfo {

    public static readonly string[] arsPropertyNames = { "Elevation", "Wetness", "Temperature", "Life", "Goodness", "Rarity", "LENGTH" };

    public TileTerrain tile;

    public int nColumnAccordingToThread;
    public int iThreadMadeBy;

    public BiomeType biometype;

    public int[] arnPropertyValues;
    public float[] arfBiomeScores;

    public TileInfo(TileTerrain _tile) {
        tile = _tile;
    }

    public void OnUpdate() {
        tile.UpdateTileVisuals();
    }
}
