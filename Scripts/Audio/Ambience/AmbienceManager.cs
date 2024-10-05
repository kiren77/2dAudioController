using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ContextStateGroup;

public class AmbienceManager : AudioManager
{
    private static AmbienceManager _instance;
    public new static AmbienceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AmbienceManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("AmbienceManager");
                    _instance = go.AddComponent<AmbienceManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    public List<StateAmbienceData> stateAmbienceMappings;
    public OWE_AmbienceParameterManager ambienceParameterManager;

    private void Awake()
    {
        Debug.Log("AmbienceManager: Awake called.");
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        InitializeMappings();
    }

    private void Start()
    {
        Debug.Log("AmbienceManager: Start called.");
        if (stateAmbienceMappings == null || stateAmbienceMappings.Count == 0)
        {
            Debug.LogWarning("AmbienceManager: stateAmbienceMappings is empty.");
        }
    }

    private void InitializeMappings()
    {
        if (stateAmbienceMappings == null || stateAmbienceMappings.Count == 0)
        {
            Debug.LogError("AmbienceManager: stateAmbienceMappings is not set or empty.");
            return;
        }

        audioMap.Clear();
        foreach (var mapping in stateAmbienceMappings)
        {
            if (mapping == null || mapping.ambienceEmitter == null)
            {
                Debug.LogError("AmbienceManager: Found a null mapping or null ambience emitter.");
                continue;
            }

            audioMap[mapping.state] = mapping.ambienceEmitter;
        }
    }

    public void PlayCurrentStateAmbience()
{
    ContextState currentState = ContextStateManager.Instance.GetCurrentState();
    if (currentState == ContextState.StateMenu)
    {
        Debug.Log("AmbienceManager: Skipping ambience for StateMenu.");
        return;
    }

    PlayAudioForState(currentState);
}


    public void UpdateCurrentEmitterParameters()
    {
        if (currentPlayingEmitter != null && ambienceParameterManager != null)
        {
            float biomeSelector = ambienceParameterManager.BiomeSelectorValue;
            float manmadeSelector = ambienceParameterManager.ManmadeSelectorValue;

            if (currentPlayingEmitter.EventInstance.isValid())
            {
                currentPlayingEmitter.EventInstance.setParameterByName("BiomeSelector", biomeSelector);
                currentPlayingEmitter.EventInstance.setParameterByName("ManmadeSelector", manmadeSelector);
            }
        }
    }
}
