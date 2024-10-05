using System.Collections.Generic;
using UnityEngine;

public class AudioStateManager : MonoBehaviour
{
    private Dictionary<string, AudioState> _audioStates = new Dictionary<string, AudioState>();

    public void SetAudioState(string context, float volume, bool isMuted)
    {
        if (_audioStates.ContainsKey(context))
        {
            _audioStates[context].Volume = volume;
            _audioStates[context].IsMuted = isMuted;
        }
        else
        {
            _audioStates.Add(context, new AudioState(volume, isMuted));
        }
    }

    public AudioState GetAudioState(string context)
    {
        return _audioStates.ContainsKey(context) ? _audioStates[context] : null;
    }

    public void SaveAudioStates()
    {
        foreach (var state in _audioStates)
        {
            PlayerPrefs.SetFloat(state.Key + "_Volume", state.Value.Volume);
            PlayerPrefs.SetInt(state.Key + "_IsMuted", state.Value.IsMuted ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void LoadAudioStates()
    {
        foreach (var context in _audioStates.Keys)
        {
            float volume = PlayerPrefs.GetFloat(context + "_Volume", 1.0f);
            bool isMuted = PlayerPrefs.GetInt(context + "_IsMuted", 0) == 1;
            SetAudioState(context, volume, isMuted);
        }
    }
}
