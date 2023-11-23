/* using UnityEngine;
 using UnityEngine.UI;
 using FMODUnity;
 using FMOD.Studio;
 using System.Collections;


public class AudioToggle : MonoBehaviour { 
    [SerializeField] private FMOD.Studio.VCA audioVCA;
    [SerializeField] private string vcaPath = "vca:/Master";

    // Added a button field
[SerializeField] private Button unmuteButton; // Assign the unmute button component in the Unity editor
[SerializeField] private Button muteButton; // Assign the mute button component in the Unity editor


 public Image unmuteImage;
    public Image muteImage;
    public bool isMuted = false;
    // Removed the toggle field // [SerializeField] private Toggle toggle;
    // Added a button field [SerializeField] private Button button;


    void Start()
{
    unmuteImage.gameObject.SetActive(!isMuted);
    muteImage.gameObject.SetActive(isMuted);
    string vcaPath = this.vcaPath;
    audioVCA = RuntimeManager.GetVCA(vcaPath);

    unmuteButton.onClick.AddListener(() => ToggleAudio(false)); // Pass false for the unmute button
    muteButton.onClick.AddListener(() => ToggleAudio(true)); // Pass true for the mute button
}


    public void ToggleAudio(bool isMuteButton)
{
    if (isMuteButton)
    {
        // Mute button was clicked
        isMuted = true;
    }
    else
    {
        // Unmute button was clicked
        isMuted = false;
    }

    unmuteImage.gameObject.SetActive(!isMuted);
    muteImage.gameObject.SetActive(isMuted);
    StopAllCoroutines();
    // Add a log statement to indicate that ToggleAudio is being called with the button that was clicked
    Debug.Log("ToggleAudio called with " + (isMuteButton ? "muteButton" : "unmuteButton"));
    // Changed the if-else statements to a ternary operator
    StartCoroutine(isMuted ? FadeOutVCA() : FadeInVCA());
}


    IEnumerator FadeOutVCA()
    {
        float fadeDuration = 3f;
        float elapsedTime = 0f;
        float startVolume;
        float finalVolume;
        if (audioVCA.isValid())
        {
            audioVCA.getVolume(out startVolume, out finalVolume);
            float targetVolume = 0f;
            while (elapsedTime < fadeDuration)
            {
                float volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeDuration);
                audioVCA.setVolume(volume);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            audioVCA.setVolume(targetVolume);
        }
    }

    IEnumerator FadeInVCA()
    {
        float fadeDuration = 2f;
        float elapsedTime = 0f;
        float startVolume;
        float finalVolume;
        if (audioVCA.isValid())
        {
            audioVCA.getVolume(out startVolume, out finalVolume);
            float targetVolume = 1f;
            while (elapsedTime < fadeDuration)
            {
                float volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeDuration);
                audioVCA.setVolume(volume);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            audioVCA.setVolume(targetVolume);
        }
    }
} */