using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public static class LibAssets {

    public static TileBase[] arAssetTileBaseBiomes = new TileBase[(int)BiomeType.LENGTH];
    public static TileBase[] arAssetTileBaseElevations = new TileBase[(int)BiomeType.LENGTH];
    public static TileBase[] arAssetTileBaseForests = new TileBase[(int)BiomeType.LENGTH];
    public static TileBase[] arAssetTileBaseCities = new TileBase[(int)BiomeType.LENGTH];

    public static TileBase LoadAssetTileBase(BiomeType biometype) {
        if (arAssetTileBaseBiomes[(int)biometype] == null) {

            string sPathToLoad = string.Format("Assets/Tilemaps/Terrain/ssTerrain_{0}.asset", Biome.arsBiomeNames[(int)biometype]);
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

            string sPathToLoad = string.Format("Assets/Tilemaps/Elevation/ssElevation_{0}.asset", Biome.arsElevationTypeNames[(int)elevationtype]);
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

            string sPathToLoad = string.Format("Assets/Tilemaps/Forest/ssForest_{0}.asset", Biome.arsForestTypeNames[(int)foresttype]);
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

            string sPathToLoad = string.Format("Assets/Tilemaps/City/ssCities_{0}.asset", Biome.arsCityTypeNames[(int)citytype]);
            TileBase tilebaseLoaded = (TileBase)AssetDatabase.LoadAssetAtPath(sPathToLoad, typeof(TileBase));
            if (tilebaseLoaded == null) {
                Debug.LogErrorFormat("Could not load Asset \"{0}\"", sPathToLoad);
            }
            arAssetTileBaseCities[(int)citytype] = tilebaseLoaded;
        }

        return arAssetTileBaseCities[(int)citytype];
    }

}
