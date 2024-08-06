using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class ParameterSliderController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public RectTransform parentRect; // Set this to the parent square shape in the inspector
    public StudioEventEmitter fmodEmitter; // Assign this in the Unity Inspector
    public ParameterSliderView sliderView; // The view representing the slider

    private Vector2 dragOffset;
    private float lastUpdate; // Track the last update time
    private float updateInterval = 0.1f; // Interval between updates in seconds

    private void Start()
    {
        // Check if fmodEmitter is assigned
        if (fmodEmitter == null)
        {
            Debug.LogError("FMOD StudioEventEmitter is not assigned in ParameterSliderController.");
            return;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, eventData.position, eventData.pressEventCamera, out dragOffset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            Vector2 newPosition = parentRect.TransformPoint(localPointerPosition - dragOffset);
            sliderView.UpdateDotPosition(newPosition);

            // Throttle parameter updates
            if (Time.time - lastUpdate > updateInterval)
            {
                fmodEmitter.SetParameter("Parameter1", newPosition.x);
                fmodEmitter.SetParameter("Parameter2", newPosition.y);
                lastUpdate = Time.time; // Update the last update time
            }
        }
    }
}
