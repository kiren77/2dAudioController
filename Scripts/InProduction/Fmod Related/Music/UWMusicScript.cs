using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ContextStateGroup;

public class UWMusicScript : MonoBehaviour
{
    public StudioEventEmitter UWMusicEmitter;

    void Update()
    {
        if (ContextStateManager.Instance.GetCurrentState() == ContextState.StateUW)
        {
            if (!UWMusicEmitter.IsPlaying())
            {
                UWMusicEmitter.Play();
            }
        }
        else
        {
            if (UWMusicEmitter.IsPlaying())
            {
                UWMusicEmitter.Stop();
            }
        }
    }
}