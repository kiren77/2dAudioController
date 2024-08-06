
using UnityEngine;
using FMODUnity;

public class FMODParameterControl
{
    private StudioEventEmitter fmodEmitter;

    public FMODParameterControl(StudioEventEmitter emitter)
    {
        fmodEmitter = emitter;
    }

    public void SetParameter(string parameterName, float value)
    {
        fmodEmitter.SetParameter(parameterName, value);
    }
}