using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using ContextStateGroup;
public class ContextMusicButton : MonoBehaviour
{
    public Button playPauseButton;
    public Image playIcon;
    public Image pauseIcon;
    public int musicSelectionValue;
    
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
    if (MusicManager.Instance != null)
    {
        if (isPlaying)
        {
            MusicManager.Instance.PauseCurrentAudio();
            isPlaying = false;
        }
        else
        {
            MusicManager.Instance.PlayAudioForState(ContextStateManager.Instance.GetCurrentState());
            isPlaying = true;
        }

        UpdateUI();
    }
    else
    {
        Debug.LogError("MusicManager instance is null.");
    }
}


    private void UpdateUI()
    {
        playIcon.gameObject.SetActive(!isPlaying);
        pauseIcon.gameObject.SetActive(isPlaying);
    }
}
