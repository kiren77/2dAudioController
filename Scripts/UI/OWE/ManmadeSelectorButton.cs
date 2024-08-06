using UnityEngine;
using UnityEngine.UI;

public class ManmadeSelectorButton : MonoBehaviour
{
    public AmbienceParameterManager ambienceParameterManager;

    // Add functionality to handle button clicks
    public void OnManmadeButtonClick(string manmade)
    {
        switch (manmade)
        {
            case "Market":
                ambienceParameterManager.SetManmadeToMarket();
                break;
            default:
                Debug.LogWarning("Unknown manmade type: " + manmade);
                break;
        }
    }
}
