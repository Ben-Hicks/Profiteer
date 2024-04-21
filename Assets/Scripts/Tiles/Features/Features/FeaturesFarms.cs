using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureFarmWheat : Feature {

    private FeatureFarmWheat(TileTerrain _tileterrain) : base(_tileterrain) { }

    public static Feature Create(TileTerrain tileterrain) {
        return new FeatureFarmWheat(tileterrain) {
            sName = "Wheat Farm",
            featuretype = FeatureType.WheatFarm,
        };
    }
}
