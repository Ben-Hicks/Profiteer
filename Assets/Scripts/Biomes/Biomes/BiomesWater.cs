using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeRiver : Biome {

    private BiomeRiver() { }

    public static Biome Create() {
        return new BiomeRiver() {
            biometype = BiomeType.River,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                
                )
        };
    }
}

public class BiomeLake : Biome {

    private BiomeLake() { }

    public static Biome Create() {
        return new BiomeLake() {
            biometype = BiomeType.Lake,
            supportedFeatureTypes = new RandCollection<FeatureType>(

                )
        };
    }
}

public class BiomeOcean : Biome {

    private BiomeOcean() { }

    public static Biome Create() {
        return new BiomeOcean() {
            biometype = BiomeType.Ocean,
            supportedFeatureTypes = new RandCollection<FeatureType>(

                )
        };
    }
}
