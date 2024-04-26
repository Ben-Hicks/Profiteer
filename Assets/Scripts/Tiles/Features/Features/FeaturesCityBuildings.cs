using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureBlacksmith : Feature {

    private FeatureBlacksmith(TileTerrain _tileterrain) : base(_tileterrain) { }

    public static Feature Create(TileTerrain tileterrain) {
        return new FeatureBlacksmith(tileterrain) {
            featuretype = FeatureType.Blacksmith,
        };
    }
}

public class FeaturePotionShop : Feature {

    private FeaturePotionShop(TileTerrain _tileterrain) : base(_tileterrain) { }

    public static Feature Create(TileTerrain tileterrain) {
        return new FeaturePotionShop(tileterrain) {
            featuretype = FeatureType.PotionShop,
        };
    }
}