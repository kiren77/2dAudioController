using UnityEngine;
using UnityEngine.UI;
using ContextStateGroup;
using FMOD.Studio;

public class AudioUIController : MonoBehaviour
{
    public Slider VolumeSlider;
    public Button MuteButton;
    public Button UnmuteButton;
    public bool IsMusic;

    private VCA vca;

    private void Awake()
    {
        InitializeVCA();
    }

    private void Start()
    {
        if (VolumeSlider != null)
        {
            VolumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: VolumeSlider is not assigned.");
        }

        if (MuteButton != null)
        {
            MuteButton.onClick.AddListener(OnMuteButtonClicked);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: MuteButton is not assigned.");
        }

        if (UnmuteButton != null)
        {
            UnmuteButton.onClick.AddListener(OnUnmuteButtonClicked);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: UnmuteButton is not assigned.");
        }

        if (ContextStateManager.Instance != null)
        {
            ContextStateManager.Instance.OnContextStateChanged += HandleContextStateChanged;
        }
    }

    private void OnDisable()
    {
        if (VolumeSlider != null)
        {
            VolumeSlider.onValueChanged.RemoveListener(OnVolumeSliderChanged);
        }

        if (MuteButton != null)
        {
            MuteButton.onClick.RemoveListener(OnMuteButtonClicked);
        }

        if (UnmuteButton != null)
        {
            UnmuteButton.onClick.RemoveListener(OnUnmuteButtonClicked);
        }

        if (ContextStateManager.Instance != null)
        {
            ContextStateManager.Instance.OnContextStateChanged -= HandleContextStateChanged;
        }
    }

    private void InitializeVCA()
    {
        if (ContextStateManager.Instance == null)
        {
            Debug.LogWarning($"{gameObject.name}: ContextStateManager is not available.");
            return;
        }

        ContextState currentState = ContextStateManager.Instance.GetCurrentState();
        if (IsMusic)
        {
            vca = MusicManager.Instance.GetMusicVCA(currentState);
        }
        else
        {
            vca = AmbienceManager.Instance.GetAmbienceVCA(currentState);
        }

        if (vca.isValid())
        {
            Debug.Log($"{gameObject.name}: VCA initialized for state {currentState}");
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: VCA is not valid for state {currentState}");
        }
    }

    private void HandleContextStateChanged(ContextState newState)
    {
        InitializeVCA();
    }

    private void OnVolumeSliderChanged(float value)
    {
        if (vca.isValid())
        {
            vca.setVolume(value);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: VCA is not valid in OnVolumeSliderChanged");
        }
    }

    private void OnMuteButtonClicked()
    {
        if (vca.isValid())
        {
            vca.setVolume(0);
            if (VolumeSlider != null)
            {
                VolumeSlider.value = 0;
            }
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: VCA is not valid in OnMuteButtonClicked");
        }
    }

    private void OnUnmuteButtonClicked()
    {
        if (vca.isValid())
        {
            vca.setVolume(1);
            if (VolumeSlider != null)
            {
                VolumeSlider.value = 1;
            }
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: VCA is not valid in OnUnmuteButtonClicked");
        }
    }
}
