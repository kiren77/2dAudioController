using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ContextStateGroup;

public class MusicManager : AudioManager
{
    private static MusicManager _instance;
    public new static MusicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MusicManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("MusicManager");
                    _instance = go.AddComponent<MusicManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    public List<StateMusicData> stateMusicMappings;

    private void Awake()
    {
        Debug.Log("MusicManager: Awake called.");
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
        Debug.Log("MusicManager: Start called.");
        if (stateMusicMappings == null || stateMusicMappings.Count == 0)
        {
            Debug.LogWarning("MusicManager: stateMusicMappings is empty.");
        }
    }

    private void InitializeMappings()
    {
        if (stateMusicMappings == null || stateMusicMappings.Count == 0)
        {
            Debug.LogError("MusicManager: stateMusicMappings is not set or empty.");
            return;
        }

        audioMap.Clear();
        foreach (var mapping in stateMusicMappings)
        {
            if (mapping.musicEmitter != null)
            {
                audioMap[mapping.state] = mapping.musicEmitter;
            }
        }
    }

    public void PlayCurrentStateMusic()
{
    ContextState currentState = ContextStateManager.Instance.GetCurrentState();
    if (currentState == ContextState.StateMenu)
    {
        Debug.Log("MusicManager: Skipping music for StateMenu.");
        return;
    }

    PlayAudioForState(currentState);
}

}
