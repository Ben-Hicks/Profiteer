using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : Singleton<MapGenerator> {

    public int nSeed;

    public int nTilesPerRiverSource;
    public float fRiverWetnessBomb;
    public float fLakeWetnessBomb;
    public float fOceanWetnessBomb;

    public float fRandomOffset;

    public int nValleyMaxHeight;
    public int nHillMinHeight;
    public int nMountainMinHeight;

    public int nForestMinLife;
    public int nSwampMinWetness;
    public int nSwampMaxElevation;
    public int nGreatwoodMinLife;
    public int nGreatwoodMinGoodness;
    public int nCursewoordMaxGoodness;
    public int nJungleMinTemperature;
    public int nJungleMinWetness;

    public int nTilesPerCity;
    public int nMinCityPopulation;
    public int nMinTownPopulation;

    [Range(0, 100)]
    public int nFeatureDensity; //As a (0, 100) percentage

    public Dictionary<BiomeType, int> dictBiomeCounts = new Dictionary<BiomeType, int>();
    public Dictionary<ElevationType, int> dictElevationCounts = new Dictionary<ElevationType, int>();
    public Dictionary<ForestType, int> dictForestCounts = new Dictionary<ForestType, int>();

    public List<TilePropertyGenerator> lstPropertyGenerators;

    [System.Serializable]
    public struct TilePropertyGenerator {
        public string sName;

        public float fPerlinRegionSize;
        public float fPerlinOffset;
        public float fPerlinRegionSizeSecondary;
        public float fPerlinOffsetSecondary;
        public int nMinValue;
        public int nMaxValue;

        public Gradient gradDisplay;
    }

    public List<BiomeGenerator> lstBiomeGenerators;

    [System.Serializable]
    public class BiomeIdealProperty {
        public TileInfoProperties tileinfoProperty;
        public int nIdealValue;
        public float fWeight;
        public int nDomainSize {
            get {
                return MapGenerator.Get().lstPropertyGenerators[(int)tileinfoProperty].nMaxValue
                  - MapGenerator.Get().lstPropertyGenerators[(int)tileinfoProperty].nMinValue;
            }
        }

        public float GetWeightedScore(float fValue) {
            //Could optimize this with a storing of domainsize
            return fWeight * (Mathf.Abs(fValue - nIdealValue) / (0f + nDomainSize));
        }

    }

    [System.Serializable]
    public class BiomeGenerator {
        public string sName;

        public BiomeType biometype;
        public List<BiomeIdealProperty> lstIdealProperties;

        public bool bSupportsValleys;
        public bool bSupportsHills;
        public bool bSupportsMountains;

        public float GetTotalScore(float[] arfPropertyValues) {
            float fWeightSum = 0f;
            float fScore = 0f;
            for (int i = 0; i < lstIdealProperties.Count; i++) {
                fWeightSum += lstIdealProperties[i].fWeight;
                float fWeightedScore = lstIdealProperties[i].GetWeightedScore(arfPropertyValues[(int)lstIdealProperties[i].tileinfoProperty]);
                fScore += fWeightedScore;
            }

            return fScore / fWeightSum;
        }
    }


    public Color GetPropertyColour(TileInfoProperties property, float fValue) {
        return lstPropertyGenerators[(int)property].gradDisplay.Evaluate(
            Mathf.InverseLerp(lstPropertyGenerators[(int)property].nMinValue, lstPropertyGenerators[(int)property].nMaxValue, fValue));
    }

    public float GenerateProperty(int x, int y, TilePropertyGenerator generator) {
        float fPerlin = Mathf.PerlinNoise(
            x / generator.fPerlinRegionSize + generator.fPerlinOffset + fRandomOffset,
            y / generator.fPerlinRegionSize + generator.fPerlinOffset + fRandomOffset);


        if (generator.fPerlinOffsetSecondary != 0 && generator.fPerlinRegionSizeSecondary != 0) {
            //Debug.LogFormat("Generating secondary with offset={0}, regionsize={1}", generator.fPerlinOffsetSecondary, generator.fPerlinRegionSizeSecondary);

            float fPerlinSecondary = Mathf.PerlinNoise(
            x / generator.fPerlinRegionSizeSecondary + generator.fPerlinOffsetSecondary + fRandomOffset,
            y / generator.fPerlinRegionSizeSecondary + generator.fPerlinOffsetSecondary + fRandomOffset);


            fPerlin = (fPerlin + fPerlinSecondary) / 2;

        }

        //Debug.LogFormat("({0},{1}) generated {2} of type {3}", x, y, fPerlin, generator.sName);

        return Mathf.Lerp(generator.nMinValue, generator.nMaxValue, fPerlin);
    }

    public void IncrementTypeCount<T>(T type, ref Dictionary<T, int> dictCount) {

        if (dictCount.ContainsKey(type)) {
            dictCount[type] = 1 + dictCount[type];
        } else {
            dictCount.Add(type, 1);
        }
    }

    public void PrintBiomeCounts() {
        Debug.LogFormat("Biome Counts: ");
        for (BiomeType iBiome = (BiomeType)0; iBiome < BiomeType.LENGTH; iBiome++) {
            Debug.LogFormat("\n{0}: {1}", Biomes.arsBiomeNames[(int)iBiome], dictBiomeCounts.ContainsKey(iBiome) ? dictBiomeCounts[iBiome] : 0);
        }
        Debug.Log("\n");
    }

    public void PrintElevationCounts() {
        Debug.LogFormat("Elevation Counts: ");
        for (ElevationType iElevation = (ElevationType)0; iElevation < ElevationType.LENGTH; iElevation++) {
            Debug.LogFormat("\n{0}: {1}", Biomes.arsElevationTypeNames[(int)iElevation],
                dictElevationCounts.ContainsKey(iElevation) ? dictElevationCounts[iElevation] : 0);
        }
        Debug.Log("\n");
    }

    public void PrintForestCounts() {
        Debug.LogFormat("Forest Counts: ");
        for (ForestType iForest = (ForestType)0; iForest < ForestType.LENGTH; iForest++) {
            Debug.LogFormat("\n{0}: {1}", Biomes.arsForestTypeNames[(int)iForest],
                dictForestCounts.ContainsKey(iForest) ? dictForestCounts[iForest] : 0);
        }
        Debug.Log("\n");
    }

    public void PopulateTileInfo(TileInfo tileinfo) {

        tileinfo.arfPropertyValues = new float[(int)TileInfoProperties.LENGTH];
        tileinfo.arfBiomeScores = new float[(int)BiomeType.LENGTH];

        for (int i = 0; i < (int)TileInfoProperties.LENGTH; i++) {
            tileinfo.arfPropertyValues[i] = GenerateProperty(tileinfo.tile.x, tileinfo.tile.y, lstPropertyGenerators[i]);
            if (i == (int)TileInfoProperties.Temperature) {
                //Debug.LogFormat("Column {0} generates temp {1}", tileinfo.tile.coords.x, tileinfo.arnPropertyValues[i]);
            }
        }


        AssignBiome(tileinfo);
        if (dictBiomeCounts.ContainsKey(tileinfo.biometype)) {
            dictBiomeCounts[tileinfo.biometype] = 1 + dictBiomeCounts[tileinfo.biometype];
        } else {
            dictBiomeCounts.Add(tileinfo.biometype, 1);
        }

    }

    public void PopulateAllTileInfos() {

        for (int i = 0; i < Map.Get().lstTiles.Count; i++) {
            //Debug.LogFormat("Working on column {0}", i);
            foreach (TileTerrain t in Map.Get().lstTiles[i].lstTiles) {
                PopulateTileInfo(t.tileinfo);
            }
        }
    }

    public void FillOcean(TileTerrain tileSource) {
        Queue<TileTerrain> queueTilesOnShore = new Queue<TileTerrain>();
        queueTilesOnShore.Enqueue(tileSource);

        while (queueTilesOnShore.Count > 0) {
            TileTerrain tileShore = queueTilesOnShore.Dequeue();

            if (tileShore.tileinfo.biometype == BiomeType.Ocean) continue;

            tileShore.tileinfo.watertype = WaterType.Ocean;
            tileShore.tileinfo.biometype = BiomeType.Ocean;
            tileShore.tileinfo.WetnessBomb(fOceanWetnessBomb);

            //Enqueue each tile adjacent to this one that isn't already an Ocean
            Map.Get().FoldHex1(tileShore, 0, (TileTerrain t, int y) => {
                if (t == null) return 0;
                if (t.tileinfo.biometype != BiomeType.Ocean && t.tileinfo.fElevation <= 0) {
                    queueTilesOnShore.Enqueue(t);
                }

                return 0;
            });

        }
    }

    public void FillLake(TileTerrain tileBottom) {

        float fWaterlevel = tileBottom.tileinfo.fElevation;

        //Debug.LogFormat("Creating a lake at {0} with water level {1}", tileBottom, fWaterlevel);

        List<TileTerrain> lstTilesOnShore = new List<TileTerrain>();
        lstTilesOnShore.Add(tileBottom);

        TileTerrain tileLowestShore;
        int nFailsafebreak = 75;

        while (lstTilesOnShore.Count > 0 && nFailsafebreak > 0) {
            nFailsafebreak--;

            tileLowestShore = lstTilesOnShore[0];
            for (int i = 1; i < lstTilesOnShore.Count; i++) {
                //Note - this isn't perfect, since we keep Lake/Ocean tiles in this list

                //Debug.LogFormat("Comparing lowestShore ({0} with elevation {1}) to potential tile ({2} with elevation {3})",
                //tileLowestShore.tileinfo.biometype, tileLowestShore.tileinfo.fElevation,
                //   lstTilesOnShore[i].tileinfo.biometype, lstTilesOnShore[i].tileinfo.fElevation);
                if (tileLowestShore.tileinfo.biometype == BiomeType.Lake
                    || (lstTilesOnShore[i].tileinfo.fElevation < tileLowestShore.tileinfo.fElevation
                    && lstTilesOnShore[i].tileinfo.biometype != BiomeType.Lake
                    && lstTilesOnShore[i].tileinfo.biometype != BiomeType.Ocean)) {
                    tileLowestShore = lstTilesOnShore[i];
                }
            }
            lstTilesOnShore.Remove(tileLowestShore);
            //Debug.LogFormat("lowest shore has biome type {0}", tileLowestShore.tileinfo.biometype);

            if (tileLowestShore.tileinfo.fElevation < fWaterlevel) {
                //If this tile is lower than our current water level, then we'll form a new
                // river moving in that direction
                //Debug.LogFormat("Found a new source leaving the {1}-depth lake at {0} with felevation={2}",
                //    tileLowestShore, fWaterlevel, tileLowestShore.tileinfo.fElevation);
                CreateRiver(tileLowestShore, 200);
                return;
            } else {
                //The next lowest tile height around is still higher than our lake, so we'll raise the level of the 
                // lake to this new tile and make it a lake tile
                fWaterlevel = Mathf.Max(fWaterlevel, tileLowestShore.tileinfo.fElevation);
                tileLowestShore.tileinfo.biometype = BiomeType.Lake;
                tileLowestShore.tileinfo.watertype = WaterType.Lake;
                tileLowestShore.tileinfo.WetnessBomb(fLakeWetnessBomb);

                //Debug.LogFormat("Spreading the lake to the next-lowest shore tile {0} with elevation {1}",
                //    tileLowestShore, tileLowestShore.tileinfo.fElevation);

                //Then add all adjacent tiles to be explored later
                Map.Get().FoldHex1(tileLowestShore, 0, (TileTerrain t, int y) => {
                    if (t == null) return 0;
                    if (t.tileinfo.biometype != BiomeType.Lake) {
                        lstTilesOnShore.Add(t);
                    }

                    return 0;
                });

            }

        }

        Debug.Log("Ran out of tiles, or placed enough lake tiles to failsafe-exit");

    }

    public void CreateRiver(TileTerrain tileSource, int nMaxDist) {

        while (nMaxDist >= 0) {

            if (tileSource.tileinfo.IsWater()) return;

            if (tileSource.tileinfo.fElevation <= 0) {
                FillOcean(tileSource);
                return;
            }

            //Debug.Log("TODO: add in river directions");
            tileSource.tileinfo.biometype = BiomeType.River;
            tileSource.tileinfo.watertype = WaterType.UD;
            tileSource.tileinfo.WetnessBomb(fRiverWetnessBomb);

            //Grab the lowest elevation tile adjacent to tileSource
            TileTerrain tileNextLowest = Map.Get().FoldHex1<TileTerrain>(tileSource, null, (TileTerrain t, TileTerrain tLowest) => {
                if (t == null) return tLowest;
                if (t == tileSource) return tLowest;
                if (tLowest == null) {
                    if (t.tileinfo.fElevation <= tileSource.tileinfo.fElevation) return t;
                } else {
                    if (t.tileinfo.fElevation < tLowest.tileinfo.fElevation) return t;
                }
                return tLowest;
            });

            if (tileNextLowest == null) {
                //If we don't have a clear direction to continue our river, we can make a lake out of this tile
                FillLake(tileSource);
                return;

            } else {
                //Then we have a lower tile to progress our river to
                nMaxDist--;
                tileSource = tileNextLowest;
            }
        }

        Debug.LogErrorFormat("Reached the max river length at {0} without creating a lake/ocean", tileSource);

    }


    public void CreateWater() {

        int nRivers = Mathf.FloorToInt((Map.Get().nMapHeight * Map.Get().nMapWidth) / nTilesPerRiverSource);

        //Create a few starting sources for rivers
        for (int i = 0; i < nRivers; i++) {

            TileTerrain tileRandomHighest = Map.Get().GetRandomTile();

            for (int iAttempts = 0; iAttempts < 10; iAttempts++) {
                TileTerrain tileRandom = Map.Get().GetRandomTile();

                if (tileRandom.tileinfo.fElevation > tileRandomHighest.tileinfo.fElevation) {
                    tileRandomHighest = tileRandom;
                }
            }

            //Debug.LogFormat("Spawning River at {0}", tileRandomHighest);
            CreateRiver(tileRandomHighest, 200);
        }
    }


    public void AssignBiome(TileInfo tileinfo) {
        float fBestScore = 1000f;
        BiomeType biometypeBest = BiomeType.LENGTH;

        if (tileinfo.IsWater()) {
            //Then we've already applied a water-based biome to this tile, so we can skip assigning anything else

        } else {

            for (int i = 0; i < lstBiomeGenerators.Count; i++) {
                float fBiomeScore = lstBiomeGenerators[i].GetTotalScore(tileinfo.arfPropertyValues);
                if (fBiomeScore < fBestScore) {
                    fBestScore = fBiomeScore;
                    biometypeBest = lstBiomeGenerators[i].biometype;
                }
                tileinfo.arfBiomeScores[i] = fBiomeScore;
            }

            tileinfo.biometype = biometypeBest;
        }

        IncrementTypeCount(tileinfo.biometype, ref dictBiomeCounts);
    }

    public void AssignAllBiomes() {
        for (int i = 0; i < Map.Get().lstTiles.Count; i++) {
            //Debug.LogFormat("Working on column {0}", i);
            foreach (TileTerrain t in Map.Get().lstTiles[i].lstTiles) {
                AssignBiome(t.tileinfo);
            }
        }

        FormAllRegions();
    }

    public void FormAllRegions() {

        for (int i = 0; i < Map.Get().lstTiles.Count; i++) {
            //Debug.LogFormat("Working on column {0}", i);
            foreach (TileTerrain t in Map.Get().lstTiles[i].lstTiles) {
                if (t.region == null) {
                    Region regionNew = new Region(t);

                    Map.Get().RegisterRegion(regionNew);
                }
            }
        }

    }



    public void AssignElevationMulti(TileInfo tileinfo) {

        if (tileinfo.IsWater()) return;

        if (tileinfo.fElevation <= nValleyMaxHeight && lstBiomeGenerators[(int)tileinfo.biometype].bSupportsValleys) {
            tileinfo.elevationtype = ElevationType.Valley;
        } else if (tileinfo.fElevation >= nMountainMinHeight && lstBiomeGenerators[(int)tileinfo.biometype].bSupportsMountains) {
            tileinfo.elevationtype = ElevationType.Mountains;
        } else if (tileinfo.fElevation >= nHillMinHeight && lstBiomeGenerators[(int)tileinfo.biometype].bSupportsHills) {
            tileinfo.elevationtype = ElevationType.Hills;
        } else {
            tileinfo.elevationtype = ElevationType.None;
        }

        IncrementTypeCount(tileinfo.elevationtype, ref dictElevationCounts);
    }

    public void AssignAllElevationMultis() {
        for (int i = 0; i < Map.Get().lstTiles.Count; i++) {
            //Debug.LogFormat("Working on column {0}", i);
            foreach (TileTerrain t in Map.Get().lstTiles[i].lstTiles) {
                AssignElevationMulti(t.tileinfo);
            }
        }
    }

    public void AssignForestMulti(TileInfo tileinfo) {
        if (tileinfo.IsWater()) return;

        if (tileinfo.fLife < nForestMinLife) {
            tileinfo.foresttype = ForestType.None;
        } else if (tileinfo.fLife > nGreatwoodMinLife && tileinfo.fLife > nGreatwoodMinGoodness) {
            tileinfo.foresttype = ForestType.Greatwoods;
        } else if (tileinfo.fGoodness < nCursewoordMaxGoodness) {
            tileinfo.foresttype = ForestType.Cursewoods;
        } else if (tileinfo.fWetness > nJungleMinWetness && tileinfo.fTemperature > nJungleMinTemperature) {
            tileinfo.foresttype = ForestType.Jungle;
        } else if (tileinfo.fWetness > nSwampMinWetness && tileinfo.fElevation < nSwampMaxElevation) {
            tileinfo.foresttype = ForestType.Swamp;
        } else {
            tileinfo.foresttype = ForestType.Forest;
        }

        IncrementTypeCount(tileinfo.foresttype, ref dictForestCounts);
    }

    public void AssignAllForestMultis() {
        for (int i = 0; i < Map.Get().lstTiles.Count; i++) {
            //Debug.LogFormat("Working on column {0}", i);
            foreach (TileTerrain t in Map.Get().lstTiles[i].lstTiles) {
                AssignForestMulti(t.tileinfo);
            }
        }
    }

    public void CreateCity(int iCity, TileTerrain tileCenter, int nPopulation) {

        City cityNew = new City(iCity, tileCenter, nPopulation);
        Map.Get().RegisterCity(cityNew);

        List<(TileTerrain, int)> lstTilesOnCityEdge = new List<(TileTerrain, int)>();
        lstTilesOnCityEdge.Add((tileCenter, 1));

        (TileTerrain, int) tileHighestPop;
        int nPopulationLeft = nPopulation;

        while (lstTilesOnCityEdge.Count > 0 && nPopulationLeft > 0) {

            tileHighestPop = lstTilesOnCityEdge[0];
            for (int i = 1; i < lstTilesOnCityEdge.Count; i++) {
                //Note - this isn't perfect, since we keep City tiles in this list

                if (lstTilesOnCityEdge[i].Item1.tileinfo.citytype == CityType.None
                    && lstTilesOnCityEdge[i].Item1.tileinfo.fPopulation / lstTilesOnCityEdge[i].Item2 > 
                    tileHighestPop.Item1.tileinfo.fPopulation / tileHighestPop.Item2) {
                    tileHighestPop = lstTilesOnCityEdge[i];
                }
            }
            lstTilesOnCityEdge.Remove(tileHighestPop);
            //Debug.LogFormat("lowest shore has biome type {0}", tileLowestShore.tileinfo.biometype);

            TileTerrain tileNewCity = tileHighestPop.Item1;

            /*if (tileNewCity == tileCenter) {
                tileNewCity.tileinfo.citytype = CityType.Capital;
            } else */
            if (tileNewCity.tileinfo.fPopulation > nMinCityPopulation) {
                tileNewCity.tileinfo.citytype = CityType.City;
            } else if (tileNewCity.tileinfo.fPopulation > nMinTownPopulation) {
                tileNewCity.tileinfo.citytype = CityType.Town;
            } else {
                tileNewCity.tileinfo.citytype = CityType.Village;
            }
            cityNew.AddToCity(tileNewCity);
            nPopulationLeft -= Mathf.FloorToInt(tileNewCity.tileinfo.fPopulation);

            //Then add all adjacent tiles to be explored later
            Map.Get().FoldHex1(tileNewCity, 0, (TileTerrain t, int y) => {
                if (t == null) return 0;
                if (t.tileinfo.citytype == CityType.None
                && t.tileinfo.IsWater() == false) {
                    lstTilesOnCityEdge.Add((t, tileHighestPop.Item2 + 1));
                }

                return 0;
            });

        }
    }

    public void AssignAllCityMultis() {

        int nCities = Mathf.FloorToInt((Map.Get().nMapHeight * Map.Get().nMapWidth) / nTilesPerCity);

        for (int i=0; i<nCities; i++) {

            TileTerrain tileCityCenter = Map.Get().GetRandomTile();
            if (tileCityCenter.tileinfo.IsWater()) continue;
            int fPopulation = Mathf.FloorToInt(Mathf.Pow(tileCityCenter.tileinfo.fPopulation, 2.4f));

            Debug.LogFormat("Creating city of size {0} at {1}", fPopulation, tileCityCenter);

            CreateCity(i, tileCityCenter, fPopulation);

        }

    }

    public void AssignFeature(TileInfo tileinfo) {

        FeatureType featuretypeToSet;

        if (Random.Range(0, 100) < nFeatureDensity) {

            if (tileinfo.citytype != CityType.None) {
                featuretypeToSet = Biomes.randcolCityFeatures.GetRandom();
            } else {
                featuretypeToSet = Biomes.GetBiome(tileinfo.biometype).supportedFeatureTypes.GetRandom();
            }

            Features.CreateAndSetFeature(tileinfo, featuretypeToSet);
        }
    }

    public void AssignAllFeatures() {
        for (int i = 0; i < Map.Get().lstTiles.Count; i++) {
            //Debug.LogFormat("Working on column {0}", i);
            foreach (TileTerrain t in Map.Get().lstTiles[i].lstTiles) {
                AssignFeature(t.tileinfo);
            }
        }
    }

    public void UpdateSeed() {
        Random.InitState(nSeed);
        fRandomOffset = Random.Range(0f, 10000f);

        /*
        float fSmallest = 1000f;
        float fBiggest = -1000f;

        for(float i=0f; i<30; i++) {
            for (float j = 0f; j < 30; j++) {
                float m = i + 0.2314f;
                float n = j + 0.87342f;
                float p = Mathf.PerlinNoise(m, n);
                Debug.LogFormat("Perlin({0},{1}) = {2}", m, n, p);
                if (p < fSmallest) fSmallest = p;
                if (p > fBiggest) fBiggest = p;

            }
        }
        Debug.LogFormat("Smallest={0}, Biggest ={1}", fSmallest, fBiggest);
        */
    }

    public override void Init() {

    }
}
