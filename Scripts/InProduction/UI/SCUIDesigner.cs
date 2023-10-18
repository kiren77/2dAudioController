using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCUIDesigner : MonoBehaviour
{
    // SCUIDesigner variables
    public float enlargeHover = 1.5f;
    public float topButtonScale = 2f;
    public float rotationSpeed = 5f;
    public float growSpeed = 10f;
    public float shrinkSpeed = 10f;
    public AnimationCurve growEase;
    public AnimationCurve shrinkEase;
    public float buttonScale = 1f;
    public float scaleFactor = 1f;

    public void ArrangeButtons(List<Button> buttons)
    {
        // Arrange buttons in a semi-circle using prefab assets
        // ...
    }

    public void RotateClickedToTop(Transform transform, List<Button> buttons, int lastPressedButtonIndex)
    {
        // Rotate clicked button to top
        // ...
        
        float targetAngle = lastPressedButtonIndex * (360f / buttons.Count);
        float currentAngle = transform.localEulerAngles.z;
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);
        transform.localEulerAngles = new Vector3(0, 0, currentAngle);
    }

    public void ScaleTopButton(Button button)
    {
        // Scale top button
        button.transform.localScale = Vector3.one * buttonScale * topButtonScale;
    }

    public void ResetScale(Button button)
    {
        // Reset scale of button
        button.transform.localScale = Vector3.one * buttonScale;
    }

    public void ScaleHoveredButton(List<Button> buttons, int hoveredButtonIndex, int lastPressedButtonIndex)
    {
        // Scale hovered button
        if (hoveredButtonIndex != -1 && hoveredButtonIndex != lastPressedButtonIndex)
        {
            Vector3 targetScale = Vector3.one * enlargeHover;
            Vector3 currentScale = buttons[hoveredButtonIndex].transform.localScale;
            currentScale.x += growEase.Evaluate(Time.deltaTime * growSpeed) * (targetScale.x - currentScale.x);
            currentScale.y += growEase.Evaluate(Time.deltaTime * growSpeed) * (targetScale.y - currentScale.y);
            currentScale.z += growEase.Evaluate(Time.deltaTime * growSpeed) * (targetScale.z - currentScale.z);
            buttons[hoveredButtonIndex].transform.localScale = currentScale;
        }
        else if (hoveredButtonIndex == -1)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (i != lastPressedButtonIndex)
                {
                    Vector3 targetScale = Vector3.one * buttonScale;
                    Vector3 currentScale = buttons[i].transform.localScale;
                    currentScale.x += shrinkEase.Evaluate(Time.deltaTime * shrinkSpeed) * (targetScale.x - currentScale.x);
                    currentScale.y += shrinkEase.Evaluate(Time.deltaTime * shrinkSpeed) * (targetScale.y - currentScale.y);
                    currentScale.z += shrinkEase.Evaluate(Time.deltaTime * shrinkSpeed) * (targetScale.z - currentScale.z);
                    buttons[i].transform.localScale = currentScale;
                }
            }
        }
    }
}