using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioView : MonoBehaviour
{
    [SerializeField] private Button unmuteButton;
    [SerializeField] private Button muteButton;
    [SerializeField] public Slider slider;
    [SerializeField] private Image unmuteImage;
    [SerializeField] private Image muteImage;
    [SerializeField] private Image volumeImage;
    [SerializeField] private Sprite NoSoundwave, oneWaveSprite, twoWaveSprite, threeWaveSprite;
    [SerializeField] private bool enableDebugging;

    public event Action<bool> OnToggleAudio;
    public event UnityEngine.Events.UnityAction<float> OnChangeVolume;

    // Reference to AudioToggleModel to access the mute state
    public AudioToggleModel audioToggleModel;

    void Start()
    {
        unmuteButton.onClick.AddListener(() => OnToggleAudio?.Invoke(false));
        muteButton.onClick.AddListener(() => OnToggleAudio?.Invoke(true));
        slider.onValueChanged.AddListener(OnChangeVolume);
    }

    public void UpdateToggleUI(bool isMuted)
    {
        unmuteImage.gameObject.SetActive(!isMuted);
        muteImage.gameObject.SetActive(isMuted);
    }

    public void UpdateSliderUI(float value)
    {
        slider.value = value;

        // Update volume image based on the slider value
        if (value == 0f)
        {
            volumeImage.sprite = NoSoundwave;
        }
        else if (value > 0f && value <= 0.33f)
        {
            volumeImage.sprite = oneWaveSprite;
        }
        else if (value > 0.33f && value <= 0.66f)
        {
            volumeImage.sprite = twoWaveSprite;
        }
        else if (value > 0.66f && value <= 1f)
        {
            volumeImage.sprite = threeWaveSprite;
        }
        else
        {
            volumeImage.sprite = NoSoundwave; // or any other desired behavior
        }

        // Enable or disable mute/unmute buttons based on the slider value
        bool buttonsInteractable = value > 0f;
        unmuteButton.interactable = buttonsInteractable;
        muteButton.interactable = buttonsInteractable;

        // Log a debug message if the buttons are deactivated and debugging is enabled
        if (!buttonsInteractable && enableDebugging)
        {
            Debug.Log("Mute/Unmute buttons are deactivated because the volume is at 0.");
        }
    }

    private void UpdateMuteButtonInteractivity(bool isSliderAtZero)
    {
        muteButton.interactable = !isSliderAtZero;
    }
}