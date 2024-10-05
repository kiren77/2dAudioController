// CenterManager.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EmotionSystem;

public class CenterManager : MonoBehaviour
{
    public TextMeshProUGUI emotionText;
    public Image centerImage;

    private void OnEnable()
    {
        if (EmotionalStateManager.Instance != null)
        {
            EmotionalStateManager.Instance.OnStateChange += UpdateUI;
        }
        else
        {
            Debug.LogError("EmotionalStateManager instance is null in CenterManager.OnEnable");
        }
    }

    private void OnDisable()
    {
        if (EmotionalStateManager.Instance != null)
        {
            EmotionalStateManager.Instance.OnStateChange -= UpdateUI;
        }
    }

    private void UpdateUI(EmotionalState newState)
    {
        if (centerImage != null)
        {
            centerImage.color = GetColorForState(newState);
        }
        else
        {
            Debug.LogWarning("CenterManager: centerImage is not assigned.");
        }

        if (emotionText != null)
        {
            emotionText.text = $"Now playing: {newState}";
        }
        else
        {
            Debug.LogWarning("CenterManager: emotionText is not assigned.");
        }
    }

    private Color GetColorForState(EmotionalState state)
    {
        switch (state)
        {
            case EmotionalState.Admiration: return new Color(0.15f, 1f, 0f, 1f); // #27FF00FF
            case EmotionalState.Terror: return new Color(0f, 1f, 0.58f, 1f); // #00FF95FF
            case EmotionalState.Amazement: return new Color(0f, 0.66f, 1f, 1f); // #00A9FFFF
            case EmotionalState.Grief: return new Color(0.19f, 0.26f, 1f, 1f); // #3142FFFF
            case EmotionalState.Loathing: return new Color(0.98f, 0.23f, 0.93f, 1f); // #FA3AECFF
            case EmotionalState.Rage: return new Color(0.96f, 0.19f, 0.24f, 1f); // #F5323EFF
            case EmotionalState.Vigilance: return new Color(1f, 0.65f, 0.04f, 1f); // #FFA60BFF
            case EmotionalState.Ecstasy: return new Color(0.98f, 1f, 0f, 1f); // #FBFF00FF
            case EmotionalState.Trust: return new Color(0.45f, 1f, 0.35f, 1f); // #74FF5AFF
            case EmotionalState.Fear: return new Color(0.41f, 1f, 0.75f, 1f); // #69FFC0FF
            case EmotionalState.Surprise: return new Color(0.36f, 0.78f, 1f, 1f); // #5DC8FFFF
            case EmotionalState.Sadness: return new Color(0.44f, 0.49f, 1f, 1f); // #707CFFFF
            case EmotionalState.Disgust: return new Color(0.95f, 0.49f, 0.92f, 1f); // #F37DEBFF
            case EmotionalState.Anger: return new Color(0.99f, 0.36f, 0.4f, 1f); // #FD5C65FF
            case EmotionalState.Anticipation: return new Color(0.98f, 0.75f, 0.35f, 1f); // #FAC05BFF
            case EmotionalState.Joy: return new Color(1f, 1f, 0.67f, 1f); // #FEFFACFF
            case EmotionalState.Acceptance: return new Color(0.74f, 1f, 0.69f, 1f); // #BDFFB1FF
            case EmotionalState.Apprehension: return new Color(0.65f, 0.98f, 0.85f, 1f); // #A6FAD8FF
            case EmotionalState.Distraction: return new Color(0.53f, 0.84f, 1f, 1f); // #88D7FFFF
            case EmotionalState.Pensiveness: return new Color(0.65f, 0.68f, 0.98f, 1f); // #A6ADFAFF
            case EmotionalState.Boredom: return new Color(1f, 0.78f, 0.98f, 1f); // #FFC6FBFF
            case EmotionalState.Annoyance: return new Color(1f, 0.67f, 0.69f, 1f); // #FFACB1FF
            case EmotionalState.Interest: return new Color(0.99f, 0.83f, 0.54f, 1f); // #FDD389FF
            case EmotionalState.Serenity: return new Color(0.88f, 0.88f, 0.82f, 1f); // #E0E0D0FF
            case EmotionalState.Neutral:
            default: return Color.gray; // Neutral color
        }
    }
}
