using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeForest : Biome {

    private BiomeForest() { }

    public static Biome Create() {
        return new BiomeForest() {
            biometype = BiomeType.Forest,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.WitchsHut, 1),
                (FeatureType.Cave, 3)
                )
        };
    }
}

public class BiomeCursewoods : Biome {

    private BiomeCursewoods() { }

    public static Biome Create() {
        return new BiomeCursewoods() {
            biometype = BiomeType.Cursewoods,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.Cave, 1),
                (FeatureType.Graveyard, 6),
                (FeatureType.WitchsHut, 5)
                )
        };
    }
}

public class BiomeGreatwoods : Biome {

    private BiomeGreatwoods() { }

    public static Biome Create() {
        return new BiomeGreatwoods() {
            biometype = BiomeType.Greatwoods,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.GoldMine, 3)
                )
        };
    }
}

public class BiomeJungle : Biome {

    private BiomeJungle() { }

    public static Biome Create() {
        return new BiomeJungle() {
            biometype = BiomeType.Jungle,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.WitchsHut, 1)
                )
        };
    }
}

public class BiomeSwamp : Biome {

    private BiomeSwamp() { }

    public static Biome Create() {
        return new BiomeSwamp() {
            biometype = BiomeType.Swamp,
            supportedFeatureTypes = new RandCollection<FeatureType>(
                (FeatureType.WitchsHut, 6),
                (FeatureType.Graveyard, 4)
                )
        };
    }
}
