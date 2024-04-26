using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileInfoProperties { Elevation, Wetness, Temperature, Life, Goodness, Population, Rarity, LENGTH };

public class TileInfo {

    public static readonly string[] arsPropertyNames = { "Elevation", "Wetness", "Temperature", "Life", "Goodness", "Population", "Rarity", "LENGTH" };

    public TileTerrain tile;

    public int nColumnAccordingToThread;
    public int iThreadMadeBy;

    public BiomeType biometype;
    public ElevationType elevationtype;
    public ForestType foresttype;
    public CityType citytype;
    public WaterType watertype;
    public Feature feature;

    public float[] arfPropertyValues;
    public float[] arfBiomeScores;

    public float fElevation {
        get { return arfPropertyValues[(int)TileInfoProperties.Elevation]; }
        set { arfPropertyValues[(int)TileInfoProperties.Elevation] = value; }
    }

    public float fWetness {
        get { return arfPropertyValues[(int)TileInfoProperties.Wetness]; }
        set { arfPropertyValues[(int)TileInfoProperties.Wetness] = value; }
    }

    public float fTemperature {
        get { return arfPropertyValues[(int)TileInfoProperties.Temperature]; }
        set { arfPropertyValues[(int)TileInfoProperties.Temperature] = value; }
    }

    public float fLife {
        get { return arfPropertyValues[(int)TileInfoProperties.Life]; }
        set { arfPropertyValues[(int)TileInfoProperties.Life] = value; }
    }

    public float fGoodness {
        get { return arfPropertyValues[(int)TileInfoProperties.Goodness]; }
        set { arfPropertyValues[(int)TileInfoProperties.Goodness] = value; }
    }

    public float fPopulation {
        get { return arfPropertyValues[(int)TileInfoProperties.Population]; }
        set { arfPropertyValues[(int)TileInfoProperties.Population] = value; }
    }

    public float fRarity {
        get { return arfPropertyValues[(int)TileInfoProperties.Rarity]; }
        set { arfPropertyValues[(int)TileInfoProperties.Rarity] = value; }
    }

    public bool IsWater() {
        return biometype == BiomeType.Ocean
            || biometype == BiomeType.Lake
            || biometype == BiomeType.River;
    }

    public TileInfo(TileTerrain _tile) {
        tile = _tile;
    }

    public void OnUpdate() {
        tile.UpdateTileVisuals();
    }

    public void WetnessBomb(float fWetnessAmount) {
        Map.Get().FoldHex2(tile, 0, (TileTerrain t, int y) => {
            t.tileinfo.fWetness += fWetnessAmount;
            return y;
        });
    }
}
