using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using ContextStateGroup;
using OWE_AmbienceControl;

public class AmbienceManager : AudioManager
{
    public static new AmbienceManager Instance { get; private set; }

    public List<StateAmbienceData> stateAmbienceMappings;
    public OWE_AmbienceParameterManager ambienceParameterManager;

    private Dictionary<ContextState, string> ambienceVCAPathMap;

    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        base.Awake();
        InitializeMappings();
    }

    private void InitializeMappings()
    {
        audioMap = new Dictionary<ContextState, StudioEventEmitter>();
        ambienceVCAPathMap = new Dictionary<ContextState, string>();

        foreach (var mapping in stateAmbienceMappings)
        {
            audioMap[mapping.state] = mapping.ambienceEmitter;
            ambienceVCAPathMap[mapping.state] = mapping.vcaPath;
        }
    }

    protected override void HandleStateChange(ContextState newState)
    {
        base.HandleStateChange(newState);
    }

    public VCA GetAmbienceVCA(ContextState state)
    {
        if (ambienceVCAPathMap.TryGetValue(state, out var vcaPath))
        {
            return RuntimeManager.GetVCA(vcaPath);
        }
        return default;
    }

    private void StartNewEmitter(StudioEventEmitter newEmitter)
    {
        currentPlayingEmitter = newEmitter;

        if (currentPlayingEmitter != null && !currentPlayingEmitter.IsPlaying())
        {
            if (ambienceParameterManager != null)
            {
                float biomeSelector = ambienceParameterManager.BiomeSelectorValue;
                float manmadeSelector = ambienceParameterManager.ManmadeSelectorValue;

                currentPlayingEmitter.SetParameter("BiomeSelector", biomeSelector);
                currentPlayingEmitter.SetParameter("ManmadeSelector", manmadeSelector);
            }

            currentPlayingEmitter.Play();
        }
    }
}
