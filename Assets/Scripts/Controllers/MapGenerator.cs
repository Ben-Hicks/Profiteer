using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : Singleton<MapGenerator> {

    public int nSeed;

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

    public Dictionary<BiomeType, int> dictBiomeCounts = new Dictionary<BiomeType, int>();
    public Dictionary<ElevationType, int> dictElevationCounts = new Dictionary<ElevationType, int>();
    public Dictionary<ForestType, int> dictForestCounts = new Dictionary<ForestType, int>();

    public List<TilePropertyGenerator> lstPropertyGenerators;

    [System.Serializable]
    public struct TilePropertyGenerator {
        public string sName;

        public float fPerlinRegionSize;
        public float fPerlinOffset;
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

        public float GetWeightedScore(int nValue) {
            //Could optimize this with a storing of domainsize
            return fWeight * (Mathf.Abs(nValue - nIdealValue) / (0f + nDomainSize));
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

        public float GetTotalScore(int[] arnPropertyValues) {
            float fWeightSum = 0f;
            float fScore = 0f;
            for(int i=0; i<lstIdealProperties.Count; i++) {
                fWeightSum += lstIdealProperties[i].fWeight;
                float fWeightedScore = lstIdealProperties[i].GetWeightedScore(arnPropertyValues[(int)lstIdealProperties[i].tileinfoProperty]);
                fScore += fWeightedScore;
            }

            return fScore / fWeightSum;
        }
    }
    
    
    public Color GetPropertyColour(TileInfoProperties property, int nValue) {
        return lstPropertyGenerators[(int)property].gradDisplay.Evaluate(
            Mathf.InverseLerp(lstPropertyGenerators[(int)property].nMinValue, lstPropertyGenerators[(int)property].nMaxValue, nValue));
    }

    public int GenerateProperty(int x, int y, TilePropertyGenerator generator) {
        float fPerlin = Mathf.PerlinNoise(
            x / generator.fPerlinRegionSize + generator.fPerlinOffset + fRandomOffset,
            y / generator.fPerlinRegionSize + generator.fPerlinOffset + fRandomOffset);


        //Debug.LogFormat("({0},{1}) generated {2} of type {3}", x, y, fPerlin, generator.sName);

        return (int)Mathf.Lerp(generator.nMinValue, generator.nMaxValue, fPerlin);
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
            Debug.LogFormat("\n{0}: {1}", Biome.arsBiomeNames[(int)iBiome], dictBiomeCounts.ContainsKey(iBiome) ? dictBiomeCounts[iBiome] : 0);
        }
        Debug.Log("\n");
    }

    public void PrintElevationCounts() {
        Debug.LogFormat("Elevation Counts: ");
        for (ElevationType iElevation = (ElevationType)0; iElevation < ElevationType.LENGTH; iElevation++) {
            Debug.LogFormat("\n{0}: {1}", Biome.arsElevationTypeNames[(int)iElevation],
                dictElevationCounts.ContainsKey(iElevation) ? dictElevationCounts[iElevation] : 0);
        }
        Debug.Log("\n");
    }

    public void PrintForestCounts() {
        Debug.LogFormat("Forest Counts: ");
        for (ForestType iForest = (ForestType)0; iForest < ForestType.LENGTH; iForest++) {
            Debug.LogFormat("\n{0}: {1}", Biome.arsForestTypeNames[(int)iForest],
                dictForestCounts.ContainsKey(iForest) ? dictForestCounts[iForest] : 0);
        }
        Debug.Log("\n");
    }

    public void PopulateTileInfo(TileInfo tileinfo) {

        tileinfo.arnPropertyValues = new int[(int)TileInfoProperties.LENGTH];
        tileinfo.arfBiomeScores = new float[(int)BiomeType.LENGTH];

        for(int i=0; i<(int)TileInfoProperties.LENGTH; i++) {
            tileinfo.arnPropertyValues[i] = GenerateProperty(tileinfo.tile.v3Coords.x, tileinfo.tile.v3Coords.y, lstPropertyGenerators[i]);
            if(i == (int)TileInfoProperties.Temperature) {
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

    public void CreateWater() {

    }

    public void AssignBiome(TileInfo tileinfo) {
        float fBestScore = 1000f;
        BiomeType biometypeBest = BiomeType.LENGTH;

        for(int i=0; i<lstBiomeGenerators.Count; i++) {
            float fBiomeScore = lstBiomeGenerators[i].GetTotalScore(tileinfo.arnPropertyValues);
            if(fBiomeScore < fBestScore) {
                fBestScore = fBiomeScore;
                biometypeBest = lstBiomeGenerators[i].biometype;
            }
            tileinfo.arfBiomeScores[i] = fBiomeScore;
        }
        
        tileinfo.biometype = biometypeBest;

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
                if(t.region == null){
                    Region regionNew = new Region(t);

                    Map.Get().RegisterRegion(regionNew);
                }
            }
        }

    }

    

    public void AssignElevationMulti(TileInfo tileinfo) {
        if(tileinfo.nElevation <= nValleyMaxHeight && lstBiomeGenerators[(int)tileinfo.biometype].bSupportsValleys) {
            tileinfo.elevationtype = ElevationType.Valley;
        } else if (tileinfo.nElevation >= nMountainMinHeight && lstBiomeGenerators[(int)tileinfo.biometype].bSupportsMountains) {
            tileinfo.elevationtype = ElevationType.Mountains;
        } else if (tileinfo.nElevation >= nHillMinHeight && lstBiomeGenerators[(int)tileinfo.biometype].bSupportsHills) {
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
        if(tileinfo.nLife < nForestMinLife) {
            tileinfo.foresttype = ForestType.None;
        } else if(tileinfo.nLife > nGreatwoodMinLife && tileinfo.nLife > nGreatwoodMinGoodness) {
            tileinfo.foresttype = ForestType.Greatwoods;
        } else if(tileinfo.nGoodness < nCursewoordMaxGoodness) {
            tileinfo.foresttype = ForestType.Cursewoods;
        } else if(tileinfo.nWetness > nJungleMinWetness && tileinfo.nTemperature > nJungleMinTemperature) {
            tileinfo.foresttype = ForestType.Jungle;
        } else if (tileinfo.nWetness > nSwampMinWetness && tileinfo.nElevation < nSwampMaxElevation) {
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

    public void AssignAllCityMultis() {

    }

    public void AssignAllFeatures() {

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
        UpdateSeed();
    }
}
