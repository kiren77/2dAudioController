using UnityEngine;
using UnityEngine.UI;
using ContextStateGroup;

public class ButtonToContext : MonoBehaviour
{
    public Button button;
    public ContextState targetState;

    void Start()
    {
        if (button == null)
        {
            Debug.LogError("Button is not assigned.");
            return;
        }
        
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (ContextStateManager.Instance != null)
        {
            Debug.Log($"ButtonToContext: Attempting to change state to {targetState}");
            ContextStateManager.Instance.SetCurrentState(targetState);
            Debug.Log("Current State is now: " + targetState);
        }
        else
        {
            Debug.LogError("ContextStateManager instance is null.");
        }
    }
}
