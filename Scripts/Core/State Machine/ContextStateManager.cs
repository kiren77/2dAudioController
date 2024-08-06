using UnityEngine;
using System;

namespace ContextStateGroup
{
    public enum ContextState
    {
        StateMenu,
        StateOWI,
        StateOWE,
        StateUW,
        StateBT,
        StateTP,
        StateEmo
    }

    public class ContextStateManager : MonoBehaviour
    {
        public static ContextStateManager Instance { get; private set; }

        public event Action<ContextState> OnContextStateChanged;

        private ContextState currentState = ContextState.StateMenu; // Set the initial state here

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Debug.Log($"ContextStateManager: Initial state is {currentState}");

            // Notify UIManager about the initial state
            if (UIManager.Instance != null)
            {
                UIManager.Instance.UpdatePanelVisibility(currentState);
            }
        }

        public void SetCurrentState(ContextState newState)
        {
            if (currentState != newState)
            {
                currentState = newState;
                OnContextStateChanged?.Invoke(currentState);
                Debug.Log($"ContextStateManager: State changed to {currentState}");

                // Notify UIManager about the state change
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.UpdatePanelVisibility(currentState);
                }
            }
        }

        public ContextState GetCurrentState()
        {
            return currentState;
        }
    }
}
