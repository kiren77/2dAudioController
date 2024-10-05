using UnityEngine;
using UnityEngine.UI;

public class OWE_AmbienceButtonHandler : MonoBehaviour
{
    public Button biomeButton;
    public Button manmadeButton;
    public string biomeType;
    public string manmadeType;

    void Start()
    {
        biomeButton.onClick.AddListener(() => OWE_AmbienceStateManager.Instance.SetBiome(biomeType));
        manmadeButton.onClick.AddListener(() => OWE_AmbienceStateManager.Instance.SetManmade(manmadeType));
    }
}
