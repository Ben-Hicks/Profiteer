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
        } else if (GUILayout.Button("Clear Map")) {
            ClearMap();
        } else if (GUILayout.Button("Spawn Random Entity")) {
            SpawnRandomEntity();
        } else if (GUILayout.Button("Update Random Seed")) {
            UpdateRandomSeed();
        } else if (GUILayout.Button("Check lstTile size")) {
            CheckLstTileSize();
        }
    }

    private void CreateMap() {
        Debug.Log("Creating Map");

        Map.Get().SpawnMap();

        Debug.Log("Map Creation Complete");
    }

    private void PopulateMap() {
        Debug.Log("Populating Map");

        Map.Get().PopulateMap();

        Debug.Log("Map Population Complete");
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

    private void CheckLstTileSize() {
        Debug.LogFormat("lstTiles = {0}", Map.Get().lstTiles);
        if (Map.Get().lstTiles != null) {
            Debug.LogFormat("lstTiles.Count = {0}", Map.Get().lstTiles.Count);
        }
    }
}
