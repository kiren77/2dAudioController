using UnityEngine;
using EmotionSystem;

public class EmotionalStateLogger : MonoBehaviour
{
    private void Start()
    {
        // Subscribe to the state change event
        EmotionalStateManager.Instance.OnStateChange += LogCurrentState;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        if (EmotionalStateManager.Instance != null)
        {
            EmotionalStateManager.Instance.OnStateChange -= LogCurrentState;
        }
    }

    private void LogCurrentState(EmotionalState newState)
    {
        // Log the current emotional state to the console
        Debug.Log("Current Emotional State: " + newState);
    }
}
