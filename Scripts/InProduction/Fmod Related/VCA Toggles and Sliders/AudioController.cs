using UnityEngine;
using System.Collections;
public class AudioController : MonoBehaviour
{
    // Reference these in the Unity Editor
    public AudioToggleModel audioToggleModel;
    public SliderVolumeModel sliderVolumeModel;
    public AudioView audioView;

    [SerializeField] private float fadeInDuration = 2f; // Default value, editable in Inspector
    [SerializeField] private float fadeOutDuration = 2f; // Default value, editable in Inspector


    void Start()
    {
        // Ensure audioView is assigned before using it
        if (audioView == null)
        {
            audioView = GetComponent<AudioView>();
        }

        if (audioToggleModel == null)
    {
        Debug.LogError("AudioToggleModel is not assigned in the Inspector");
        return;
    }

    if (sliderVolumeModel == null)
    {
        Debug.LogError("SliderVolumeModel is not assigned in the Inspector");
        return;
    }

    if (audioView == null)
    {
        Debug.LogError("AudioView is not assigned in the Inspector");
        return;
    }

        // Add listeners to audioView events
        audioView.OnToggleAudio += ToggleAudio;
        audioView.OnChangeVolume += new UnityEngine.Events.UnityAction<float>(ChangeVolume);

        // Update UI based on the current state of audioToggleModel and sliderVolumeModel
        audioView.UpdateToggleUI(audioToggleModel.isMuted);
        audioView.UpdateSliderUI(sliderVolumeModel.GetSliderValue());
    }

    public void ToggleAudio(bool isMuteButton)
{
    if (audioToggleModel.isMuted == isMuteButton) return;

    audioToggleModel.isMuted = isMuteButton;
    audioView.UpdateToggleUI(audioToggleModel.isMuted);

    // Call UpdateSliderUI to ensure the sprite updates correctly
    audioView.UpdateSliderUI(sliderVolumeModel.GetSliderValue());

    if (audioToggleModel.isMuted)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutVCA(fadeOutDuration));
    }
    else
    {
        StopAllCoroutines();
        StartCoroutine(FadeInVCA(fadeInDuration));
    }
}


    public void ChangeVolume(float value)
{
    sliderVolumeModel.SetSliderValue(value);
    audioToggleModel.SetVolume(value);
    audioView.UpdateSliderUI(value); // This line already updates the UI

    // Mute or unmute based on the slider value, but do not adjust volume here
    if (value == 0f && !audioToggleModel.isMuted)
    {
        audioToggleModel.isMuted = true;
        audioView.UpdateToggleUI(true);
    }
    else if (value > 0f && audioToggleModel.isMuted)
    {
        audioToggleModel.isMuted = false;
        audioView.UpdateToggleUI(false);
    }
}






  IEnumerator FadeOutVCA(float fadeDuration)
    {
        float elapsedTime = 0f;
        float startVolume;
        float finalVolume;
        if (audioToggleModel.IsValid())
        {
            audioToggleModel.GetVolume(out startVolume, out finalVolume);
            float targetVolume = 0f;
            while (elapsedTime < fadeDuration)
            {
                float volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeDuration);
                audioToggleModel.SetVolume(volume);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            audioToggleModel.SetVolume(targetVolume);
        }
    }

    IEnumerator FadeInVCA(float fadeDuration)
    {
        float elapsedTime = 0f;
        float startVolume = 0f;
        float targetVolume = sliderVolumeModel.GetSliderValue();
        if (audioToggleModel.IsValid())
        {
            while (elapsedTime < fadeDuration)
            {
                float volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeDuration);
                audioToggleModel.SetVolume(volume);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            audioToggleModel.SetVolume(targetVolume);
        }
    }

}