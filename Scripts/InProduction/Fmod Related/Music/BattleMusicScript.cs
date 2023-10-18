using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ContextStateGroup;

public class BattleMusicScript : MonoBehaviour
{
    public StudioEventEmitter battleMusicEmitter;

    void Update()
    {
        if (ContextStateManager.Instance.GetCurrentState() == ContextState.StateBT)
        {
            if (!battleMusicEmitter.IsPlaying())
            {
                battleMusicEmitter.Play();
            }
        }
        else
        {
            if (battleMusicEmitter.IsPlaying())
            {
                battleMusicEmitter.Stop();
            }
        }
    }
}