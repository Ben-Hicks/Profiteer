using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeAshland : Biome {

    private BiomeAshland() { }

    public static Biome Create() {
        return new BiomeAshland() {
            biometype = BiomeType.Ashland,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.Cave, 1)
                )
        };
    }
}

public class BiomeDesert : Biome {

    private BiomeDesert() { }

    public static Biome Create() {
        return new BiomeDesert() {
            biometype = BiomeType.Desert,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.Cave, 1)
                )
        };
    }
}

public class BiomeMesas : Biome {

    private BiomeMesas() { }

    public static Biome Create() {
        return new BiomeMesas() {
            biometype = BiomeType.Mesas,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.Cave, 3),
                (FeatureType.CopperMine, 3),
                (FeatureType.GoldMine, 5)
                )
        };
    }
}

public class BiomeVolcanic : Biome {

    private BiomeVolcanic() { }

    public static Biome Create() {
        return new BiomeVolcanic() {
            biometype = BiomeType.Volcanic,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.Cave, 4),
                (FeatureType.GoldMine, 7)
                )
        };
    }
}
