using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonColorChanger : MonoBehaviour, IPointerClickHandler
{
    // The image component of the button
    private Image buttonImage;

    // The image component of the other gameobject
    public Image otherImage;

    // The speed of the color change
    public float speed = 1f;

    // The target color of the other image
    private Color targetColor;

    // The original color of the other image
    private Color originalColor;

    // A flag to indicate if the color change is in progress
    private bool changingColor = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the image component of the button
        buttonImage = GetComponent<Image>();

        // Get the original color of the other image
        originalColor = otherImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        // If the color change is in progress
        if (changingColor)
        {
            // Lerp the color of the other image from the original color to the target color
            otherImage.color = Color.Lerp(otherImage.color, targetColor, speed * Time.deltaTime);

            // If the color difference is small enough
            if (ColorDifference(otherImage.color, targetColor) < 0.01f)
            {
                // Set the color of the other image to the target color
                otherImage.color = targetColor;

                // Set the flag to false
                changingColor = false;
            }
        }
    }

    // A method to calculate the color difference
    private float ColorDifference(Color c1, Color c2)
    {
        // Return the sum of the absolute differences of the RGB components
        return Mathf.Abs(c1.r - c2.r) + Mathf.Abs(c1.g - c2.g) + Mathf.Abs(c1.b - c2.b);
    }

    // A method to handle the pointer click event
    public void OnPointerClick(PointerEventData eventData)
    {
        // Get the color of the button image
        Color buttonColor = buttonImage.color;

        // Set the target color of the other image to the same color as the button image, but with 50% transparency
        targetColor = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 0.5f);

        // Set the flag to true
        changingColor = true;
    }
}