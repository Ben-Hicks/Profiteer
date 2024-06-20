using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureFarmWheat : Feature {

    private FeatureFarmWheat(TileTerrain _tileterrain) : base(_tileterrain) {

        tileterrain.tileinfo.tileSearchables = new TileSearchables(
            tileterrain,
            new Searchable(tileterrain, "Harvest Wheat", 8, "You harvest Wheat from the Farm",
            (Entity ent) => { Debug.LogFormat("{0} is harvesting wheat", ent); }
            ),
            new Searchable(tileterrain, "Cannot Harvest Wheat", 5, "You aren't able to harvest Wheat from the Farm",
            (Entity ent) => { Debug.LogFormat("{0} cannot harvest wheat", ent); }
            )
            );
    }

    public static Feature Create(TileTerrain tileterrain) {
        return new FeatureFarmWheat(tileterrain) {
            featuretype = FeatureType.WheatFarm,
        };
    }
}
