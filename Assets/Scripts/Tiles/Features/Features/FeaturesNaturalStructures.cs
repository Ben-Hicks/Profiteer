using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureCave : Feature {

    private FeatureCave(TileTerrain _tileterrain) : base(_tileterrain) { }

    public static Feature Create(TileTerrain tileterrain) {
        return new FeatureCave(tileterrain) {
            featuretype = FeatureType.Cave,
    };
    }
}
