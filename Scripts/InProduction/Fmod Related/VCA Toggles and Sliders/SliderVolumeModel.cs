using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeModel : MonoBehaviour
{
    public Slider slider; // Assign this in the Unity Editor
    public float sliderValue;

    void Awake()
    {
        //Debug.Log("Slider: " + slider); // Add this line to check the Slider component

        // Ensure the slider is assigned
        if (slider == null)
        {
            Debug.LogError("[SliderVolumeModel] Slider is not assigned in the Inspector.");
            return;
        }

        // Initialize slider value
        sliderValue = slider.value;
    }

    public void SetSliderValue(float value)
    {
        // Check if the slider is assigned before setting its value
        if (slider == null)
        {
            Debug.LogError("[SliderVolumeModel] Attempted to set value on an unassigned slider.");
            return;
        }

        sliderValue = value;
        slider.value = value;
    }

    public float GetSliderValue()
    {
        // Return the current slider value
        return sliderValue;
    }
}
