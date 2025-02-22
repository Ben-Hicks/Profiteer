﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;

//[System.Serializable]
public class TileTerrain {
    public Map map;

    public Vector3Int v3Coords;
    public Vector3 v3WorldPosition;

    public Tile tmtile;
    public TileInfo tileinfo;
    public Entity ent;
    public Region region;

    public TileSearchables tileSearchables {
        get { return tileinfo.tileSearchables; }
        set { tileinfo.tileSearchables = value; }
    }

    public TileTerrain[] arAdj;
    public TileTerrain U {
        get { return arAdj[(int)Dir.U]; }
        set { arAdj[(int)Dir.U] = value; }
    }
    public TileTerrain UR {
        get { return arAdj[(int)Dir.UR]; }
        set { arAdj[(int)Dir.UR] = value; }
    }
    public TileTerrain DR {
        get { return arAdj[(int)Dir.DR]; }
        set { arAdj[(int)Dir.DR] = value; }
    }
    public TileTerrain D {
        get { return arAdj[(int)Dir.D]; }
        set { arAdj[(int)Dir.D] = value; }
    }
    public TileTerrain DL {
        get { return arAdj[(int)Dir.DL]; }
        set { arAdj[(int)Dir.DL] = value; }
    }
    public TileTerrain UL {
        get { return arAdj[(int)Dir.UL]; }
        set { arAdj[(int)Dir.UL] = value; }
    }

    public int x {
        get { return v3Coords.y; }
    }

    public int y {
        get { return v3Coords.x; }
    }

    public TileTerrain(Map _map, int x, int y) {
        map = _map;
        v3Coords = new Vector3Int(y, x, 0);
        arAdj = new TileTerrain[6];
        tileinfo = new TileInfo(this);
        v3WorldPosition = map.tilemapTerrain.GetCellCenterWorld(v3Coords);
    }
    
    public override string ToString() {
        return string.Format("({0},{1})", this.x, this.y);
    }

    public void UpdateTileVisuals() {
        DisplayTileLayer(map.tilemapTerrain, LibAssets.LoadAssetTileBase(tileinfo.biometype));
        DisplayTileLayer(map.tilemapElevation, LibAssets.LoadAssetTileBase(tileinfo.elevationtype));
        DisplayTileLayer(map.tilemapForest, LibAssets.LoadAssetTileBase(tileinfo.foresttype));
        DisplayTileLayer(map.tilemapCity, LibAssets.LoadAssetTileBase(tileinfo.citytype));
        if(tileinfo.feature != null) DisplayTileLayer(map.tilemapFeatures, LibAssets.LoadAssetTileBase(tileinfo.feature.featuretype));
    }

    public void DisplayTileLayer(Tilemap tilemap, TileBase tilebase) {
        tilemap.SetTile(v3Coords, tilebase);
        tilemap.SetTileFlags(v3Coords, TileFlags.None);
        tilemap.SetColor(v3Coords, Color.white);
    }


    public void DisplayProperty(TileInfoProperties property) {
        if (tileinfo.arfPropertyValues == null) return;

        //Debug.LogFormat("Setting colour {0}", MapGenerator.Get().GetPropertyColour(property, tileinfo.arnPropertyValues[(int)property]));

        map.tilemapTerrain.SetTile(v3Coords, LibAssets.LoadAssetTileBase(BiomeType.Arctic));
        map.tilemapTerrain.SetTileFlags(v3Coords, TileFlags.None);
        map.tilemapTerrain.SetColor(v3Coords, MapGenerator.Get().GetPropertyColour(property, tileinfo.arfPropertyValues[(int)property]));
    }

    public void Highlight() {
        map.tilemapHighlighting.SetTile(v3Coords, LibAssets.LoadAssetHighlight());
        map.tilemapHighlighting.SetTileFlags(v3Coords, TileFlags.None);
        Color colHighlight = new Color(1, 1, 1, 0.4f);
        map.tilemapHighlighting.SetColor(v3Coords, colHighlight);
    }

    public void Unhighlight() {
        map.tilemapHighlighting.SetTile(v3Coords, null);
    }

    public static int Dist(TileTerrain t1, TileTerrain t2) {
        int nDx = Mathf.Abs(t1.x - t2.x);
        int nDy = Mathf.Abs(t1.y - t2.y);
        int nDiag;

        if (nDx % 2 == 0) {
            nDiag = nDx / 2;
        } else if (t1.x % 2 == 1) {
            if (t2.y > t1.y) {
                nDiag = ((nDx + 1) / 2);
            } else {
                nDiag = ((nDx - 1) / 2);
            }
        } else {
            if (t2.y >= t1.y) {
                nDiag = ((nDx - 1) / 2);
            } else {
                nDiag = ((nDx + 1) / 2);
            }
        }

        if (nDiag >= nDy) return nDx;
        
        return nDx + nDy - nDiag;
    }

    public static TileTerrain ClosestToTile(TileTerrain tileTarget, params Entity[] arsEntityCandidates) {
        return ClosestToTile(tileTarget, new List<Entity>(arsEntityCandidates));
    }

    public static TileTerrain ClosestToTile(TileTerrain tileTarget, List<Entity> lstCandidates) {
        List<TileTerrain> lstTileCandidates = new List<TileTerrain>();
        foreach(Entity e in lstCandidates) {
            lstTileCandidates.Add(e.tile);
        }

        return ClosestToTile(tileTarget, lstTileCandidates);
    }

    public static TileTerrain ClosestToTile(TileTerrain tileTarget, params TileTerrain[] argsTileCandidates) {
        return ClosestToTile(tileTarget, new List<TileTerrain>(argsTileCandidates));
    }

    public static TileTerrain ClosestToTile(TileTerrain tileTarget, List<TileTerrain> lstCandidates) {
        if(lstCandidates.Count == 0) {
            Debug.LogError("Cannot find a closest Tile since none were provided");
            return null;
        }

        TileTerrain tileClosestSoFar = lstCandidates[0];
        int nClosestDist = Dist(tileTarget, tileClosestSoFar);

        for(int i=1; i<lstCandidates.Count; i++) {
            int nCurDist = Dist(tileTarget, lstCandidates[i]);
            if (nCurDist < nClosestDist) {
                tileClosestSoFar = lstCandidates[i];
                nClosestDist = nCurDist;
            }
        }

        return tileClosestSoFar;
    }
}
