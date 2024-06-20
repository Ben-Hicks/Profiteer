using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public static class LibAssets {

    public static TileBase[] arAssetTileBaseBiomes = new TileBase[(int)BiomeType.LENGTH];
    public static TileBase[] arAssetTileBaseElevations = new TileBase[(int)ElevationType.LENGTH];
    public static TileBase[] arAssetTileBaseForests = new TileBase[(int)ForestType.LENGTH];
    public static TileBase[] arAssetTileBaseCities = new TileBase[(int)CityType.LENGTH];
    public static TileBase[] arAssetTileBaseFeatures = new TileBase[(int)FeatureType.LENGTH];
    public static TileBase[] arAssetTileBaseHighlights = new TileBase[1];

    public static TileBase LoadAssetTileBase(BiomeType biometype) {
        if (arAssetTileBaseBiomes[(int)biometype] == null) {

            string sPathToLoad = string.Format("Assets/Tilemaps/Terrain/ssTerrain_{0}.asset", Biomes.arsBiomeNames[(int)biometype]);
            TileBase tilebaseLoaded = (TileBase)AssetDatabase.LoadAssetAtPath(sPathToLoad, typeof(TileBase));
            if (tilebaseLoaded == null) {
                Debug.LogErrorFormat("Could not load Asset \"{0}\"", sPathToLoad);
            }
            arAssetTileBaseBiomes[(int)biometype] = tilebaseLoaded;
        }

        return arAssetTileBaseBiomes[(int)biometype];
    }

    public static TileBase LoadAssetTileBase(ElevationType elevationtype) {
        if (arAssetTileBaseElevations[(int)elevationtype] == null) {

            string sPathToLoad = string.Format("Assets/Tilemaps/MultiFeatures/Elevation/ssElevation_{0}.asset", Biomes.arsElevationTypeNames[(int)elevationtype]);
            TileBase tilebaseLoaded = (TileBase)AssetDatabase.LoadAssetAtPath(sPathToLoad, typeof(TileBase));
            if (tilebaseLoaded == null) {
                Debug.LogErrorFormat("Could not load Asset \"{0}\"", sPathToLoad);
            }
            arAssetTileBaseElevations[(int)elevationtype] = tilebaseLoaded;
        }

        return arAssetTileBaseElevations[(int)elevationtype];
    }

    public static TileBase LoadAssetTileBase(ForestType foresttype) {
        if (arAssetTileBaseForests[(int)foresttype] == null) {

            string sPathToLoad = string.Format("Assets/Tilemaps/MultiFeatures/Forest/ssForest_{0}.asset", Biomes.arsForestTypeNames[(int)foresttype]);
            TileBase tilebaseLoaded = (TileBase)AssetDatabase.LoadAssetAtPath(sPathToLoad, typeof(TileBase));
            if (tilebaseLoaded == null) {
                Debug.LogErrorFormat("Could not load Asset \"{0}\"", sPathToLoad);
            }
            arAssetTileBaseForests[(int)foresttype] = tilebaseLoaded;
        }

        return arAssetTileBaseForests[(int)foresttype];
    }

    public static TileBase LoadAssetTileBase(CityType citytype) {
        if (arAssetTileBaseCities[(int)citytype] == null) {

            string sPathToLoad = string.Format("Assets/Tilemaps/MultiFeatures/City/ssCities_{0}.asset", Biomes.arsCityTypeNames[(int)citytype]);
            TileBase tilebaseLoaded = (TileBase)AssetDatabase.LoadAssetAtPath(sPathToLoad, typeof(TileBase));
            if (tilebaseLoaded == null) {
                Debug.LogErrorFormat("Could not load Asset \"{0}\"", sPathToLoad);
            }
            arAssetTileBaseCities[(int)citytype] = tilebaseLoaded;
        }

        return arAssetTileBaseCities[(int)citytype];
    }

    public static TileBase LoadAssetTileBase(FeatureType featuretype) {
        if (arAssetTileBaseFeatures[(int)featuretype] == null) {

            string sPathToLoad = string.Format("Assets/Tilemaps/Features/ssFeatures_{0}.asset", Features.arsFeatureTypeNames[(int)featuretype]);
            TileBase tilebaseLoaded = (TileBase)AssetDatabase.LoadAssetAtPath(sPathToLoad, typeof(TileBase));
            if (tilebaseLoaded == null) {
                Debug.LogErrorFormat("Could not load Asset \"{0}\"", sPathToLoad);
            }
            arAssetTileBaseFeatures[(int)featuretype] = tilebaseLoaded;
        }

        return arAssetTileBaseFeatures[(int)featuretype];
    }

    public static TileBase LoadAssetHighlight() {
        //Currently only supporting 1 image of highlighting
        if (arAssetTileBaseHighlights[0] == null) {

            string sPathToLoad = string.Format("Assets/Tilemaps/Highlights/ssHighlights_{0}.asset", "Highlight");
            TileBase tilebaseLoaded = (TileBase)AssetDatabase.LoadAssetAtPath(sPathToLoad, typeof(TileBase));
            if (tilebaseLoaded == null) {
                Debug.LogErrorFormat("Could not load Asset \"{0}\"", sPathToLoad);
            }
            arAssetTileBaseHighlights[0] = tilebaseLoaded;
        }

        return arAssetTileBaseHighlights[0];
    }

}
