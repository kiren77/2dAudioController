using UnityEditor;
using UnityEngine;
using System.Collections.Generic; // Required for List<>
using ContextStateGroup; // Required for ContextState
using FMODUnity; // Required for StudioEventEmitter

[CustomEditor(typeof(MusicManager))]
public class MusicManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MusicManager manager = (MusicManager)target;

        if (manager.stateMusicMappings == null)
        {
            manager.stateMusicMappings = new List<StateMusicData>();
        }

        for (int i = 0; i < manager.stateMusicMappings.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");
            manager.stateMusicMappings[i].state = (ContextState)EditorGUILayout.EnumPopup("State", manager.stateMusicMappings[i].state);
            manager.stateMusicMappings[i].musicEmitter = (StudioEventEmitter)EditorGUILayout.ObjectField("Music Emitter", manager.stateMusicMappings[i].musicEmitter, typeof(StudioEventEmitter), true);
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Add Mapping"))
        {
            manager.stateMusicMappings.Add(new StateMusicData());
        }

        if (GUILayout.Button("Remove Mapping"))
        {
            if (manager.stateMusicMappings.Count > 0)
            {
                manager.stateMusicMappings.RemoveAt(manager.stateMusicMappings.Count - 1);
            }
        }

        EditorUtility.SetDirty(target);
    }
}
