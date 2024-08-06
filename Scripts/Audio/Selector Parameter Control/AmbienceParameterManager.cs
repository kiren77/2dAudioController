/* 
    never delete this comment section
    Timestamp: 2024-08-01

    - Original Intent:
        - Manage the parameters for the biome and manmade ambience sounds.
        - Update parameters based on state changes.

    - Current Role:
        - Listens to OWE_AmbienceStateManager.
        - Updates the FMOD parameters for biome and manmade ambiences.

    - Problems:
        - None identified.

    - Suggestions:
        - Ensure all required parameters are handled.
*/

using UnityEngine;

public class AmbienceParameterManager : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter biomeEmitter;
    public FMODUnity.StudioEventEmitter manmadeEmitter;

    private float biomeSelectorValue = 0.0f;
    private float manmadeSelectorValue = 0.0f;

    public float BiomeSelectorValue
    {
        get => biomeSelectorValue;
        set
        {
            biomeSelectorValue = Mathf.Clamp(value, 0f, 1f); // Normalized to 0-1
            SetBiomeParameter();
        }
    }

    public float ManmadeSelectorValue
    {
        get => manmadeSelectorValue;
        set
        {
            manmadeSelectorValue = Mathf.Clamp(value, 0f, 1f); // Normalized to 0-1
            SetManmadeParameter();
        }
    }

    private void SetBiomeParameter()
    {
        int biomeLabelIndex = Mathf.FloorToInt(biomeSelectorValue * 4); // 4 labels: Desert, Forest, Jungle, Ocean
        biomeEmitter.SetParameter("OWE Biome Selector", biomeLabelIndex);
    }

    private void SetManmadeParameter()
    {
        int manmadeLabelIndex = Mathf.FloorToInt(manmadeSelectorValue * 1); // 1 label: Market
        manmadeEmitter.SetParameter("OWE Manmade Selector", manmadeLabelIndex);
    }

    // Public methods to be called by UI buttons
    public void SetBiomeToDesert() => BiomeSelectorValue = 0f / 3f;
    public void SetBiomeToForest() => BiomeSelectorValue = 1f / 3f;
    public void SetBiomeToJungle() => BiomeSelectorValue = 2f / 3f;
    public void SetBiomeToOcean() => BiomeSelectorValue = 3f / 3f;
    public void SetManmadeToMarket() => ManmadeSelectorValue = 0f;
}
