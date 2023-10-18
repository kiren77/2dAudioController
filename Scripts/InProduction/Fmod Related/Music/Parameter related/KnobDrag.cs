using UnityEngine;
using UnityEngine.EventSystems;

public class KnobDrag : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public RectTransform parentRect; // Set this to the parent square shape in the inspector

    public Vector2 dragOffset;

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, eventData.position, eventData.pressEventCamera, out dragOffset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            transform.position = parentRect.TransformPoint(localPointerPosition - dragOffset);
        }
    }
}
