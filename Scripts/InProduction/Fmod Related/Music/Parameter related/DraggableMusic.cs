using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggableMusic : MonoBehaviour
{ 
    public Image dotAsset;
    public RectTransform otherRectTransform; // reference to the other game object's RectTransform component

    //DON'T FORGET TO ACTIVATE COLLIDER FOR OBJECT!
    //represents difference in position between mouse and object
    private Vector2 difference = Vector2.zero; 

    // Add four public Color variables to represent the colors for each extreme
    public Color colorTop = Color.red;
    public Color colorBottom = Color.cyan;
    public Color colorRight = Color.yellow;
    public Color colorLeft = Color.blue;

    //OnMouseDown and OnMouseDrag concern with movement of Dot
    //OnMouseDown the mouse is dragged, the difference in distance with the Dot is established
    //OnMouseDrag the position of the Dot is refreshed by subtracting the previously established difference 

    //DETECTION
    //Finding current difference in distance between mouse and dot
private void OnMouseDown()
{
    // Use Input.GetTouch if touch input is supported
    if (Input.touchSupported)
    {
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - (Vector2)transform.position;
    }
    else
    {
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }
}

private void OnMouseDrag()
{
    Vector2 size = otherRectTransform.sizeDelta;
    float halfWidth = size.x / 2;
    float halfHeight = size.y / 2;

    // Use Input.GetTouch if touch input is supported
    if (Input.touchSupported)
    {
        dotAsset.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - difference;
    }
    else
    {
        dotAsset.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    dotAsset.transform.localPosition = new Vector3(
        Mathf.Clamp(transform.localPosition.x, -halfWidth, halfWidth),
        Mathf.Clamp(transform.localPosition.y, -halfHeight, halfHeight),
        -0.01f);

    // Use Color.Lerp to gradually change the color of the otherRectTransform game object's image as the dotAsset moves across the 2D spectrum
    float xLerp = (dotAsset.transform.localPosition.x + halfWidth) / size.x;
    float yLerp = (dotAsset.transform.localPosition.y + halfHeight) / size.y;
    Color xColor = Color.Lerp(colorLeft, colorRight, xLerp);
    Color yColor = Color.Lerp(colorBottom, colorTop, yLerp);
    otherRectTransform.GetComponent<Image>().color = Color.Lerp(xColor, yColor, 0.5f);
}

}
