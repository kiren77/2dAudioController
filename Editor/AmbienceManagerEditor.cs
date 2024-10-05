using UnityEditor;
using UnityEngine;
using System.Collections.Generic; // Required for List<>
using ContextStateGroup; // Required for ContextState
using FMODUnity; // Required for StudioEventEmitter

[CustomEditor(typeof(AmbienceManager))]
public class AmbienceManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AmbienceManager manager = (AmbienceManager)target;

        if (manager.stateAmbienceMappings == null)
        {
            manager.stateAmbienceMappings = new List<StateAmbienceData>();
        }

        for (int i = 0; i < manager.stateAmbienceMappings.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");
            manager.stateAmbienceMappings[i].state = (ContextState)EditorGUILayout.EnumPopup("State", manager.stateAmbienceMappings[i].state);
            manager.stateAmbienceMappings[i].ambienceEmitter = (StudioEventEmitter)EditorGUILayout.ObjectField("Ambience Emitter", manager.stateAmbienceMappings[i].ambienceEmitter, typeof(StudioEventEmitter), true);
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Add Mapping"))
        {
            manager.stateAmbienceMappings.Add(new StateAmbienceData());
        }

        if (GUILayout.Button("Remove Mapping"))
        {
            if (manager.stateAmbienceMappings.Count > 0)
            {
                manager.stateAmbienceMappings.RemoveAt(manager.stateAmbienceMappings.Count - 1);
            }
        }

        EditorUtility.SetDirty(target);
    }
}
