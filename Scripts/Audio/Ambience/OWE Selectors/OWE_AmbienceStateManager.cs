using System.Collections.Generic;
using UnityEngine;
using ContextStateGroup;


public class OWE_AmbienceStateManager : MonoBehaviour
{
    public static OWE_AmbienceStateManager Instance { get; private set; }

    private string currentBiome;
    private string currentManmade;

    public delegate void AmbienceStateChanged(string biome, string manmade);
    public event AmbienceStateChanged OnAmbienceStateChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetBiome(string biomeType)
    {
        currentBiome = biomeType;
        OnAmbienceStateChanged?.Invoke(currentBiome, currentManmade);
    }

    public void SetManmade(string manmadeType)
    {
        currentManmade = manmadeType;
        OnAmbienceStateChanged?.Invoke(currentBiome, currentManmade);
    }

    public string GetCurrentBiome()
    {
        return currentBiome;
    }

    public string GetCurrentManmade()
    {
        return currentManmade;
    }
}
