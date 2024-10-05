using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using ContextStateGroup;
public class ContextAmbienceButton : MonoBehaviour
{
    public Button playPauseButton;
    public Image playIcon;
    public Image pauseIcon;
    public int ambienceSelectionValue;
    
    private bool isPlaying = false;

    private void Start()
    {
        if (playPauseButton != null)
        {
            playPauseButton.onClick.AddListener(OnPlayPauseButtonClicked);
        }
        else
        {
            Debug.LogError("PlayPauseButton is not assigned in the Inspector.");
        }

        UpdateUI();
    }

    private void OnPlayPauseButtonClicked()
{
    if (AmbienceManager.Instance != null)
    {
        if (isPlaying)
        {
            AmbienceManager.Instance.PauseCurrentAudio();
            isPlaying = false;
        }
        else
        {
            AmbienceManager.Instance.PlayAudioForState(ContextStateManager.Instance.GetCurrentState());
            isPlaying = true;
        }

        UpdateUI();
    }
    else
    {
        Debug.LogError("AmbienceManager instance is null.");
    }
}


    private void UpdateUI()
    {
        playIcon.gameObject.SetActive(!isPlaying);
        pauseIcon.gameObject.SetActive(isPlaying);
    }
}
