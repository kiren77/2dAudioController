using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ContextStateGroup;

public class OWIMusicScript : MonoBehaviour
{
    public StudioEventEmitter OWIMusicEmitter;

    void Update()
    {
        if (ContextStateManager.Instance.GetCurrentState() == ContextState.StateOWI)
        {
            if (!OWIMusicEmitter.IsPlaying())
            {
                OWIMusicEmitter.Play();
            }
        }
        else
        {
            if (OWIMusicEmitter.IsPlaying())
            {
                OWIMusicEmitter.Stop();
            }
        }
    }
}