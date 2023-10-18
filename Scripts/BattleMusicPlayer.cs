using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextStateGroup;

public class BattleMusicPlayer : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter fmodEmitter; // Set this to the FMOD event emitter in the inspector

    void Start()
    {
       
    }

    public void UpdateMusic()
    {
        // Check the current context state
        if (ContextStateManager.Instance.GetCurrentState() == ContextState.StateBT)
        {
            // If the context state is StateBT, start playing the music if it's not already playing
            if (!fmodEmitter.IsPlaying())
            {
                fmodEmitter.Play();
            }
        }
        else
        {
            // If the context state is not StateBT, stop playing the music if it's currently playing
            if (fmodEmitter.IsPlaying())
            {
                fmodEmitter.Stop();
            }
        }
    }
}
