using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : Singleton<Map> {

    public float fTileHeight;
    public float fTileWidth;

    public int nMapHeight;
    public int nMapWidth;

    public List<Entity> lstEntities;
    public List<Region> lstAllRegions;
    public Dictionary<BiomeType, List<Region>> dictRegionsByBiome;
    public List<City> lstCities;


    //[System.Serializable]
    public class Col {
        public List<TileTerrain> lstTiles;
        public int Count {
            get { return lstTiles.Count; }
        }

        public Col() {
            lstTiles = new List<TileTerrain>();
        }

        public TileTerrain this[int index] => this.lstTiles[index];
        public void Add(TileTerrain t) {
            lstTiles.Add(t);
        }
    }
    public List<Col> lstTiles;

    public Tilemap tilemapTerrain;
    public Tilemap tilemapElevation;
    public Tilemap tilemapForest;
    public Tilemap tilemapWater;
    public Tilemap tilemapCity;
    public Tilemap tilemapFeatures;

    public Tilemap tilemapHighlighting;

    public GameObject pfEntity;
    
    public GameObject goEntityContainer;



    public TileTerrain GetTile(int x, int y) {
        //Debug.LogFormat("GetTile with {0},{1}", x, y);

        if (y < 0 || x < 0) return null;
        if (y >= nMapHeight || x >= nMapWidth) return null;

        return lstTiles[x][y];
    }

    public TileTerrain GetTileInDir(TileTerrain tile, Dir dir, int nTileDist = 1) {
        for (int i=0; i<nTileDist; i++) {
            if (tile == null) return null;
            tile = tile.arAdj[(int)dir];
        }
        return tile;

    }

    public TileTerrain GetRandomTile() {
        return GetTile(Random.Range(0, nMapWidth), Random.Range(0, nMapHeight));
    }

    public delegate Y Combiner<Y>(TileTerrain t, Y y);

    public Y FoldHex1<Y>(TileTerrain tileCenter, Y yBase, Combiner<Y> combiner) {
        yBase = combiner(tileCenter, yBase);

        foreach (TileTerrain tileAdj in tileCenter.arAdj) {
            if (tileAdj != null) {
                yBase = combiner(tileAdj, yBase);
            }
        }

        return yBase;
    }

    public Y FoldHex2<Y>(TileTerrain tileCenter, Y yBase, Combiner<Y> combiner) {
        yBase = combiner(tileCenter, yBase);

        foreach(Dir dir in Direction.arAllDirs) {
            TileTerrain tileAdj = GetTileInDir(tileCenter, dir);
            if (tileAdj != null) {
                combiner(tileAdj, yBase);

                TileTerrain tileAdj2nd = GetTileInDir(tileAdj, dir);
                if (tileAdj2nd != null) {
                    combiner(tileAdj2nd, yBase);
                }

                tileAdj2nd = GetTileInDir(tileAdj, Direction.Next(dir));
                if (tileAdj2nd != null) {
                    combiner(tileAdj2nd, yBase);
                }
            }
        }
        
        return yBase;
    }

    //Note: Won't perfectly inspect all tiles if they're very close to the edge of the map
    public Y FoldHex4<Y>(TileTerrain tileCenter, Y yBase, Combiner<Y> combiner) {
        yBase = FoldHex2<Y>(tileCenter, yBase, combiner);

        foreach (Dir dir in Direction.arAllDirs) {
            TileTerrain tileRecursiveCenter = GetTileInDir(tileCenter, dir, 3);
            if (tileRecursiveCenter != null) {

                yBase = FoldHex2(tileRecursiveCenter, yBase, combiner);

                TileTerrain tileGap = GetTileInDir(GetTileInDir(tileCenter, dir), Direction.Next(dir));
                if (tileGap != null) {
                    combiner(tileGap, yBase);
                }

                tileGap = GetTileInDir(GetTileInDir(tileGap, dir), Direction.Next(dir));
                if (tileGap != null) {
                    combiner(tileGap, yBase);
                }
            }
        }

        return yBase;
    }

    public void SpawnEntity(TileTerrain tile) {
        GameObject goEntityNew = GameObject.Instantiate(pfEntity, goEntityContainer.transform);
        Entity entNew = goEntityNew.GetComponent<Entity>();

        entNew.InitOnTile(tile);

        if(lstEntities == null) lstEntities = new List<Entity>();

        entNew.SetId(lstEntities.Count);
        lstEntities.Add(entNew);
    }
    

    public void SpawnTile(int x, int y) {

        TileTerrain tileNew = new TileTerrain(this, x, y);

        lstTiles[x].Add(tileNew);

        //Set up connections to adjacents
        TileTerrain tileD = GetTile(x, y - 1);
        if (tileD != null) {
            tileD.U = tileNew;
            tileNew.D = tileD;
        }

        TileTerrain tileUL = GetTile(x - 1, x % 2 == 0 ? y : y + 1);
        if (tileUL != null) {
            tileUL.DR = tileNew;
            tileNew.UL = tileUL;
        }

        TileTerrain tileDL = GetTile(x - 1, x % 2 == 0 ? y - 1 : y);
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

        lstAllRegions = new List<Region>();
        dictRegionsByBiome = new Dictionary<BiomeType, List<Region>>();
        lstCities = new List<City>();
    }

    public void Unused() {

        /*Threader.Get().DistributeTask(TaskType.TileInfoPopulation, lstTiles,

            (List<TileTerrain> lstCol) => {
                Debug.LogFormat("Working on column {0}", lstCol[0].v3Coords.x);
                //foreach (Tile t in lstCol) {
                //    MapGenerator.Get().PopulateTileInfo(t.tileinfo);
                //}
            }

            , FinishPopulateMap);
           */
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


    public void FullMapGeneration() {
        SpawnMap();

        MapGenerator.Get().PopulateAllTileInfos();
        MapGenerator.Get().CreateWater();
        MapGenerator.Get().AssignAllBiomes();
        MapGenerator.Get().AssignAllElevationMultis();
        MapGenerator.Get().AssignAllForestMultis();
        MapGenerator.Get().AssignAllCityMultis();
        MapGenerator.Get().AssignAllFeatures();

        UpdateAllTileVisuals();
    }

    public void UpdateAllTileVisuals() {

        if (lstTiles != null) {
            foreach (Col col in lstTiles) {
                foreach (TileTerrain t in col.lstTiles) {
                    t.UpdateTileVisuals();
                }
            }
        }
    }

    public void ShowTileProprties(TileInfoProperties prop) {
        Debug.LogFormat("Showing property {0}", prop);

        if (lstTiles != null) {
            foreach (Col col in lstTiles) {
                foreach (TileTerrain t in col.lstTiles) {
                    t.DisplayProperty(prop);
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
            for(int i=0; i<lstTiles.Count; i++) {
                lstTiles[i] = null;
            }
            tilemapTerrain.ClearAllTiles();
            tilemapElevation.ClearAllTiles();
            lstTiles = null;
        }

        MapGenerator.Get().dictBiomeCounts.Clear();
        
    }

    public void RegisterRegion(Region region) {
        lstAllRegions.Add(region);

        if (dictRegionsByBiome.ContainsKey(region.biometype) == false) {
            dictRegionsByBiome.Add(region.biometype, new List<Region>());
        }

        region.sName = string.Format("{0}-{1}", region.biometype, dictRegionsByBiome[region.biometype].Count);
        dictRegionsByBiome[region.biometype].Add(region);
    }

    public void RegisterCity(City city) {
        lstCities.Add(city);
    }

    public void SpawnRandomEntity() {
        int y = Random.Range(0, nMapHeight);
        int x = Random.Range(0, nMapWidth);

        SpawnEntity(GetTile(x, y));
    }

    public override void Init() {

        MapGenerator.Get().UpdateSeed();

        FullMapGeneration();
        
        /*
        if (lstEntities != null) {
            Debug.Log("We already have entities - no need to spawn more");
        } else {
            lstEntities = new List<Entity>();

            SpawnEntity(dictTiles[4][4]);
        }
        */
    }

    public void HandleKeyboardInputs() {
        if (Input.GetKeyUp(KeyCode.Space)) {
            UpdateAllTileVisuals();
        } else if (Input.GetKeyUp(KeyCode.Alpha1)) {
            ShowTileProprties(TileInfoProperties.Elevation);
        } else if (Input.GetKeyUp(KeyCode.Alpha2)) {
            ShowTileProprties(TileInfoProperties.Wetness);
        } else if (Input.GetKeyUp(KeyCode.Alpha3)) {
            ShowTileProprties(TileInfoProperties.Temperature);
        } else if (Input.GetKeyUp(KeyCode.Alpha4)) {
            ShowTileProprties(TileInfoProperties.Life);
        } else if (Input.GetKeyUp(KeyCode.Alpha5)) {
            ShowTileProprties(TileInfoProperties.Goodness);
        } else if (Input.GetKeyUp(KeyCode.Alpha6)) {
            ShowTileProprties(TileInfoProperties.Population);
        } else if (Input.GetKeyUp(KeyCode.Alpha7)) {
            ShowTileProprties(TileInfoProperties.Rarity);
        }
    }

    public void HandleMouseInputs() {

        if (Input.GetMouseButtonUp(0)) {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int v3ClickedTile = tilemapTerrain.WorldToCell(worldPoint);

            // Try to get a tile from cell position
            TileBase tilebaseClicked = tilemapTerrain.GetTile(v3ClickedTile);

            if (tilebaseClicked != null) {
                TileTerrain tileClicked = GetTile(v3ClickedTile.y, v3ClickedTile.x);
                Debug.LogFormat("Clicked on Tile {0}", tileClicked);
                foreach(Entity ent in lstEntities) {
                    //Debug.LogFormat("Dist from {0} to {1} is {2}", tileClicked, ent.tile, TileTerrain.Dist(tileClicked, ent.tile));
                    //List<TileTerrain> lstPath = Pathing.FindPath(ent.tile, tileClicked);
                    //MapHighlighting.Get().SetAllHighlighting(lstPath);
                    ent.MoveToTile(tileClicked);

                    //MapHighlighting.Get().SetAllHighlighting(Pathing.GetTilesInMovementRange(tileClicked, 100));
                }
            } else {
                Debug.Log("Clicked on no tile");
            }
        }
    }

    public void Update() {

        HandleKeyboardInputs();
        HandleMouseInputs();

    }
}
