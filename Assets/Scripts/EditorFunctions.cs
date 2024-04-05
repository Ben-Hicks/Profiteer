using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorFunctions : EditorWindow {

    [MenuItem("Window/Editor Functions")]
    public static void ShowWindow() {
        GetWindow<EditorFunctions>("Editor Functions");
    }

    private void OnGUI() {
        if (GUILayout.Button("Create Map")) {
            CreateMap();
        } else if (GUILayout.Button("Populate Map")) {
            PopulateMap();
        } else if (GUILayout.Button("Create Water")) {
            CreateWater();
        } else if (GUILayout.Button("Assign Biomes")) {
            AssignBiomes();
        } else if (GUILayout.Button("Assign Elevation Multis")) {
            AssignElevationMultis();
        } else if (GUILayout.Button("Assign Forest Multis")) {
            AssignForestMultis();
        } else if (GUILayout.Button("Assign City Multis")) {
            AssignCityMultis();
        } else if (GUILayout.Button("Full Map Generation")) {
            AssignCityMultis();
        } else if (GUILayout.Button("Spawn Random Entity")) {
            SpawnRandomEntity();
        } else if (GUILayout.Button("Update Random Seed")) {
            UpdateRandomSeed();
        } else if (GUILayout.Button("Print Biome Counts")) {
            PrintBiomeCounts();
        } else if (GUILayout.Button("Print Elevation Counts")) {
            PrintElevationCounts();
        } else if (GUILayout.Button("Print Forest Counts")) {
            PrintForestCounts();
        }
    }

    private void CreateMap() {
        Debug.Log("Creating Map");

        Map.Get().SpawnMap();

        Debug.Log("Map Creation Complete");
    }

    private void PopulateMap() {
        Debug.Log("Populating Map");

        MapGenerator.Get().PopulateAllTileInfos();
        Map.Get().UpdateAllTileVisuals();

        Debug.Log("Map Population Complete");
    }

    private void CreateWater() {
        Debug.Log("Creating Water");

        MapGenerator.Get().CreateWater();
        Map.Get().UpdateAllTileVisuals();

        Debug.Log("Water Creating Complete");
    }

    private void AssignBiomes() {
        Debug.Log("Assigning Biomes");

        MapGenerator.Get().AssignAllBiomes();
        Map.Get().UpdateAllTileVisuals();

        Debug.Log("Biomes Assigning Complete");
    }

    private void AssignElevationMultis() {
        Debug.Log("Assigning Elevation Multis");

        MapGenerator.Get().AssignAllElevationMultis();
        Map.Get().UpdateAllTileVisuals();

        Debug.Log("Elevation Multis Assigning Complete");
    }

    private void AssignForestMultis() {
        Debug.Log("Assigning Forest Multis");

        MapGenerator.Get().AssignAllForestMultis();
        Map.Get().UpdateAllTileVisuals();

        Debug.Log("Forest Multis Assigning Complete");
    }

    private void AssignCityMultis() {
        Debug.Log("Assigning City Multis");

        MapGenerator.Get().AssignAllCityMultis();
        Map.Get().UpdateAllTileVisuals();

        Debug.Log("City Multis Assigning Complete");
    }

    private void AssignFeatures() {
        Debug.Log("Assigning Features");

        MapGenerator.Get().AssignAllFeatures();
        Map.Get().UpdateAllTileVisuals();

        Debug.Log("Feature Assigning Complete");
    }

    private void FullMapGeneration() {
        Debug.Log("Generating Full Map");

        Map.Get().FullMapGeneration();

        Debug.Log("Full Map Generation Complete");
    }

    private void ClearMap() {
        Debug.Log("Clearing Map");

        Map.Get().ClearMap();

        Debug.Log("Map Clear Complete");
    }

    private void SpawnRandomEntity() {
        Debug.Log("Spawning Random Entity");

        Map.Get().SpawnRandomEntity();

        Debug.Log("Random Entity Spawned");
    }

    private void UpdateRandomSeed() {
        Debug.Log("Updating Random Seed");

        MapGenerator.Get().UpdateSeed();

        Debug.Log("Random Seed Update");
    }

    private void PrintBiomeCounts() {

        MapGenerator.Get().PrintBiomeCounts();
    }

    private void PrintElevationCounts() {

        MapGenerator.Get().PrintElevationCounts();
    }

    private void PrintForestCounts() {

        MapGenerator.Get().PrintForestCounts();
    }
}
