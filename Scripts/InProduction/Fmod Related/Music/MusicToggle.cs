using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using System.Collections;

public class MusicToggle : MonoBehaviour
{
    public string musicVCAPath;
    private VCA musicVCA;
    public Image playImage;
    public Image muteImage;

    public bool isMuted = false;

    void Start()
    {
        musicVCA = RuntimeManager.GetVCA(musicVCAPath);
        playImage.gameObject.SetActive(!isMuted);
        muteImage.gameObject.SetActive(isMuted);    }

    public void ToggleMusic()
    {
        isMuted = !isMuted;
        playImage.gameObject.SetActive(!isMuted);
        muteImage.gameObject.SetActive(isMuted);

        StopAllCoroutines();

        if (isMuted)
        {
            StartCoroutine(FadeOutVCA());
        }
        else
        {
            StartCoroutine(FadeInVCA());
        }
    }

    IEnumerator FadeOutVCA()
    {
        float fadeDuration = 3f; // duration of the fade in seconds
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float volume = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            musicVCA.setVolume(volume);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        musicVCA.setVolume(0f);
    }

    IEnumerator FadeInVCA()
    {
        float fadeDuration = 2f; // duration of the fade in seconds
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float volume = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            musicVCA.setVolume(volume);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        musicVCA.setVolume(1f);
    }
}
