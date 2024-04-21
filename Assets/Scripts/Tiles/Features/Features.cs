using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FeatureType {
    WheatFarm,

    CopperMine, IronMine, GoldMine,

    Blacksmith, PotionShop,

    WitchsHut,

    Cave,
    
    Graveyard,

    LENGTH
}

public static class Features {

    private static System.Func<TileTerrain, Feature>[] arFeatureConstructors = {
        FeatureFarmWheat.Create,

        FeatureCopperMine.Create,
        FeatureIronMine.Create,
        FeatureGoldMine.Create,

        FeatureBlacksmith.Create,
        FeaturePotionShop.Create,

        FeatureWitchsHut.Create,

        FeatureCave.Create,

        FeatureGraveyard.Create,
            
    };

    public static Feature CreateFeature( TileTerrain tileterrain, FeatureType featuretype) {

        return arFeatureConstructors[(int)featuretype](tileterrain);
    }
}
