using UnityEngine;
using UnityEngine.UI;
using ContextStateGroup;

public class ButtonToContext : MonoBehaviour
{
    public Button button;
    public ContextState targetState;
    private MusicManager musicManager;
    private AmbienceManager ambienceManager;

    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        ambienceManager = FindObjectOfType<AmbienceManager>();

        if (musicManager == null)
        {
            Debug.LogError("MusicManager not found in the scene.");
        }

        if (ambienceManager == null)
        {
            Debug.LogError("AmbienceManager not found in the scene.");
        }

        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (ContextStateManager.Instance != null)
        {
            ContextStateManager.Instance.SetCurrentState(targetState);
            Debug.Log("Current State is " + targetState);
        }
        else
        {
            Debug.LogError("ContextStateManager instance is null.");
        }
    }
}
