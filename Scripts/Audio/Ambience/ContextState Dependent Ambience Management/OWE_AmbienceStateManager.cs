using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class OWE_AmbienceStateManager : MonoBehaviour
{
    public static OWE_AmbienceStateManager Instance { get; private set; }

    private Dictionary<(string, string), StudioEventEmitter> ambienceTypeMap;
    private StudioEventEmitter currentBiomeEmitter;
    private StudioEventEmitter currentManmadeEmitter;

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

    public void InitializeAmbienceMap(List<(string biome, string manmade, StudioEventEmitter emitter)> mappings)
    {
        ambienceTypeMap = new Dictionary<(string, string), StudioEventEmitter>();
        foreach (var mapping in mappings)
        {
            ambienceTypeMap[(mapping.biome, mapping.manmade)] = mapping.emitter;
        }
    }

    public void PlayBiome(StudioEventEmitter emitter)
    {
        if (currentBiomeEmitter != null && currentBiomeEmitter.IsPlaying())
        {
            currentBiomeEmitter.Stop();
        }
        currentBiomeEmitter = emitter;
        currentBiomeEmitter.Play();
    }

    public void PlayManmade(StudioEventEmitter emitter)
    {
        if (currentManmadeEmitter != null && currentManmadeEmitter.IsPlaying())
        {
            currentManmadeEmitter.Stop();
        }
        currentManmadeEmitter = emitter;
        currentManmadeEmitter.Play();
    }

    public void StopAllAmbiences()
    {
        currentBiomeEmitter?.Stop();
        currentManmadeEmitter?.Stop();
    }

    public void HandleAmbienceChange(string biomeType, string manmadeType)
    {
        if (ambienceTypeMap.TryGetValue((biomeType, manmadeType), out var emitter))
        {
            PlayBiome(emitter);  // Adjust as necessary to distinguish between biome and manmade sounds
        }
        else
        {
            Debug.LogWarning($"No emitter found for biome: {biomeType}, manmade: {manmadeType}");
        }
    }

    // Add this method
    public StudioEventEmitter GetEmitterForType(string biomeType, string manmadeType)
    {
        return ambienceTypeMap.TryGetValue((biomeType, manmadeType), out var emitter) ? emitter : null;
    }
}
