using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using ContextStateGroup;

public class MusicManager : AudioManager
{
    public static new MusicManager Instance { get; private set; }

    public List<StateMusicData> stateMusicMappings;

    private Dictionary<ContextState, string> musicVCAPathMap;

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
        musicVCAPathMap = new Dictionary<ContextState, string>();

        foreach (var mapping in stateMusicMappings)
        {
            audioMap[mapping.state] = mapping.musicEmitter;
            musicVCAPathMap[mapping.state] = mapping.vcaPath;
        }
    }

    protected override void HandleStateChange(ContextState newState)
    {
        base.HandleStateChange(newState);
    }

    public VCA GetMusicVCA(ContextState state)
    {
        if (musicVCAPathMap.TryGetValue(state, out var vcaPath))
        {
            return RuntimeManager.GetVCA(vcaPath);
        }
        return default;
    }
}
