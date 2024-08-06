using System;
using UnityEngine;

namespace ContextStateGroup
{
    public class ContextStateManagerEvents : MonoBehaviour
    {
        public static ContextStateManagerEvents Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public event Action<ContextState> OnContextStateChanged;
        public event Action<string> OnInteriorTypeChanged;
        public event Action<string> OnManMadeTypeChanged;
        public event Action<string> OnUWTypeChanged;
        public event Action<string> OnEmoTypeChanged;
        public event Action<string> OnBattleTypeChanged;
        public event Action<string> OnBiomeTypeChanged;

        public void InvokeOnContextStateChanged(ContextState newState)
        {
            OnContextStateChanged?.Invoke(newState);
        }

        public void InvokeOnInteriorTypeChanged(string newType)
        {
            OnInteriorTypeChanged?.Invoke(newType);
        }

        public void InvokeOnManMadeTypeChanged(string newType)
        {
            OnManMadeTypeChanged?.Invoke(newType);
        }

        public void InvokeOnUWTypeChanged(string newType)
        {
            OnUWTypeChanged?.Invoke(newType);
        }

        public void InvokeOnEmoTypeChanged(string newType)
        {
            OnEmoTypeChanged?.Invoke(newType);
        }

        public void InvokeOnBattleTypeChanged(string newType)
        {
            OnBattleTypeChanged?.Invoke(newType);
        }

        public void InvokeOnBiomeTypeChanged(string newType)
        {
            OnBiomeTypeChanged?.Invoke(newType);
        }
    }
}
