using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class SliderVolume : MonoBehaviour
{
    public StudioEventEmitter audioEvent;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;
    [SerializeField][Range(-80f, 10f)] private float busVolume;
    
    

    void Start()
    {    

        //when the slider value is changed, we add a listener "v"
        // to represent the float value
        slider.onValueChanged.AddListener((v) => {
            // v is stringified and injected into the Volume level text field
            sliderText.text = v.ToString("0.00");
            audioEvent.EventInstance.setVolume(v);
        });
    }
}
