using UnityEditor;
using UnityEngine;
using System.Linq;

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
        var contextStateManager = FindObjectOfType<ContextStateGroup.ContextStateManager>();
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
        var audioUIControllers = FindObjectsOfType<AudioUIController>();
        if (audioUIControllers == null || audioUIControllers.Length == 0)
        {
            Debug.LogError("No AudioUIController instances found in the scene.");
            return;
        }

        foreach (var audioUIController in audioUIControllers)
        {
            if (audioUIController.VolumeSlider == null)
            {
                Debug.LogError("AudioUIController: volumeSlider is not assigned.");
            }

            if (audioUIController.MuteButton == null)
            {
                Debug.LogError("AudioUIController: muteButton is not assigned.");
            }

            if (audioUIController.UnmuteButton == null)
            {
                Debug.LogError("AudioUIController: unmuteButton is not assigned.");
            }

            Debug.Log("AudioUIController is present and validated.");
        }
    }
}
