using UnityEngine;

public class ScaleSiblings : MonoBehaviour
{
    public Transform panel;
    public Transform[] siblings;

    void Start()
    {
        UpdateSiblingScales();
    }

    void UpdateSiblingScales()
    {
        float panelWidth = panel.GetComponent<RectTransform>().rect.width;
        float panelHeight = panel.GetComponent<RectTransform>().rect.height;
        float minDimension = Mathf.Min(panelWidth, panelHeight);
        float widthPerSibling = panelWidth / siblings.Length;

        for (int i = 0; i < siblings.Length; i++)
        {
            RectTransform siblingRect = siblings[i].GetComponent<RectTransform>();
            siblingRect.anchoredPosition = new Vector2(widthPerSibling * i, 0);
            siblingRect.localScale = new Vector3(minDimension, minDimension, 1);
        }
    }

    // Call this method only when necessary, such as on panel size change
    public void OnPanelSizeChanged()
    {
        UpdateSiblingScales();
    }
}
