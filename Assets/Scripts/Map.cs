﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : Singleton<Map> {

    public float fTileHeight;
    public float fTileWidth;

    public int nMapHeight;
    public int nMapWidth;

    public List<Entity> lstEntities;

    [System.Serializable]
    public class Col {
        public List<Tile> lstTiles;
        public int Count {
            get { return lstTiles.Count; }
        }

        public Col() {
            lstTiles = new List<Tile>();
        }

        public Tile this[int index] => this.lstTiles[index];
        public void Add(Tile t) {
            lstTiles.Add(t);
        }
    }
    public List<Col> lstTiles;

    public Tilemap tilemap;

    public GameObject pfTile;
    public GameObject pfEntity;

    public GameObject goMapContainer;
    public GameObject goEntityContainer;


    public Tile GetTile(int x, int y) {

        if (y < 0 || x < 0) return null;
        if (y >= nMapHeight || x >= nMapWidth) return null;

        return lstTiles[x][y];
    }

    public Tile GetTileInDir(Tile tile, Dir dir, int nTileDist = 1) {
        for(int i=0; i<nTileDist; i++) {
            tile = tile.arAdj[(int)dir];
        }
        return tile;

    }

    public void SpawnEntity(Tile tile) {
        GameObject goEntityNew = GameObject.Instantiate(pfEntity, goEntityContainer.transform);
        Entity entNew = goEntityNew.GetComponent<Entity>();

        entNew.InitOnTile(tile);

        entNew.SetId(lstEntities.Count);
        lstEntities.Add(entNew);
    }
    

    public void SpawnTile(int x, int y) {
        GameObject goTileNew = GameObject.Instantiate(pfTile, goMapContainer.transform);
        Tile tileNew = goTileNew.GetComponent<Tile>();

        tileNew.InitCoords(x, y);

        lstTiles[x].Add(tileNew);

        //Set up connections to adjacents
        Tile tileD = GetTile(x, y - 2);
        if (tileD != null) {
            tileD.U = tileNew;
            tileNew.D = tileD;
        }

        Tile tileUL = GetTile(x - 1, x % 2 == 0 ? y : y + 1);
        if (tileUL != null) {
            tileUL.DR = tileNew;
            tileNew.UL = tileUL;
        }

        Tile tileDL = GetTile(x - 1, x % 2 == 0 ? y - 1 : y);
        if (tileDL != null) {
            tileDL.UR = tileNew;
            tileNew.DL = tileDL;
        }

    }

    public void SpawnMap() {

        if(lstTiles != null && lstTiles.Count == nMapWidth && lstTiles[0].Count == nMapHeight) {
            Debug.Log("Map Already Exists with correct dimensions!:  Not spawning anything new");
            return;
        }
        //If we're at this point, we need to clear away any previous map to ensure we have a blank slate to start from
        ClearMap();

        lstTiles = new List<Col>();
        
        for (int x=0; x<nMapWidth; x++) {
            lstTiles.Add(new Col());

            for (int y = 0; y < nMapHeight; y++) {

                SpawnTile(x, y);

            }
        }
    }

    public void PopulateMap() {

        
        //Threader.Get().DistributeTask<List<Tile>>(TaskType.TileInfoPopulation, lstTiles,
        Threader.Get().DistributeTask(TaskType.TileInfoPopulation, lstTiles,

            (List<Tile> lstCol) => {
                Debug.LogFormat("Working on column {0}", lstCol[0].coords.x);
                //foreach (Tile t in lstCol) {
                //    MapGenerator.Get().PopulateTileInfo(t.tileinfo);
                //}
            }

            , FinishPopulateMap);
            
    }

    public void FinishThreadTest() {
        Debug.Log("Thread Test finished");
        UpdateAllTileVisuals();
    }

    public void FinishPopulateMap() {
        Debug.Log("Finished populating map");
        UpdateAllTileVisuals();
        //SetBiomes();
    }


    public void UpdateAllTileVisuals() {

        if (lstTiles != null) {
            foreach (Col col in lstTiles) {
                foreach (Tile t in col.lstTiles) {
                    t.UpdateTileVisuals();
                }
            }
        }
    }

    public void ClearMap() {

        if (lstEntities != null) {
            foreach (Entity ent in lstEntities) {
                GameObject.DestroyImmediate(ent.gameObject);
            }
            lstEntities = null;
        }

        if (lstTiles != null) {
            foreach (Col col in lstTiles) {
                foreach (Tile t in col.lstTiles) {
                    GameObject.DestroyImmediate(t.gameObject);
                }
            }
            lstTiles = null;
        }
        
    }

    public void SpawnRandomEntity() {
        int y = Random.Range(0, nMapHeight);
        int x = Random.Range(0, nMapWidth);
        if ((y + x) % 2 == 1) {
            if (x > 0) x--;
            else x++;
        }
        SpawnEntity(GetTile(x, y));
    }

    public override void Init() {

        SpawnMap();

        //PopulateMap();

        /*
        if (lstEntities != null) {
            Debug.Log("We already have entities - no need to spawn more");
        } else {
            lstEntities = new List<Entity>();

            SpawnEntity(dictTiles[4][4]);
        }
        */
    }
}
