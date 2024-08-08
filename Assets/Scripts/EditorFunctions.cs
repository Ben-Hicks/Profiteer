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
        if (false) {

            //Map stuff
            /*} else if (GUILayout.Button("Create Map")) {
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
            } else if (GUILayout.Button("Update Random Seed")) {
                UpdateRandomSeed();
            } else if (GUILayout.Button("Print Biome Counts")) {
                PrintBiomeCounts();
            } else if (GUILayout.Button("Print Elevation Counts")) {
                PrintElevationCounts();
            } else if (GUILayout.Button("Print Forest Counts")) {
                PrintForestCounts();
                */
            //Entity Stuff
        } else if (GUILayout.Button("Spawn Random Human")) {
            SpawnRandomHuman();
        } else if (GUILayout.Button("Spawn Random Herbavore")) {
            SpawnRandomHerbavore();
        } else if (GUILayout.Button("Spawn Random Predator")) {
            SpawnRandomPredator();

            // Notification Stuff
        } else if (GUILayout.Button("Give 5 gold")) {
            GiveGold();


            //Test combat
        } else if (GUILayout.Button("Start Test Combat")) {
            CreateTestCombat();
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

    private void SpawnRandomHuman() {
        Debug.Log("Spawning Random Human");

        Map.Get().SpawnRandomEntity(Map.Get().pfEntityHuman);

        Debug.Log("Random Human Spawned");
    }

    private void SpawnRandomHerbavore() {
        Debug.Log("Spawning Random Herbavore");

        Map.Get().SpawnRandomEntity(Map.Get().pfEntityHerbivore);

        Debug.Log("Random Herbavore Spawned");
    }

    private void SpawnRandomPredator() {
        Debug.Log("Spawning Random Predator");

        Map.Get().SpawnRandomEntity(Map.Get().pfEntityPredator);

        Debug.Log("Random Predator Spawned");
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

    private void GiveGold() {

        NotificationController.Get().SpawnNotification(
            "Do you want 5 gold ore?",
            ("Yes", () => {
                EntityController.Get().entPlayer.inv.AddItem(new ItemGoldOre(5));
            }),
            ("No", () => {
                Debug.LogFormat("Guess you wanna be poor");
            })
        

            );
    }

    private void CreateTestCombat() {

        if(EntityController.Get().entPlayer == null) {
            SpawnRandomHuman();
        }

        if(EntityController.Get().lstEnemyEntities == null || EntityController.Get().lstEnemyEntities.Count == 0) {
            SpawnRandomPredator();
        }

        CombatFactory.Get().CreateCombat(new List<Entity>() { EntityController.Get().entPlayer },
            new List<Entity>() { EntityController.Get().lstEnemyEntities[0] });


    }

}


