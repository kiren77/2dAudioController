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
        private static EmotionalStateManager _instance;
        public static EmotionalStateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<EmotionalStateManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("EmotionalStateManager");
                        _instance = go.AddComponent<EmotionalStateManager>();
                        DontDestroyOnLoad(go);
                    }
                }
                return _instance;
            }
        }

        public EmotionalState CurrentState { get; private set; } = EmotionalState.Neutral;

        public event Action<EmotionalState> OnStateChange;

        [SerializeField]
        private bool enableLogging = false;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            if (ContextStateManagerEvents.Instance != null)
            {
                ContextStateManagerEvents.Instance.OnContextStateChanged += HandleContextStateChange;
            }
            else
            {
                Debug.LogError("ContextStateManagerEvents.Instance is null in EmotionalStateManager.OnEnable");
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
