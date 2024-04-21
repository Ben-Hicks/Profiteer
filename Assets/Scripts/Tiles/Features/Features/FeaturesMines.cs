using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureCopperMine : Feature {

    private FeatureCopperMine(TileTerrain _tileterrain) : base(_tileterrain) { }

    public static Feature Create(TileTerrain tileterrain) {
        return new FeatureCopperMine(tileterrain) {
            sName = "Copper Mine",
            featuretype = FeatureType.CopperMine,
        };
    }
}


public class FeatureIronMine : Feature {

    private FeatureIronMine(TileTerrain _tileterrain) : base(_tileterrain) { }

    public static Feature Create(TileTerrain tileterrain) {
        return new FeatureIronMine(tileterrain) {
            sName = "Iron Mine",
            featuretype = FeatureType.IronMine,
        };
    }
}


public class FeatureGoldMine : Feature {

    private FeatureGoldMine(TileTerrain _tileterrain) : base(_tileterrain) { }

    public static Feature Create(TileTerrain tileterrain) {
        return new FeatureGoldMine(tileterrain) {
            sName = "Gold Mine",
            featuretype = FeatureType.GoldMine,
        };
    }
}