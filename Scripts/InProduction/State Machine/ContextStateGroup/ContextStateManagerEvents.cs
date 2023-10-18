using System;

namespace ContextStateGroup
{
    public class ContextStateManagerEvents
    {
        private static ContextStateManagerEvents _instance;
        public static ContextStateManagerEvents Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ContextStateManagerEvents();
                }
                return _instance;
            }
        }

        public event Action<ContextState> OnContextStateChanged;
        public event Action<string> OnBiomeTypeChanged;
        public event Action<string> OnInteriorTypeChanged;
        public event Action<string> OnManMadeTypeChanged;
        public event Action<string> OnUWTypeChanged;
        public event Action<string> OnBattleTypeChanged;
        public event Action<string> OnTransportationTypeChanged;

        public void InvokeOnContextStateChanged(ContextState state)
        {
            OnContextStateChanged?.Invoke(state);
        }

        public void InvokeOnBiomeTypeChanged(string BiomeType)
        {
            OnBiomeTypeChanged?.Invoke(BiomeType);
        }

        public void InvokeOnInteriorTypeChanged(string interiorType)
        {
            OnInteriorTypeChanged?.Invoke(interiorType);
        }

        public void InvokeOnManMadeTypeChanged(string manMadeType)
        {
            OnManMadeTypeChanged?.Invoke(manMadeType);
        }

        public void InvokeOnUWTypeChanged(string uwtype)
        {
            OnUWTypeChanged?.Invoke(uwtype);
        }

        public void InvokeOnBattleTypeChanged(string battleType)
        {
            OnBattleTypeChanged?.Invoke(battleType);
        }

        public void InvokeOnTransportationTypeChanged(string transportationType)
        {
            OnTransportationTypeChanged?.Invoke(transportationType);
        }
    }
}   