using UnityEngine;
using UnityEngine.UI;

public class ParameterSliderView : MonoBehaviour
{
    public RectTransform sliderArea; // Area within which the dot can move
    public RectTransform dot; // The movable dot
    public Image otherRectTransform; // Reference to the other game object's RectTransform component

    public Color colorTop = Color.red;
    public Color colorBottom = Color.cyan;
    public Color colorRight = Color.yellow;
    public Color colorLeft = Color.blue;

    public void UpdateDotPosition(Vector2 newPosition)
    {
        // Ensure the new position is within the bounds of sliderArea
        newPosition.x = Mathf.Clamp(newPosition.x, sliderArea.rect.min.x, sliderArea.rect.max.x);
        newPosition.y = Mathf.Clamp(newPosition.y, sliderArea.rect.min.y, sliderArea.rect.max.y);

        // Update the position of the dot
        dot.localPosition = newPosition;

        // Update color based on dot position
        float xLerp = (dot.localPosition.x + sliderArea.rect.width / 2) / sliderArea.rect.width;
        float yLerp = (dot.localPosition.y + sliderArea.rect.height / 2) / sliderArea.rect.height;
        Color xColor = Color.Lerp(colorLeft, colorRight, xLerp);
        Color yColor = Color.Lerp(colorBottom, colorTop, yLerp);
        otherRectTransform.color = Color.Lerp(xColor, yColor, 0.5f);
    }
}