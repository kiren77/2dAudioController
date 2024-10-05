using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(ContextStatePanelManager))]
public class ContextStatePanelManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ContextStatePanelManager manager = (ContextStatePanelManager)target;

        if (manager.panelMappings == null)
        {
            manager.panelMappings = new List<StatePanelMapping>();
        }

        for (int i = 0; i < manager.panelMappings.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");
            manager.panelMappings[i].state = (ContextStateGroup.ContextState)EditorGUILayout.EnumPopup("State", manager.panelMappings[i].state);
            manager.panelMappings[i].panel = (GameObject)EditorGUILayout.ObjectField("Panel", manager.panelMappings[i].panel, typeof(GameObject), true);
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Add Mapping"))
        {
            manager.panelMappings.Add(new StatePanelMapping());
        }

        if (GUILayout.Button("Remove Mapping"))
        {
            if (manager.panelMappings.Count > 0)
            {
                manager.panelMappings.RemoveAt(manager.panelMappings.Count - 1);
            }
        }

        EditorUtility.SetDirty(target);
    }
}
