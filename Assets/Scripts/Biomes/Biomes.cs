using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BiomeType {
    Arctic, Tundra, Icefields,
    Savannah, Plains, Desert, Wasteland, Mesas,
    Volcanic, Ashland,
    Forest, Cursewoods, Jungle, Greatwoods, Swamp,
    Valley, Highlands, Mountains,
    Grasslands, Meadows,
    River, Lake, Ocean

    , LENGTH
};


public enum ElevationType {
    None, Valley, Hills, Mountains

    , LENGTH
}

public enum ForestType {
    None, Forest, Cursewoods, Greatwoods, Swamp, Jungle

    , LENGTH
}

public enum WaterType {
    None,
    UU, UUR, UDR, UD, UDL, UUL,
    URU, URUR, URDR, URD, URDL, URUL,
    DRU, DRUR, DRDR, DRD, DRDL, DRUL,
    DU, DUR, DDR, DD, DDL, DUL,
    DLU, DLUR, DLDR, DLD, DLDL, DLUL,
    ULU, ULUR, ULDR, ULD, ULDL, ULUL,

    Lake, Ocean,

    LENGTH
}

public enum CityType {
    None, Village, Town, City, Capital,

    LENGTH
}

public class Biomes {

    public static readonly string[] arsBiomeNames = {
            "Arctic", "Tundra", "Icefields",
            "Savannah", "Plains", "Desert", "Wasteland", "Mesas",
            "Volcanic", "Ashland",
            "Forest", "Cursewoods", "Jungle", "Greatwoods", "Swamp",
            "Valley", "Highlands", "Mountains",
            "Grasslands", "Meadows",
            "River", "Lake", "Ocean"
            , "LENGTH" };

    public static readonly string[] arsElevationTypeNames = { "None", "Valley", "Hills", "Mountains" };

    public static readonly string[] arsForestTypeNames = { "None", "Forest", "Cursewoods", "Greatwoods", "Swamp", "Jungle" };

    public static readonly string[] arsWaterTypeNames = {
        "None",
    "UU", "UUR", "UDR", "UD", "UDL", "UUL",
    "URU", "URUR", "URDR", "URD", "URDL", "URUL",
    "DRU", "DRUR", "DRDR", "DRD", "DRDL", "DRUL",
    "DU", "DUR", "DDR", "DD", "DDL", "DUL",
    "DLU", "DLUR", "DLDR", "DLD", "DLDL", "DLUL",
    "ULU", "ULUR", "ULDR", "ULD", "ULDL", "ULUL",

    "Lake", "Ocean"

    };

    public static readonly string[] arsCityTypeNames = { "None", "Village", "Town", "City", "Capital" };



    public static Biome[] arBiomes = new Biome[(int)BiomeType.LENGTH];

    public static System.Func<Biome>[] arBiomeConstructors = {

        BiomeArctic.Create, BiomeTundra.Create, BiomeIceFields.Create,
        BiomeSavannah.Create, BiomePlains.Create, BiomeDesert.Create, BiomeWasteland.Create, BiomeMesas.Create,
        BiomeVolcanic.Create, BiomeAshland.Create,
        BiomeForest.Create, BiomeCursewoods.Create, BiomeJungle.Create, BiomeGreatwoods.Create, BiomeSwamp.Create,
        BiomeValley.Create, BiomeHighlands.Create, BiomeMountains.Create,
        BiomeGrasslands.Create, BiomeMeadows.Create, 
        BiomeRiver.Create, BiomeLake.Create, BiomeOcean.Create
    };

    public static Biome GetBiome(BiomeType biometype) {

        int i = (int)biometype;

        if (arBiomes[i] == null) {
            arBiomes[i] = arBiomeConstructors[i]();
        }

        return arBiomes[i];
    }

    public static RandCollection<FeatureType> randcolCityFeatures = new RandCollection<FeatureType>(
            (FeatureType.Blacksmith, 2),
            (FeatureType.PotionShop, 2)
        );
}
