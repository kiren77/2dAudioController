using UnityEngine;
using System.Linq; // Include this for Linq methods like Contains
namespace CampaignSession.DayNightCycle
{
    // Delegate declaration moved outside of the DayNightManager class
    public delegate void DayNightChanged(bool isDay);

    public class DayNightManager : MonoBehaviour
    {
        public static DayNightManager Instance { get; private set; }
        public event DayNightChanged OnDayNightChanged;

        private bool _isDay = true; // Default to true

        public bool IsDay
        {
            get { return _isDay; }
            set
            {
                if (_isDay != value)
                {
                    _isDay = value;
                    OnDayNightChanged?.Invoke(_isDay);
                }
            }
        }

        public bool IsHandlerSubscribed(DayNightChanged handler)
{
    return OnDayNightChanged?.GetInvocationList().Contains(handler) ?? false;
}


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetDayNight(bool isDay)
        {
            if (_isDay != isDay)
            {
                IsDay = isDay; // This will trigger the event if there's a change
            }
        }
    }
}