// DayNightButtons.cs
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DayNightButtons : MonoBehaviour
{
    public Button toDay;
    public Button toNight;
    public Image dayImage;
    public Image nightImage;

    public Transform sunMoonParentTransform;
    public Image sunImage;
    public Image moonImage;

    private float elapsedTimeFadeTo;
    private float elapsedTimeRotateTo;

    [SerializeField]
    private float rotationSpeed = 1.0f;

    [SerializeField]
    private float scale = 1.0f;

    public Color dayColor = Color.white;
    public Color nightColor = Color.black;

    public Image background;

    Vector2 sunStartScale;
    Vector2 moonStartScale;

    private float currentAngle = 180.0f;
    private bool isRotating = false;

    // Reference to the DayNightUIManager script
    public DayNightUIManager dayNightUIManager;

    void Start()
    {
        bool isDay = ContextStateManager.isDay;
        switchDayNightValueChanged(isDay);

        toDay.onClick.AddListener(() => dayNightUIManager.UpdateDayNightState(true));
        toNight.onClick.AddListener(() => dayNightUIManager.UpdateDayNightState(false));
    }

  public float delayDuration = 2.0f;

  public void switchDayNightValueChanged(bool isDay)
  {
      ContextStateManager.isDay = isDay;

      if (isDay)
      {
          toDay.interactable = false;
          Invoke("EnableToNightButton", delayDuration);

          dayImage.gameObject.SetActive(true);
          nightImage.gameObject.SetActive(false);

          RotateTo(isDay);
      }
      else
      {
          Invoke("EnableToDayButton", delayDuration);
          toNight.interactable = false;

          dayImage.gameObject.SetActive(false);
          nightImage.gameObject.SetActive(true);

          RotateTo(isDay);
      }
  }

  void EnableToNightButton()
  {
      toNight.interactable = true;
  }

  void EnableToDayButton()
  {
      toDay.interactable = true;
  }

  public void RotateTo(bool isDay)
  {
      if (isRotating) return;

      currentAngle += 180.0f;
      StartCoroutine(RotateSunAndMoon(currentAngle));

      Color sunColor = sunImage.color;
      sunColor.a = isDay ? 1.0f : 0.2f;
      sunImage.color = sunColor;

      Color moonColor = moonImage.color;
      moonColor.a = isDay ? 0.2f : 1.0f;
      moonImage.color = moonColor;

      StartCoroutine(FadeTo(isDay ? dayColor : nightColor));
  }

  public IEnumerator RotateSunAndMoon(float targetAngle)
  {
      isRotating = true;

      float duration = 1.0f / rotationSpeed;
      float elapsedTimeRotateTo = 0.0f;
      float currentVelocity = 0.0f;
      float smoothTime = 0.3f;

      while (elapsedTimeRotateTo < duration)
      {
          elapsedTimeRotateTo += Time.deltaTime * scale;

          float zAngle = sunMoonParentTransform.localEulerAngles.z;
          zAngle = Mathf.SmoothDampAngle(zAngle, targetAngle, ref currentVelocity, smoothTime);
          sunMoonParentTransform.localRotation = Quaternion.Euler(0, 0, zAngle);

          yield return null;
      }

      isRotating = false;
  }

  public IEnumerator FadeTo(Color targetColor)
  {
      float duration = 1.0f / rotationSpeed;
      elapsedTimeFadeTo = 0.0f;
      Color startColor = background.color;

      while (elapsedTimeFadeTo < duration)
      {
          elapsedTimeFadeTo += Time.deltaTime * scale;
          background.color = Color.Lerp(startColor, targetColor, elapsedTimeFadeTo / duration);
          yield return null;
      }
   }
}
