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
    public RiverType rivertype;

    public int[] arnPropertyValues;
    public float[] arfBiomeScores;

    public int nElevation {
        get { return arnPropertyValues[(int)TileInfoProperties.Elevation]; }
        set { arnPropertyValues[(int)TileInfoProperties.Elevation] = value; }
    }

    public int nWetness {
        get { return arnPropertyValues[(int)TileInfoProperties.Wetness]; }
        set { arnPropertyValues[(int)TileInfoProperties.Wetness] = value; }
    }

    public int nTemperature {
        get { return arnPropertyValues[(int)TileInfoProperties.Temperature]; }
        set { arnPropertyValues[(int)TileInfoProperties.Temperature] = value; }
    }

    public int nLife {
        get { return arnPropertyValues[(int)TileInfoProperties.Life]; }
        set { arnPropertyValues[(int)TileInfoProperties.Life] = value; }
    }

    public int nGoodness {
        get { return arnPropertyValues[(int)TileInfoProperties.Goodness]; }
        set { arnPropertyValues[(int)TileInfoProperties.Goodness] = value; }
    }

    public int nPopulation {
        get { return arnPropertyValues[(int)TileInfoProperties.Population]; }
        set { arnPropertyValues[(int)TileInfoProperties.Population] = value; }
    }

    public int nRarity {
        get { return arnPropertyValues[(int)TileInfoProperties.Rarity]; }
        set { arnPropertyValues[(int)TileInfoProperties.Rarity] = value; }
    }

    public TileInfo(TileTerrain _tile) {
        tile = _tile;
    }

    public void OnUpdate() {
        tile.UpdateTileVisuals();
    }
}
