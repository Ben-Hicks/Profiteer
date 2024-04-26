using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeArctic : Biome {

    private BiomeArctic() { }

    public static Biome Create() {
        return new BiomeArctic() {
            biometype = BiomeType.Arctic,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.Cave, 1),
                (FeatureType.PolarBearDen, 4)
                )
        };
    }
}

public class BiomeTundra : Biome {

    private BiomeTundra() { }

    public static Biome Create() {
        return new BiomeTundra() {
            biometype = BiomeType.Tundra,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.Cave, 1),
                (FeatureType.PolarBearDen, 2)
                )
        };
    }
}

public class BiomeIceFields : Biome {

    private BiomeIceFields() { }

    public static Biome Create() {
        return new BiomeIceFields() {
            biometype = BiomeType.Icefields,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.PolarBearDen, 2)
                )
        };
    }
}
