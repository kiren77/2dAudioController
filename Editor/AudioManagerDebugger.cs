using UnityEditor;
using UnityEngine;
using FMODUnity;
using ContextStateGroup;

public class AudioManagerDebugger : EditorWindow
{
    [MenuItem("Tools/Audio Manager Debugger")]
    public static void ShowWindow()
    {
        GetWindow<AudioManagerDebugger>("Audio Manager Debugger");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Check Prefab Assignments"))
        {
            CheckPrefabAssignments();
        }

        if (GUILayout.Button("Check Music Emitters"))
        {
            CheckMusicEmitters();
        }

        if (GUILayout.Button("Check Ambience Emitters"))
        {
            CheckAmbienceEmitters();
        }
    }

    private void CheckPrefabAssignments()
    {
        var uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene.");
            return;
        }

        if (uiManager.ambienceUIPrefab == null)
        {
            Debug.LogError("Ambience UI Prefab is not assigned in UIManager.");
        }
        else
        {
            Debug.Log("Ambience UI Prefab is assigned correctly.");
        }

        if (uiManager.musicUIPrefab == null)
        {
            Debug.LogError("Music UI Prefab is not assigned in UIManager.");
        }
        else
        {
            Debug.Log("Music UI Prefab is assigned correctly.");
        }

        CheckTaggedObjects("AmbienceUI");
        CheckTaggedObjects("MusicUI");
    }

    private void CheckTaggedObjects(string tag)
    {
        var taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        if (taggedObjects.Length == 0)
        {
            Debug.LogWarning($"No GameObjects found with tag {tag}.");
        }
        else
        {
            Debug.Log($"Found {taggedObjects.Length} GameObjects with tag {tag}.");
            foreach (var obj in taggedObjects)
            {
                Debug.Log($"GameObject: {obj.name}");
            }
        }
    }

    private void CheckMusicEmitters()
    {
        var musicManager = FindObjectOfType<MusicManager>();
        if (musicManager == null)
        {
            Debug.LogError("MusicManager not found in the scene.");
            return;
        }

        foreach (var mapping in musicManager.stateMusicMappings)
        {
            if (mapping.musicEmitter == null)
            {
                Debug.LogError($"Music emitter not assigned for state {mapping.state}");
            }
            else
            {
                Debug.Log($"Music emitter assigned for state {mapping.state}: {mapping.musicEmitter.gameObject.name}");
            }
        }
    }

    private void CheckAmbienceEmitters()
    {
        var ambienceManager = FindObjectOfType<AmbienceManager>();
        if (ambienceManager == null)
        {
            Debug.LogError("AmbienceManager not found in the scene.");
            return;
        }

        foreach (var mapping in ambienceManager.stateAmbienceMappings)
        {
            if (mapping.ambienceEmitter == null)
            {
                Debug.LogError($"Ambience emitter not assigned for state {mapping.state}");
            }
            else
            {
                Debug.Log($"Ambience emitter assigned for state {mapping.state}: {mapping.ambienceEmitter.gameObject.name}");
            }
        }
    }
}
