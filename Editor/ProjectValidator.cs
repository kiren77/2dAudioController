using UnityEditor;
using UnityEngine;
using System.Linq;
using ContextStateGroup;

public class ProjectValidator : EditorWindow
{
    [MenuItem("Tools/Project Validator")]
    public static void ShowWindow()
    {
        GetWindow<ProjectValidator>("Project Validator");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Validate Project"))
        {
            ValidateProject();
        }
    }

    private void ValidateProject()
    {
        ValidateContextStateManager();
        ValidateAudioManager();
        ValidateAmbienceManager();
        ValidateMusicManager();
        ValidateAudioUIController();
    }

    private void ValidateContextStateManager()
    {
        var contextStateManager = FindObjectOfType<ContextStateManager>();
        if (contextStateManager == null)
        {
            Debug.LogError("ContextStateManager instance is missing in the scene.");
        }
        else
        {
            Debug.Log("ContextStateManager is present in the scene.");
        }
    }

    private void ValidateAudioManager()
    {
        var audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance is missing in the scene.");
            return;
        }

        Debug.Log("AudioManager is present and validated.");
    }

    private void ValidateAmbienceManager()
    {
        var ambienceManager = FindObjectOfType<AmbienceManager>();
        if (ambienceManager == null)
        {
            Debug.LogError("AmbienceManager instance is missing in the scene.");
            return;
        }

        if (ambienceManager.ambienceParameterManager == null)
        {
            Debug.LogError("AmbienceManager: ambienceParameterManager is not assigned.");
        }

        if (!ambienceManager.stateAmbienceMappings.Any())
        {
            Debug.LogWarning("AmbienceManager: stateAmbienceMappings is empty.");
        }

        Debug.Log("AmbienceManager is present and validated.");
    }

    private void ValidateMusicManager()
    {
        var musicManager = FindObjectOfType<MusicManager>();
        if (musicManager == null)
        {
            Debug.LogError("MusicManager instance is missing in the scene.");
            return;
        }

        if (!musicManager.stateMusicMappings.Any())
        {
            Debug.LogWarning("MusicManager: stateMusicMappings is empty.");
        }

        Debug.Log("MusicManager is present and validated.");
    }

    private void ValidateAudioUIController()
{
    var contextStateManager = FindObjectOfType<ContextStateManager>();
    if (contextStateManager == null)
    {
        Debug.LogError("Cannot validate AudioUIController without ContextStateManager.");
        return;
    }

    // Retrieve the current state from the ContextStateManager
    ContextState currentState = contextStateManager.GetCurrentState();

    switch (currentState)
    {
        case ContextState.StateMenu:
            Debug.Log("StateMenu: No AudioUIController expected.");
            return;

        // Add similar case checks for other states that do not require AudioUIController

        case ContextState.StateEmo:
        case ContextState.StateOWI:
        case ContextState.StateTP:
            ValidateAudioController("Music Audio UI");
            ValidateAudioController("Ambience Audio UI");
            break;

        default:
            Debug.LogWarning("Unrecognized state for validation.");
            break;
    }
}



    private void ValidateAudioController(string uiElementName)
    {
        var audioUIControllers = FindObjectsOfType<AudioUIController>();
        if (audioUIControllers == null || audioUIControllers.Length == 0)
        {
            Debug.LogError($"No AudioUIController instances found for {uiElementName} in the scene.");
            return;
        }

        foreach (var audioUIController in audioUIControllers)
        {
            var uiElement = audioUIController.transform.Find(uiElementName);
            if (uiElement == null)
            {
                Debug.LogError($"{uiElementName} is not found under AudioUIController.");
            }
        }

        Debug.Log($"{uiElementName} is validated.");
    }
}
