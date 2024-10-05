using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ContextStateGroup;


public class OWE_AmbienceParameterManager : MonoBehaviour
{
    public StudioEventEmitter biomeEmitter;
    public StudioEventEmitter manmadeEmitter;

    private Dictionary<string, int> biomeParameterMap = new Dictionary<string, int>
    {
        {"Desert", 0},
        {"Forest", 1},
        {"Jungle", 2},
        {"Ocean", 3}
    };

    private Dictionary<string, int> manmadeParameterMap = new Dictionary<string, int>
    {
        {"Market", 0},
        {"Village", 1}
    };

    private float biomeSelectorValue = 0.0f;
    private float manmadeSelectorValue = 0.0f;

    public float BiomeSelectorValue
    {
        get => biomeSelectorValue;
        set
        {
            biomeSelectorValue = Mathf.Clamp(value, 0f, 1f);
            SetBiomeParameter();
        }
    }

    public float ManmadeSelectorValue
    {
        get => manmadeSelectorValue;
        set
        {
            manmadeSelectorValue = Mathf.Clamp(value, 0f, 1f);
            SetManmadeParameter();
        }
    }

    private void OnEnable()
    {
        if (ContextStateManager.Instance.GetCurrentState() != ContextState.StateOWE)
        {
            return;
        }

        OWE_AmbienceStateManager.Instance.OnAmbienceStateChanged += UpdateAmbienceParameters;
    }

    private void OnDisable()
    {
        OWE_AmbienceStateManager.Instance.OnAmbienceStateChanged -= UpdateAmbienceParameters;
    }

    private void UpdateAmbienceParameters(string biomeType, string manmadeType)
    {
        if (biomeEmitter != null && biomeParameterMap.ContainsKey(biomeType))
        {
            BiomeSelectorValue = biomeParameterMap[biomeType] / 3f; // Normalize to 0-1 range
            biomeEmitter.SetParameter("OWE Biome Selector", BiomeSelectorValue);
            biomeEmitter.Play();
        }

        if (manmadeEmitter != null && manmadeParameterMap.ContainsKey(manmadeType))
        {
            ManmadeSelectorValue = manmadeParameterMap[manmadeType];
            manmadeEmitter.SetParameter("OWE Manmade Selector", ManmadeSelectorValue);
            manmadeEmitter.Play();
        }

        AmbienceManager.Instance.UpdateCurrentEmitterParameters();
    }

    private void SetBiomeParameter()
    {
        biomeEmitter?.SetParameter("OWE Biome Selector", biomeSelectorValue);
    }

    private void SetManmadeParameter()
    {
        manmadeEmitter?.SetParameter("OWE Manmade Selector", manmadeSelectorValue);
    }
}
