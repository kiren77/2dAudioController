using System;
using UnityEngine;
using ContextStateGroup;

namespace ContextStateGroup
{
    public class ContextStateManager : MonoBehaviour
    {
        public static ContextStateManager Instance { get; private set; }

        [SerializeField]
        private ContextState initialState = ContextState.StateMenu; // Always start with StateMenu
        private ContextState currentState;

        // Event to notify when the context state changes
        public event Action<ContextState> OnContextStateChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Set current state to the initial state defined
            currentState = initialState;
            Debug.Log($"ContextStateManager: Initial state is {currentState}");
        }

        private void Start()
        {
            // Apply the initial state explicitly
            SetCurrentState(initialState);
        }

        // Method to set a new context state
        public void SetCurrentState(ContextState newState)
{
    if (newState == currentState)
    {
        Debug.Log($"ContextStateManager: State already set to {newState}");
        return;
    }

    Debug.Log($"ContextStateManager: Changing state from {currentState} to {newState}");

    // Notify the appropriate AudioManager of the state change
    if (AudioManager.Instance != null)
    {
        AudioManager.Instance.HandleStateChange(currentState, newState);
    }

    // Update the state *before* invoking any events
    currentState = newState;

    // Log state change and invoke OnContextStateChanged event
    Debug.Log($"ContextStateManager: State changed to {currentState}");
    OnContextStateChanged?.Invoke(currentState);
}


        // Method to return the current state
        public ContextState GetCurrentState()
        {
            return currentState;
        }
    }

    // Enum representing all possible context states
    public enum ContextState
    {
        StateMenu,
        StateBT,
        StateTP,
        StateOWI,
        StateOWE,
        StateUW,
        StateEmo
    }
}
