using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ContextStateGroup;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private Dictionary<string, VCA> vcaDictionary = new Dictionary<string, VCA>();
    private Dictionary<ContextState, Dictionary<string, float>> volumeSettings = new Dictionary<ContextState, Dictionary<string, float>>();

    protected Dictionary<ContextState, StudioEventEmitter> audioMap;
    protected StudioEventEmitter currentPlayingEmitter;
    protected bool isTransitioning = false;
    protected ContextStateManager contextStateManager;

    public bool IsTransitioning => isTransitioning;

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        contextStateManager = ContextStateManager.Instance;

        if (contextStateManager != null)
        {
            contextStateManager.OnContextStateChanged += HandleStateChange;
        }
        else
        {
            Debug.LogError("ContextStateManager instance is null.");
        }
    }

    public VCA GetVCA(string vcaPath)
    {
        if (string.IsNullOrEmpty(vcaPath))
        {
            Debug.LogWarning("VCA path is null or empty.");
            return default;
        }

        if (!vcaDictionary.TryGetValue(vcaPath, out var vca))
        {
            vca = RuntimeManager.GetVCA(vcaPath);
            if (vca.isValid())
            {
                vcaDictionary[vcaPath] = vca;
            }
            else
            {
                Debug.LogWarning($"Failed to get VCA for path: {vcaPath}");
            }
        }

        return vca;
    }

    protected virtual void HandleStateChange(ContextState newState)
    {
        var newEmitter = GetAudioForState(newState);
        if (newEmitter != null)
        {
            Debug.Log($"Playing audio emitter for state {newState}");
            PlayAudio(newEmitter);
        }
        else
        {
            Debug.LogWarning($"No audio emitter found for state {newState}");
        }
    }

    protected StudioEventEmitter GetAudioForState(ContextState state)
    {
        return audioMap.TryGetValue(state, out var emitter) ? emitter : null;
    }

    public void PlayAudio(StudioEventEmitter newEmitter)
    {
        if (newEmitter != null)
        {
            if (currentPlayingEmitter != null && currentPlayingEmitter.IsPlaying())
            {
                StartCoroutine(TransitionToNewEmitter(newEmitter));
            }
            else
            {
                StartNewEmitter(newEmitter);
            }
        }
    }

    private IEnumerator TransitionToNewEmitter(StudioEventEmitter newEmitter)
    {
        isTransitioning = true;

        if (currentPlayingEmitter != null)
        {
            currentPlayingEmitter.Stop();
        }

        yield return new WaitForSeconds(0.5f);

        StartNewEmitter(newEmitter);

        isTransitioning = false;
    }

    private void StartNewEmitter(StudioEventEmitter newEmitter)
    {
        currentPlayingEmitter = newEmitter;

        if (currentPlayingEmitter != null && !currentPlayingEmitter.IsPlaying())
        {
            currentPlayingEmitter.Play();
        }
    }

    protected virtual void OnDestroy()
    {
        if (contextStateManager != null)
        {
            contextStateManager.OnContextStateChanged -= HandleStateChange;
        }
    }

    public void SaveVolumeSettings(ContextState state)
    {
        if (!volumeSettings.ContainsKey(state))
        {
            volumeSettings[state] = new Dictionary<string, float>();
        }

        foreach (var kvp in vcaDictionary)
        {
            if (kvp.Value.isValid())
            {
                kvp.Value.getVolume(out float volume);
                volumeSettings[state][kvp.Key] = volume;
                Debug.Log("Saved volume " + volume + " for state " + state + " and VCA " + kvp.Key);
            }
        }
    }

    public void LoadVolumeSettings(ContextState state)
    {
        if (volumeSettings.TryGetValue(state, out var stateVolumes))
        {
            foreach (var kvp in stateVolumes)
            {
                if (vcaDictionary.TryGetValue(kvp.Key, out var vca) && vca.isValid())
                {
                    vca.setVolume(kvp.Value);
                    Debug.Log("Loaded volume " + kvp.Value + " for state " + state + " and VCA " + kvp.Key);
                }
            }
        }
        else
        {
            Debug.LogWarning("No volume settings found for state " + state);
        }
    }
}
