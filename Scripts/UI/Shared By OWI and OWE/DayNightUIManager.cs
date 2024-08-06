using UnityEngine;
using ContextStateGroup;
using CampaignSession.DayNightCycle; // Include the namespace
public class DayNightUIManager : MonoBehaviour
{
    public GameObject parentInOWI;
    public GameObject parentInOWE;
    public DayNightButtons dayNightButtons; // Single instance of DayNightButtons
    public static DayNightUIManager Instance { get; private set; }

    public Transform OWIParent; // Parent GameObject for OWI context
    public Transform OWEParent; // Parent GameObject for OWE context

    void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (dayNightButtons != null)
        {
            dayNightButtons.gameObject.SetActive(false); // Initially set inactive
        }
    }
    else
    {
        Destroy(gameObject);
    }
}


    void Update() {
    ContextState currentState = ContextStateManager.Instance.GetCurrentState();
    MoveDayNightButtonsToCurrentContext(currentState);
}
private void MoveDayNightButtonsToCurrentContext(ContextState state) {
    if (state == ContextState.StateOWI || state == ContextState.StateOWE) {
        dayNightButtons.gameObject.SetActive(true);
        Transform targetParent = state == ContextState.StateOWI ? parentInOWI.transform : parentInOWE.transform;
        dayNightButtons.transform.SetParent(targetParent, false);
        dayNightButtons.transform.localPosition = Vector3.zero; // Center in parent
    } else {
        dayNightButtons.gameObject.SetActive(false);
    }
}
    public void UpdateDayNightState(bool isDay) {
    Debug.Log("UpdateDayNightState called with isDay: " + isDay);
    DayNightManager.Instance.SetDayNight(isDay);

    // Update the state in the DayNightButtons instance
    if (dayNightButtons != null) {
        Debug.Log("Updating DayNightButtons state");
        dayNightButtons.UpdateButtonState(isDay);
    }
}

    // Call this method to move the DayNightButtons to the appropriate parent based on context
    public void MoveDayNightButtonsToContext(ContextStateGroup.ContextState context)
    {
        if (dayNightButtons != null)
        {
            if (context == ContextStateGroup.ContextState.StateOWI && OWIParent != null)
            {
                dayNightButtons.transform.SetParent(OWIParent, false);
            }
            else if (context == ContextStateGroup.ContextState.StateOWE && OWEParent != null)
            {
                dayNightButtons.transform.SetParent(OWEParent, false);
            }
        }
    }

    // Other methods...
}