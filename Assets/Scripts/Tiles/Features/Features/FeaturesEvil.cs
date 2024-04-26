using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureGraveyard : Feature {

    private FeatureGraveyard(TileTerrain _tileterrain) : base(_tileterrain) { }

    public static Feature Create(TileTerrain tileterrain) {
        return new FeatureGraveyard(tileterrain) {
            featuretype = FeatureType.Graveyard,
    };
    }
}