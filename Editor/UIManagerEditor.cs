using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UIManager uiManager = (UIManager)target;

        // Draw individual properties except for panelMappings
        uiManager.ambienceUIPrefab = (GameObject)EditorGUILayout.ObjectField("Ambience UI Prefab", uiManager.ambienceUIPrefab, typeof(GameObject), false);
        uiManager.musicUIPrefab = (GameObject)EditorGUILayout.ObjectField("Music UI Prefab", uiManager.musicUIPrefab, typeof(GameObject), false);
        uiManager.enableLogging = EditorGUILayout.Toggle("Enable Logging", uiManager.enableLogging);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Panel Mappings", EditorStyles.boldLabel);

        for (int i = 0; i < uiManager.panelMappings.Count; i++)
        {
            var mapping = uiManager.panelMappings[i];

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("State: " + mapping.state.ToString(), EditorStyles.boldLabel);
            mapping.panel = (GameObject)EditorGUILayout.ObjectField("Panel", mapping.panel, typeof(GameObject), true);
            mapping.ambiencePrefabParent = (GameObject)EditorGUILayout.ObjectField("Ambience Prefab Parent", mapping.ambiencePrefabParent, typeof(GameObject), true);
            mapping.musicPrefabParent = (GameObject)EditorGUILayout.ObjectField("Music Prefab Parent", mapping.musicPrefabParent, typeof(GameObject), true);
            EditorGUILayout.EndVertical();

            // Update the list with the modified mapping
            uiManager.panelMappings[i] = mapping;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(uiManager);
        }
    }
}
