using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : Singleton<MapGenerator> {

    public int nSeed;

    public float fRandomOffset;

    public int nValleyMaxHeight;
    public int nHillMinHeight;
    public int nMountainMinHeight;

    public Dictionary<BiomeType, int> dictBiomeCounts = new Dictionary<BiomeType, int>();

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
    }

    public void AssignAllBiomes() {
        for (int i = 0; i < Map.Get().lstTiles.Count; i++) {
            //Debug.LogFormat("Working on column {0}", i);
            foreach (TileTerrain t in Map.Get().lstTiles[i].lstTiles) {
                AssignBiome(t.tileinfo);
            }
        }
    }

    public void PrintBiomeCounts() {
        Debug.Log("Printing Biome Counts:");
        for(BiomeType iBiome = (BiomeType)0; iBiome < BiomeType.LENGTH; iBiome++) {
            Debug.LogFormat("{0}: {1}", Biome.arsBiomeNames[(int)iBiome], dictBiomeCounts.ContainsKey(iBiome) ? dictBiomeCounts[iBiome] : 0);
        }
        Debug.Log("\n");
    }

    public void AssignElevationMulti(TileInfo tileInfo) {
        if(tileInfo.nElevation <= nValleyMaxHeight && lstBiomeGenerators[(int)tileInfo.biometype].bSupportsValleys) {
            tileInfo.elevationtype = ElevationType.Valley;
        } else if (tileInfo.nElevation >= nMountainMinHeight && lstBiomeGenerators[(int)tileInfo.biometype].bSupportsMountains) {
            tileInfo.elevationtype = ElevationType.Mountains;
        } else if (tileInfo.nElevation >= nHillMinHeight && lstBiomeGenerators[(int)tileInfo.biometype].bSupportsHills) {
            tileInfo.elevationtype = ElevationType.Hills;
        } else {
            tileInfo.elevationtype = ElevationType.None;
        }
    }

    public void AssignAllElevationMultis() {
        for (int i = 0; i < Map.Get().lstTiles.Count; i++) {
            //Debug.LogFormat("Working on column {0}", i);
            foreach (TileTerrain t in Map.Get().lstTiles[i].lstTiles) {
                AssignElevationMulti(t.tileinfo);
            }
        }
    }

    public void AssignAllForestMultis() {

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
