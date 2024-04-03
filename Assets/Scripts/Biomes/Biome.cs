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
}

public enum ForestType {
    None, Forest, Cursewoods, Greatwoods, Swamp
}

public enum RiverType {
    None,
    UU, UUR, UDR, UD, UDL, UUL,
    URU, URUR, URDR, URD, URDL, URUL,
    DRU, DRUR, DRDR, DRD, DRDL, DRUL,
    DU, DUR, DDR, DD, DDL, DUL,
    DLU, DLUR, DLDR, DLD, DLDL, DLUL,
    ULU, ULUR, ULDR, ULD, ULDL, ULUL
}

public enum CityType {
    None, Village, Town, City, Capital
}

public class Biome : MonoBehaviour {

    public static readonly string[] arsBiomeNames = {
            "Arctic", "Tundra", "Icefields",
            "Savannah", "Plains", "Desert", "Wasteland", "Mesas",
            "Volcanic", "Ashland",
            "Forest", "Cursewoods", "Jungle", "Greatwoods", "Swamp",
            "Valley", "Highlands", "Mountains",
            "Grasslands", "Meadows",
            "River", "Lake", "Ocean"
            , "LENGTH" };

    public static readonly string[] arsElevationTypeNames = { "", "Valley", "Hills", "Mountains" };

    public static readonly string[] arsForestTypeNames = { "", "Forest", "Cursewoods", "Greatwoods", "Swamp" };

    public static readonly string[] arsCityTypeNames = { "", "Village", "Town", "City", "Capital" };

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
