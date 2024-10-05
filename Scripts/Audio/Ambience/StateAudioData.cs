using System;
using FMODUnity;
using ContextStateGroup;

[Serializable]
public class StateAmbienceData
{
    public ContextState state;
    public StudioEventEmitter ambienceEmitter;
}

[Serializable]
public class StateMusicData
{
    public ContextState state;
    public StudioEventEmitter musicEmitter;
}
