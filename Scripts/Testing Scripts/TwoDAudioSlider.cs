using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoDAudioSlider : MonoBehaviour
{
    // Public variables that can be set in the inspector
    public Image dotAsset; // The dot image
    public RectTransform otherRectTransform; // The RectTransform component of another object (e.g., a background image)
    public List<FMODUnity.StudioEventEmitter> fmodEmitters; // A list of FMOD event emitters

    [SerializeField]
    public List<string[]> parameterNames; // A list of parameter names for each event

    [SerializeField]
    public List<Vector2[]> parameterRanges; // A list of parameter ranges for each event

    private Vector2 difference = Vector2.zero;

    private void OnMouseDown()
    {
        // Calculate and store the difference between mouse and dot positions
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        // Calculate half width and height of otherRectTransform
        Vector2 size = otherRectTransform.sizeDelta;
        float halfWidth = size.x / 2;
        float halfHeight = size.y / 2;

        // Update dot position based on current mouse position and stored difference value
        dotAsset.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;

        // Clamp new position to be within bounds of otherRectTransform
        dotAsset.transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x, -halfWidth, halfWidth),
            Mathf.Clamp(transform.localPosition.y, -halfHeight, halfHeight),
            -0.01f);

        // Loop through all FMOD event emitters
        for (int i = 0; i < fmodEmitters.Count; i++)
        {
            FMODUnity.StudioEventEmitter fmodEmitter = fmodEmitters[i];
            string[] names = parameterNames[i];
            Vector2[] ranges = parameterRanges[i];

            // Loop through all parameters for this event emitter
            for (int j = 0; j < names.Length; j++)
            {
                string name = names[j];
                Vector2 range = ranges[j];
                float value;

                // Calculate new value based on position of dot along either x or y axis
                if (j == 0)
                {
                    value = Mathf.InverseLerp(-50f, 50f, transform.localPosition.x) * (range.y - range.x) + range.x;
                }
                else
                {
                    value = Mathf.InverseLerp(-50f, 50f, transform.localPosition.y) * (range.y - range.x) + range.x;
                }

                // Set new value on corresponding parameter using FMOD Studio's API
                fmodEmitter.SetParameter(name, value);
            }
        }
    }
}