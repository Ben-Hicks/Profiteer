using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeValley : Biome {

    private BiomeValley() { }

    public static Biome Create() {
        return new BiomeValley() {
            biometype = BiomeType.Valley,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.Cave, 7),
                (FeatureType.CopperMine, 4),
                (FeatureType.IronMine, 3)
                )
        };
    }
}

public class BiomeHighlands : Biome {

    private BiomeHighlands() { }

    public static Biome Create() {
        return new BiomeHighlands() {
            biometype = BiomeType.Highlands,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.Cave, 3),
                (FeatureType.CopperMine, 3),
                (FeatureType.IronMine, 8),
                (FeatureType.GoldMine, 4)
                )
        };
    }
}

public class BiomeMountains : Biome {

    private BiomeMountains() { }

    public static Biome Create() {
        return new BiomeMountains() {
            biometype = BiomeType.Mountains,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.Cave, 2),
                (FeatureType.CopperMine, 1),
                (FeatureType.IronMine, 4),
                (FeatureType.GoldMine, 6)
                )
        };
    }
}
