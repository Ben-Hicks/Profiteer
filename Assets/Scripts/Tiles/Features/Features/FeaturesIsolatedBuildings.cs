using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureWitchsHut : Feature {

    private FeatureWitchsHut(TileTerrain _tileterrain) : base(_tileterrain) { }

    public static Feature Create(TileTerrain tileterrain) {
        return new FeatureWitchsHut(tileterrain) {
            featuretype = FeatureType.WitchsHut,
    };
    }
}