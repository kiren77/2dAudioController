using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EmotionSystem;

[RequireComponent(typeof(Image))]
public class EmotionButton : MonoBehaviour, IPointerClickHandler
{
    public EmotionalState associatedState;
    public float alphaThreshold = 0.1f;  // Threshold for determining clickable areas
    public bool enableTroubleshooting = false;  // Toggle for troubleshooting mode

    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();

        // Set alpha hit test threshold for precise click detection
        buttonImage.alphaHitTestMinimumThreshold = alphaThreshold;

        // Apply troubleshooting overlay if enabled
        if (enableTroubleshooting && buttonImage.sprite != null)
        {
            ApplyTroubleshootingOverlay();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnButtonClick();
        }
    }

    private void OnButtonClick()
    {
        if (EmotionalStateManager.Instance != null)
        {
            EmotionalStateManager.Instance.SetState(associatedState);
        }
        else
        {
            Debug.LogError("EmotionalStateManager.Instance is null.");
        }
    }

    private void ApplyTroubleshootingOverlay()
    {
        Texture2D originalTexture = buttonImage.sprite.texture;
        if (originalTexture == null) return;

        Texture2D tempTexture = new Texture2D(originalTexture.width, originalTexture.height);
        Color[] originalPixels = originalTexture.GetPixels();
        Color buttonColor = buttonImage.color;

        for (int i = 0; i < originalPixels.Length; i++)
        {
            Color pixel = originalPixels[i];
            // Set the troubleshooting overlay color while preserving the original alpha
            originalPixels[i] = new Color(buttonColor.r, buttonColor.g, buttonColor.b, pixel.a);
        }

        tempTexture.SetPixels(originalPixels);
        tempTexture.Apply();

        Rect rect = new Rect(0, 0, tempTexture.width, tempTexture.height);
        Sprite newSprite = Sprite.Create(tempTexture, rect, new Vector2(0.5f, 0.5f));
        buttonImage.sprite = newSprite;
    }
}
