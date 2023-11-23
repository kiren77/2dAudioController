/* using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class SliderVolume : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public AudioToggle audioToggle;
    private bool prevToggleState;
    public Image volumeImage;
    public Sprite oneWaveSprite, twoWaveSprite, threeWaveSprite;
    public string audioVCAPath;
    private FMOD.Studio.VCA audioVCA;

    void Start()
    {
        audioVCA = FMODUnity.RuntimeManager.GetVCA(audioVCAPath);

        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }
    
    private void OnSliderValueChanged(float v)
    {
        audioVCA.setVolume(v);

        if (v <= 0.10f && !audioToggle.isMuted)
        {
            prevToggleState = audioToggle.isMuted;
            audioToggle.ToggleAudio(true);
            // Set the slider inactive when muteButton is clicked
            slider.interactable = false;
        }
        else if (v > 0f && audioToggle.isMuted && prevToggleState)
        {
            prevToggleState = audioToggle.isMuted;
            audioToggle.ToggleAudio(false);
            // Set the slider active when unmuteButton is clicked
            slider.interactable = true;
        }

        // Changed the switch statement to an if-else-if statement
        if (v >= 0.01f && v <= 0.33f)
        {
            volumeImage.sprite = oneWaveSprite;
        }
        else if (v > 0.33f && v <= 0.66f)
        {
            volumeImage.sprite = twoWaveSprite;
        }
        else if (v > 0.66f && v <= 1f)
        {
            volumeImage.sprite = threeWaveSprite;
        }
    }
} */