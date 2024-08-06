using System;
using UnityEngine;
using ContextStateGroup;

namespace EmotionSystem
{
    public enum EmotionalState
    {
        Admiration,
        Terror,
        Amazement,
        Grief,
        Loathing,
        Rage,
        Vigilance,
        Ecstasy,
        Trust,
        Fear,
        Surprise,
        Sadness,
        Disgust,
        Anger,
        Anticipation,
        Joy,
        Acceptance,
        Apprehension,
        Distraction,
        Pensiveness,
        Boredom,
        Annoyance,
        Interest,
        Serenity,
        Neutral
    }

    public class EmotionalStateManager : MonoBehaviour
    {
        public static EmotionalStateManager Instance { get; private set; }
        public EmotionalState CurrentState { get; private set; } = EmotionalState.Neutral;

        public event Action<EmotionalState> OnStateChange;

        [SerializeField]
        private bool enableLogging = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            // DontDestroyOnLoad(gameObject); // Optional, if you want this to persist across scenes
        }

        private void OnEnable()
        {
            if (ContextStateManagerEvents.Instance != null)
            {
                ContextStateManagerEvents.Instance.OnContextStateChanged += HandleContextStateChange;
            }
        }

        private void OnDisable()
        {
            if (ContextStateManagerEvents.Instance != null)
            {
                ContextStateManagerEvents.Instance.OnContextStateChanged -= HandleContextStateChange;
            }
        }

        public void SetState(EmotionalState newState)
        {
            if (CurrentState != newState)
            {
                CurrentState = newState;
                OnStateChange?.Invoke(newState);

                if (enableLogging)
                {
                    Debug.Log("EmotionalState changed to: " + newState);
                }
            }
        }

        private void HandleContextStateChange(ContextState newState)
        {
            if (newState != ContextState.StateEmo)
            {
                ResetToNeutral();
            }
        }

        private void ResetToNeutral()
        {
            SetState(EmotionalState.Neutral);
        }
    }
}
