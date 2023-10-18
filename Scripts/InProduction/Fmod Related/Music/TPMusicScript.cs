using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ContextStateGroup;

public class TPMusicScript : MonoBehaviour
{
    public StudioEventEmitter TPMusicEmitter;

    void Update()
    {
        if (ContextStateManager.Instance.GetCurrentState() == ContextState.StateTP)
        {
            if (!TPMusicEmitter.IsPlaying())
            {
                TPMusicEmitter.Play();
            }
        }
        else
        {
            if (TPMusicEmitter.IsPlaying())
            {
                TPMusicEmitter.Stop();
            }
        }
    }
}