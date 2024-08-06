using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CampaignSession.DayNightCycle;
//using Debug = UnityEngine.//Debug;

public class DayNightButtons : MonoBehaviour
{
    public Button toDayButton;
    public Button toNightButton;
    public Image sunImage;
    public Image moonImage;
    public Image dayImage; // Image for day text/icon
    public Image nightImage; // Image for night text/icon
    public Image background;
    public Transform sunMoonParentTransform; // Parent transform of sun and moon
    public Color dayColor = Color.white;
    public Color nightColor = Color.black;

    [SerializeField]
    private float rotationDuration = 1.0f; // Duration for rotation, adjustable in the editor
    [SerializeField]
    private float fadeDuration = 1.0f; // Duration for fade, adjustable in the editor

    private bool isRotating = false;
    private bool isFading = false;
    void Start()
    {
        DayNightManager.Instance.OnDayNightChanged += UpdateButtonState;
        UpdateButtonState(DayNightManager.Instance.IsDay);
    }

    void OnEnable()
    {
        DayNightManager.Instance.OnDayNightChanged += UpdateButtonState;
    }

    void OnDisable()
    {
        DayNightManager.Instance.OnDayNightChanged -= UpdateButtonState;
    }

    public void OnToDayClicked()
    {
        DayNightUIManager.Instance.UpdateDayNightState(true);
    }

    public void OnToNightClicked()
    {
        DayNightUIManager.Instance.UpdateDayNightState(false);
    }

    public void UpdateButtonState(bool isDay)
{
        if (isRotating || isFading)   
         {
        return;
    }

    // Update button interactability
    toDayButton.interactable = !isDay;
    toNightButton.interactable = isDay;

    // Update sun and moon visibility
    sunImage.gameObject.SetActive(isDay);
    moonImage.gameObject.SetActive(!isDay);

    // Trigger animations
    RotateSunAndMoon(isDay ? 0 : 180); // Assuming 180 degrees is the rotation needed       
        FadeTo(isDay ? dayColor : nightColor);

    // Update day and night images
    dayImage.gameObject.SetActive(isDay);
    nightImage.gameObject.SetActive(!isDay);
}

    public void RotateSunAndMoon(float targetAngle)
    {
        if (isRotating)
        {
            return;
        }

        StartCoroutine(RotateCoroutine(targetAngle));
    }

    private IEnumerator RotateCoroutine(float targetAngle)
    {
                isRotating = true;
        //Debug.Log("Rotation Start");

        Quaternion startRotation = sunMoonParentTransform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, targetAngle);

        float elapsedTime = 0.0f;

        while (elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;
            sunMoonParentTransform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sunMoonParentTransform.rotation = endRotation;
        isRotating = false;
        //Debug.Log("Rotation End");
    }

    public void FadeTo(Color targetColor)
    {
        if (isFading)
        {
            return;
        }

        StartCoroutine(FadeCoroutine(targetColor));
    }

    private IEnumerator FadeCoroutine(Color targetColor)
    {
        isFading = true;
        

        Color startColor = background.color;
        //Debug.Log("Fade start: Start color: " + startColor + ", Target color: " + targetColor);

        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            Color currentColor = Color.Lerp(startColor, targetColor, t);
            background.color = currentColor;
            ////Debug.Log("Fading: " + currentColor);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        background.color = targetColor;
        //Debug.Log("Fade end: Final color: " + targetColor);

        isFading = false;
            }
}