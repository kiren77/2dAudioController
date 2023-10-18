using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using System.Collections;

public class ambienceToggle : MonoBehaviour
{
    public string ambienceVCAPath;
    private VCA ambienceVCA;
    public Image playImage;
    public Image muteImage;

    public bool isMuted = false;

    void Start()
    {
        ambienceVCA = RuntimeManager.GetVCA(ambienceVCAPath);
        playImage.gameObject.SetActive(!isMuted);
        muteImage.gameObject.SetActive(isMuted); 
    }

    public void Toggleambience()
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
            ambienceVCA.setVolume(volume);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ambienceVCA.setVolume(0f);
    }

    IEnumerator FadeInVCA()
    {
        float fadeDuration = 2f; // duration of the fade in seconds
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float volume = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            ambienceVCA.setVolume(volume);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ambienceVCA.setVolume(1f);
    }
}
