
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioView : MonoBehaviour
{
    [SerializeField] private Button unmuteButton;
    [SerializeField] private Button muteButton;
    [SerializeField] public Slider slider;
    [SerializeField] private Image volumeImage;
    [SerializeField] private Sprite noSoundwave, oneWaveSprite, twoWaveSprite, threeWaveSprite;

    public event Action<bool> OnToggleAudio;
    public event UnityEngine.Events.UnityAction<float> OnChangeVolume;

    void Start()
    {
        if (unmuteButton != null)
        {
            unmuteButton.onClick.AddListener(() => OnToggleAudio?.Invoke(false));
        }
        else
        {
            Debug.LogError("UnmuteButton is not assigned in the Inspector.");
        }

        if (muteButton != null)
        {
            muteButton.onClick.AddListener(() => OnToggleAudio?.Invoke(true));
        }
        else
        {
            Debug.LogError("MuteButton is not assigned in the Inspector.");
        }

        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnChangeVolume);
        }
        else
        {
            Debug.LogError("Slider is not assigned in the Inspector.");
        }
    }

    public void UpdateToggleUI(bool isMuted)
    {
        if (unmuteButton != null)
        {
            unmuteButton.gameObject.SetActive(isMuted);
        }

        if (muteButton != null)
        {
            muteButton.gameObject.SetActive(!isMuted);
        }
    }

    public void UpdateSliderUI(float value)
    {
        if (slider == null || volumeImage == null) return;

        slider.value = value;

        if (value == 0f)
        {
            volumeImage.sprite = noSoundwave;
        }
        else if (value > 0f && value <= 0.33f)
        {
            volumeImage.sprite = oneWaveSprite;
        }
        else if (value > 0.33f && value <= 0.66f)
        {
            volumeImage.sprite = twoWaveSprite;
        }
        else if (value > 0.66f)
        {
            volumeImage.sprite = threeWaveSprite;
        }
    }
}
