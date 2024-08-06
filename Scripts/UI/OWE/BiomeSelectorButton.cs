using UnityEngine;
using UnityEngine.UI;

public class BiomeSelectorButton : MonoBehaviour
{
    public AmbienceParameterManager ambienceParameterManager;

    // Add functionality to handle button clicks
    public void OnBiomeButtonClick(string biome)
    {
        switch (biome)
        {
            case "Desert":
                ambienceParameterManager.SetBiomeToDesert();
                break;
            case "Forest":
                ambienceParameterManager.SetBiomeToForest();
                break;
            case "Jungle":
                ambienceParameterManager.SetBiomeToJungle();
                break;
            case "Ocean":
                ambienceParameterManager.SetBiomeToOcean();
                break;
            default:
                Debug.LogWarning("Unknown biome type: " + biome);
                break;
        }
    }
}
