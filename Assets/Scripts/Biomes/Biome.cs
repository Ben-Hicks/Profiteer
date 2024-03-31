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

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
