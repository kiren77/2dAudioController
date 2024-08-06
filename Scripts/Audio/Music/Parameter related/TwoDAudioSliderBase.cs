using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class TwoDSliderAudioBase : MonoBehaviour
{
    protected float xValue;
    protected float yValue;

    protected float CalculateIntensity(float min, float max, float value, float multiplier)
    {
        return Mathf.InverseLerp(min, max, value) * multiplier;
    }

    protected float CalculateLoseWin(float min, float max, float value, float multiplier, float offset)
    {
        return Mathf.InverseLerp(min, max, value) * multiplier + offset;
    }
}
