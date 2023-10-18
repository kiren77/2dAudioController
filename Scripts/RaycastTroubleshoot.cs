using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycastTroubleshoot : MonoBehaviour
{
    public GraphicRaycaster graphicRaycaster;
public RectTransform targetRectTransform;

    void Update()
    {
        // Create a PointerEventData object to hold the data for the raycast
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        // Set the position of the raycast to the current mouse position
        pointerEventData.position = Input.mousePosition;

        // Create a list to hold the results of the raycast
        List<RaycastResult> results = new List<RaycastResult>();

        // Perform the raycast using the GraphicRaycaster
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
    }
}
