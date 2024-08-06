using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class PrefabValidator : EditorWindow
{
    [MenuItem("Tools/Prefab Validator")]
    public static void ShowWindow()
    {
        GetWindow<PrefabValidator>("Prefab Validator");
    }

    private GameObject ambienceUIPrefab;
    private GameObject musicUIPrefab;

    private void OnGUI()
    {
        GUILayout.Label("Prefab Validator", EditorStyles.boldLabel);

        ambienceUIPrefab = (GameObject)EditorGUILayout.ObjectField("Ambience UI Prefab", ambienceUIPrefab, typeof(GameObject), false);
        musicUIPrefab = (GameObject)EditorGUILayout.ObjectField("Music UI Prefab", musicUIPrefab, typeof(GameObject), false);

        if (GUILayout.Button("Validate Prefabs"))
        {
            ValidatePrefabs();
        }
    }

    private void ValidatePrefabs()
    {
        List<string> errors = new List<string>();

        // Check if the required tags exist
        if (!IsTagExists("AmbienceUI"))
        {
            errors.Add("Tag 'AmbienceUI' does not exist.");
        }

        if (!IsTagExists("MusicUI"))
        {
            errors.Add("Tag 'MusicUI' does not exist.");
        }

        // Check if the prefabs are assigned
        if (ambienceUIPrefab == null)
        {
            errors.Add("Ambience UI Prefab is not assigned.");
        }

        if (musicUIPrefab == null)
        {
            errors.Add("Music UI Prefab is not assigned.");
        }

        // Check if the prefabs are instantiated in the scene
        if (GameObject.FindWithTag("AmbienceUI") == null)
        {
            errors.Add("No GameObject with the tag 'AmbienceUI' found in the scene.");
        }

        if (GameObject.FindWithTag("MusicUI") == null)
        {
            errors.Add("No GameObject with the tag 'MusicUI' found in the scene.");
        }

        if (errors.Count == 0)
        {
            Debug.Log("All checks passed successfully.");
        }
        else
        {
            foreach (var error in errors)
            {
                Debug.LogError(error);
            }
        }
    }

    private bool IsTagExists(string tag)
    {
        foreach (var t in UnityEditorInternal.InternalEditorUtility.tags)
        {
            if (t.Equals(tag))
            {
                return true;
            }
        }
        return false;
    }
}
