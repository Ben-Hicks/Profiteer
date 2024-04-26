using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeaturePolarBearDen : Feature {

    private FeaturePolarBearDen(TileTerrain _tileterrain) : base(_tileterrain) { }

    public static Feature Create(TileTerrain tileterrain) {
        return new FeaturePolarBearDen(tileterrain) {
            featuretype = FeatureType.PolarBearDen,
        };
    }
}