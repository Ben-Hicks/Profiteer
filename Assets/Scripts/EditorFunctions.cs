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
        } else if (GUILayout.Button("Clear Map")) {
            ClearMap();
        } else if (GUILayout.Button("Spawn Random Entity")) {
            SpawnRandomEntity();
        }
    }

    private void CreateMap() {
        Debug.Log("Creating Map");

        Map.Get().SpawnMap();

        Debug.Log("Map Creation Complete");
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
}
