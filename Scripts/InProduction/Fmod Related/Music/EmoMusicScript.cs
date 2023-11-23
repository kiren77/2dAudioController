using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ContextStateGroup;

public class EmoMusicScript : MonoBehaviour
{
    public StudioEventEmitter emoMusicEmitter;

    void Update()
    {
        if (ContextStateManager.Instance.GetCurrentState() == ContextState.StateEmo)
        {
            if (!emoMusicEmitter.IsPlaying())
            {
                emoMusicEmitter.Play();
            }
        }
        else
        {
            if (emoMusicEmitter.IsPlaying())
            {
                emoMusicEmitter.Stop();
            }
        }
    }
}