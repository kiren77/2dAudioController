using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OverlappingTwoPanels : MonoBehaviour
{
    public RectTransform panelRectTransform;
    public RectTransform gameObjectRectTransform;

    void OnRectTransformDimensionsChange()
    {
        // Get the width of the panel
        float panelWidth = panelRectTransform.rect.width;

        // Get the current position of the UI component
        Vector3 position = gameObjectRectTransform.position;

        // Set the x-position of the UI component to be equal to the x-position of the panel plus its width
        position.x =/*  panelRectTransform.position.x + */ - panelWidth / 2;

        // Set the new position of the UI component
        gameObjectRectTransform.position = position;
    }
}