using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycastTroubleshoot : MonoBehaviour
{
    public GraphicRaycaster graphicRaycaster; // The GraphicRaycaster component attached to the Canvas
    public EventSystem eventSystem; // The EventSystem component in the scene
    public RectTransform targetRectTransform; // The RectTransform of the UI element that you want to check

    void Update()
    {
        // Create a PointerEventData object to hold the data for the raycast
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        // Set the position of the raycast to the current mouse position
        pointerEventData.position = Input.mousePosition;

        // Create a list to hold the results of the raycast
        List<RaycastResult> results = new List<RaycastResult>();

        // Perform the raycast using the GraphicRaycaster and the PointerEventData
        graphicRaycaster.Raycast(pointerEventData, results);

        // Check if the first element in the results list is the target UI element
        if (results.Count > 0 && results[0].gameObject.GetComponent<RectTransform>() == targetRectTransform)
        {
            Debug.Log("Target UI element is not being covered by another UI element");
        }
        else
        {
            Debug.Log("Target UI element is being covered by another UI element");
        }

        // Draw a debug ray from the cursor position
        DrawDebugRay(pointerEventData.position, pointerEventData.pointerCurrentRaycast.worldNormal, Color.red, 0.0f);
    }

    // Draw a debug ray from the cursor position
    public void DrawDebugRay(Vector2 position, Vector2 direction, Color color, float duration)
    {
        Debug.DrawRay(position, direction, color, duration);
    }
}
