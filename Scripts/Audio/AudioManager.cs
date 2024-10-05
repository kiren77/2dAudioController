using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using ContextStateGroup;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("AudioManager");
                    _instance = go.AddComponent<AudioManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    protected Dictionary<ContextState, StudioEventEmitter> audioMap = new Dictionary<ContextState, StudioEventEmitter>();
    protected StudioEventEmitter currentPlayingEmitter;

    public void HandleStateChange(ContextState oldState, ContextState newState)
{
    // Stop the audio for the old state
    PauseAudioForState(oldState);
    
    // Play the audio for the new state
    PlayAudioForState(newState);
}

    public void PlayAudioForState(ContextState state)
{
    var emitter = GetEmitterForState(state);
    if (emitter != null)
    {
        currentPlayingEmitter = emitter;
        PlayAudio(currentPlayingEmitter);
    }
}


    protected void PauseAudioForState(ContextState state)
    {
        var emitter = GetEmitterForState(state);
        if (emitter != null && emitter.IsPlaying())
        {
            emitter.Stop();
        }
    }

    public StudioEventEmitter GetEmitterForState(ContextState state)
    {
        return audioMap.TryGetValue(state, out var emitter) ? emitter : GetSilentEmitter();
    }

    protected virtual StudioEventEmitter GetSilentEmitter()
    {
        Debug.Log("Silent emitter used for the current state.");
        return null; // Replace with appropriate silent emitter handling if needed
    }

    protected void PlayAudio(StudioEventEmitter emitter)
    {
        if (!emitter.IsPlaying())
        {
            emitter.Play();
            Debug.Log("Playing audio.");
        }
    }

    public void PauseCurrentAudio()
    {
        if (currentPlayingEmitter != null && currentPlayingEmitter.IsPlaying())
        {
            currentPlayingEmitter.Stop();
            Debug.Log("Pausing current audio.");
        }
    }

    public void SetVolume(StudioEventEmitter emitter, float volume, bool mute)
    {
        if (emitter != null && emitter.EventInstance.isValid())
        {
            emitter.EventInstance.setVolume(mute ? 0f : volume);
        }
    }
}
