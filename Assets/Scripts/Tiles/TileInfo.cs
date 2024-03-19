using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileInfoProperties { Elevation, Wetness, Temperature, Hospitableness, LENGTH };

public class TileInfo : MonoBehaviour {

    public static readonly string[] arsPropertyNames = { "Elevation", "Wetness", "Temperature", "Hospitableness", "LENGTH" };

    public Tile tile;

    public BiomeType biometype;

    public int[] arnPropertyValues;
    public float[] arfBiomeScores;

    public void OnUpdate() {
        tile.UpdateTileVisuals();
    }
}
