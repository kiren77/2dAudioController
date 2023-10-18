using UnityEngine;

//This script is supposed to update the DayNightButtons visually based on the bool isDay
public class DayNightUIManager : MonoBehaviour
{
    public DayNightButtons exteriorDayNight;
    public DayNightButtons interiorDayNight;

    public void UpdateDayNightState(bool isDay)
    {
        exteriorDayNight.switchDayNightValueChanged(isDay);
        interiorDayNight.switchDayNightValueChanged(isDay);
    }
}
