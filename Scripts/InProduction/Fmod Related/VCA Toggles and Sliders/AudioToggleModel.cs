
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioToggleModel : MonoBehaviour
{
    public FMOD.Studio.VCA audioVCA;
    [SerializeField] private string vcaPath; // Default value, can be changed in the inspector
    public bool isMuted = false;

    void Awake() // Use Awake or Start for initialization
    {
        InitializeVCA();
    }

    private void InitializeVCA()
    {
        if (!string.IsNullOrEmpty(vcaPath))
        {
            audioVCA = RuntimeManager.GetVCA(vcaPath);
        }
        else
        {
            Debug.LogError("VCA path is not set in AudioToggleModel");
        }
    }

    public void SetVolume(float volume)
{
    if (audioVCA.isValid())
    {
        audioVCA.setVolume(volume);
    }
}

public void GetVolume(out float volume, out float finalVolume)
{
    if (audioVCA.isValid())
    {
        audioVCA.getVolume(out volume, out finalVolume);
    }
    else
    {
        volume = 0;
        finalVolume = 0;
    }
}

public bool IsValid()
{
    return audioVCA.isValid();
}
}
