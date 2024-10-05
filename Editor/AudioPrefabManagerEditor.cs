using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioPrefabManager))]
public class AudioPrefabManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AudioPrefabManager prefabManager = (AudioPrefabManager)target;

        // Draw individual properties
        prefabManager.ambienceUIPrefab = (GameObject)EditorGUILayout.ObjectField("Ambience UI Prefab", prefabManager.ambienceUIPrefab, typeof(GameObject), false);
        prefabManager.musicUIPrefab = (GameObject)EditorGUILayout.ObjectField("Music UI Prefab", prefabManager.musicUIPrefab, typeof(GameObject), false);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(prefabManager);
        }
    }
}
