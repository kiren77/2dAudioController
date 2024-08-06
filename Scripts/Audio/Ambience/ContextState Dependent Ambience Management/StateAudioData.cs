using System;
using UnityEngine;
using FMODUnity;
using ContextStateGroup; // Ensure you include the namespace for ContextState

[Serializable]
public class StateAmbienceData
{
    public ContextState state;
    public StudioEventEmitter ambienceEmitter;
    public string vcaPath;
}

[Serializable]
public class StateMusicData
{
    public ContextState state;
    public StudioEventEmitter musicEmitter;
    public string vcaPath;
}
