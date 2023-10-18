using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ContextStateGroup;

public class OWEMusicScript : MonoBehaviour
{
    public StudioEventEmitter OWEMusicEmitter;

    void Update()
    {
        if (ContextStateManager.Instance.GetCurrentState() == ContextState.StateOWE)
        {
            if (!OWEMusicEmitter.IsPlaying())
            {
                OWEMusicEmitter.Play();
            }
        }
        else
        {
            if (OWEMusicEmitter.IsPlaying())
            {
                OWEMusicEmitter.Stop();
            }
        }
    }
}