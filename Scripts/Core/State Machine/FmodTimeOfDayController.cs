using UnityEngine;
using FMODUnity;
using CampaignSession.DayNightCycle;

public class FmodTimeOfDayController : MonoBehaviour
{
    private static readonly string TimeOfDayParameterName = "TimeOfDay";

    void Start()
    {
        if (DayNightManager.Instance != null)
        {
            DayNightManager.Instance.OnDayNightChanged += HandleDayNightChange;
            SetTimeOfDay(DayNightManager.Instance.IsDay);
        }
    }

    void OnDestroy()
    {
        if (DayNightManager.Instance != null)
        {
            DayNightManager.Instance.OnDayNightChanged -= HandleDayNightChange;
        }
    }

    private void HandleDayNightChange(bool isDay)
    {
        SetTimeOfDay(isDay);
    }

    private void SetTimeOfDay(bool isDay)
    {
        float parameterValue = isDay ? 0f : 1f;
        RuntimeManager.StudioSystem.setParameterByName(TimeOfDayParameterName, parameterValue);
    }
}
