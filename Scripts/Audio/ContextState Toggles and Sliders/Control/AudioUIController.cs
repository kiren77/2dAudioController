using UnityEngine;
using UnityEngine.UI;
using ContextStateGroup;
using FMODUnity;

public class AudioUIController : MonoBehaviour
{
    public Slider VolumeSlider;
    public Button MuteButton;
    public Button UnmuteButton;
    public Button PlayButton;
    public Button PauseButton;
    public bool IsMusic;

    private StudioEventEmitter currentEmitter;

    private void Start()
    {
        InitializeAudioEmitter();

        if (VolumeSlider != null)
        {
            VolumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
        }
        if (MuteButton != null)
        {
            MuteButton.onClick.AddListener(OnMuteButtonClicked);
        }
        if (UnmuteButton != null)
        {
            UnmuteButton.onClick.AddListener(OnUnmuteButtonClicked);
        }
        if (PlayButton != null)
        {
            PlayButton.onClick.AddListener(OnPlayButtonClicked);
        }
        if (PauseButton != null)
        {
            PauseButton.onClick.AddListener(OnPauseButtonClicked);
        }
    }

    private void InitializeAudioEmitter()
{
    var state = ContextStateManager.Instance.GetCurrentState();

    if (IsMusic)
    {
        currentEmitter = MusicManager.Instance.GetEmitterForState(state);
    }
    else
    {
        currentEmitter = AmbienceManager.Instance.GetEmitterForState(state);
    }

    if (currentEmitter == null)
    {
        Debug.LogError("No audio emitter found for the current state.");
    }
}


    private void OnVolumeSliderChanged(float value)
    {
        if (currentEmitter != null && currentEmitter.EventInstance.isValid())
        {
            currentEmitter.EventInstance.setVolume(value);
        }
    }

    private void OnMuteButtonClicked()
    {
        if (currentEmitter != null && currentEmitter.EventInstance.isValid())
        {
            currentEmitter.EventInstance.setVolume(0f);
        }
    }

    private void OnUnmuteButtonClicked()
    {
        if (currentEmitter != null && currentEmitter.EventInstance.isValid())
        {
            float volume = VolumeSlider != null ? VolumeSlider.value : 1.0f;
            currentEmitter.EventInstance.setVolume(volume);
        }
    }

    private void OnPlayButtonClicked()
    {
        if (currentEmitter != null)
        {
            currentEmitter.Play();
        }
    }

    private void OnPauseButtonClicked()
    {
        if (currentEmitter != null)
        {
            currentEmitter.Stop();
        }
    }
}
