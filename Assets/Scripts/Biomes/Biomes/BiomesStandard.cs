using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BiomeGrasslands : Biome {

    private BiomeGrasslands() { }

    public static Biome Create() {
        return new BiomeGrasslands() {
            biometype = BiomeType.Grasslands,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.CopperMine, 1),
                (FeatureType.Graveyard, 1),
                (FeatureType.WheatFarm, 4)
                )
        };
    }
}

public class BiomePlains : Biome {

    private BiomePlains() { }

    public static Biome Create() {
        return new BiomePlains() {
            biometype = BiomeType.Plains,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.CopperMine, 3),
                (FeatureType.WheatFarm, 8)
                )
        };
    }
}

public class BiomeSavannah : Biome {

    private BiomeSavannah() { }

    public static Biome Create() {
        return new BiomeSavannah() {
            biometype = BiomeType.Savannah,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                
                )
        };
    }
}

public class BiomeWasteland : Biome {

    private BiomeWasteland() { }

    public static Biome Create() {
        return new BiomeWasteland() {
            biometype = BiomeType.Wasteland,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.Graveyard, 2)
                )
        };
    }
}

public class BiomeMeadows : Biome {

    private BiomeMeadows() { }

    public static Biome Create() {
        return new BiomeMeadows() {
            biometype = BiomeType.Meadows,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                
                )
        };
    }
}
