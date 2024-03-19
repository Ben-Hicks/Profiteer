using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : Singleton<Map> {

    public float fTileHeight;
    public float fTileWidth;

    public int nMapHeight;
    public int nMapWidth;

    public List<Entity> lstEntities;
    public Dictionary<int, Dictionary<int, Tile>> dictTiles;

    public GameObject pfTile;
    public GameObject pfEntity;

    public GameObject goMapContainer;
    public GameObject goEntityContainer;


    public Tile GetTile(int y, int x) {
        if ((y + x) % 2 == 1) return null;

        if (y < 0 || x < 0) return null;
        if (y >= nMapHeight || x >= nMapWidth) return null;

        return dictTiles[y][x];
    }

    public Tile GetTileInDir(Tile tile, Dir dir, int nTileDist = 1) {
        return GetTile(tile.coords.y + nTileDist * Direction.arDirY[(int)dir], tile.coords.x + nTileDist * Direction.arDirX[(int)dir]);

    }

    public void SpawnTile(int y, int x) {
        GameObject goTileNew = GameObject.Instantiate(pfTile, goMapContainer.transform);
        Tile tileNew = goTileNew.GetComponent<Tile>();

        tileNew.InitCoords(y, x);

        dictTiles[y].Add(x, tileNew);

        MapGenerator.Get().PopulateTileInfo(tileNew.GetComponent<TileInfo>(), y, x);
    }

    public void SpawnEntity(Tile tile) {
        GameObject goEntityNew = GameObject.Instantiate(pfEntity, goEntityContainer.transform);
        Entity entNew = goEntityNew.GetComponent<Entity>();

        entNew.InitOnTile(tile);

        entNew.SetId(lstEntities.Count);
        lstEntities.Add(entNew);
    }


    public void SpawnMap() {
        Debug.LogFormat("Dict is {0}", dictTiles);
        Debug.LogFormat("Dict has {0} entries", dictTiles == null ? 0 : dictTiles.Count);
        if(dictTiles != null) {
            Debug.LogError("Map Already Exists!:  Not spawning anything new");
            return;
        }
        dictTiles = new Dictionary<int, Dictionary<int, Tile>>();

        for (int y=0; y<nMapHeight; y++) {

            dictTiles.Add(y, new Dictionary<int, Tile>());

            for(int x=0; x<nMapWidth; x++) {

                if ((y + x) % 2 == 1) continue;

                SpawnTile(y, x);


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

        if (dictTiles != null) {
            foreach (int y in dictTiles.Keys) {

                foreach (int x in dictTiles[y].Keys) {
                    GameObject.DestroyImmediate(dictTiles[y][x].gameObject);
                }

            }
            dictTiles = null;
        }
    }

    public void SpawnRandomEntity() {
        int y = Random.Range(0, nMapHeight);
        int x = Random.Range(0, nMapWidth);
        if ((y + x) % 2 == 1) {
            if (x > 0) x--;
            else x++;
        }
        SpawnEntity(GetTile(y, x));
    }

    public override void Init() {
        
        SpawnMap();

        if (lstEntities != null) {
            Debug.Log("We already have entities - no need to spawn more");
        } else {
            lstEntities = new List<Entity>();

            SpawnEntity(dictTiles[4][4]);
        }
    }
}
