using UnityEngine;
using FMODUnity;

public class FmodTimeOfDayController  : MonoBehaviour
{



    
    private bool lastIsDayValue;

    // Define the name of the TimeOfDay parameter
    private static readonly string TimeOfDayParameterName = "TimeOfDay";

    void Start()
    {
        // Get the initial value of isDay
        lastIsDayValue = ContextStateManager.isDay;
    }

    void Update()
    {
        // Check if the value of isDay has changed
        if (ContextStateManager.isDay != lastIsDayValue)
        {
            // Update the value of the global FMOD parameter
            SetTimeOfDay();

            // Store the new value of isDay
            lastIsDayValue = ContextStateManager.isDay;
        }
    }

    public void SetTimeOfDay()
    {
        // Get the value of isDay from the ContextStateManager instance
        bool isDay = ContextStateManager.isDay;

        // Set the value of the global FMOD parameter based on isDay
        if (isDay)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(TimeOfDayParameterName, 0f);
        }
        else
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(TimeOfDayParameterName, 1f);
        }
    }
}
