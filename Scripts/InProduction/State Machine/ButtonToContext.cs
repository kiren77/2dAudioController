using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using ContextStateGroup;

public class ButtonToContext : MonoBehaviour
{
    public Button button;
    public ContextState targetState;

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        ContextStateManager.Instance.SetCurrentState(targetState);
        Debug.Log("Current State is " + targetState);
    }
}
