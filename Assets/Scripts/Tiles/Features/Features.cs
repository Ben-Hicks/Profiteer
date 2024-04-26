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

    PolarBearDen,

    LENGTH
}



public static class Features {

    public static string[] arsFeatureTypeNames = {
        "Wheat Farm", 

        "Copper Mine", "Iron Mine", "Gold Mine", 

        "Blacksmith", "Potion Shop",

        "Witch's Hut",

        "Cave",

        "Graveyard",

        "Polar Bear Den",
        
    };

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
            
        FeaturePolarBearDen.Create,
    };

    public static void CreateAndSetFeature(TileInfo tileinfo, FeatureType featuretype) {

        tileinfo.feature = arFeatureConstructors[(int)featuretype](tileinfo.tile);
    }
}
