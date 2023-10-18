using UnityEngine;
using System;
using ContextStateGroup;

public class PanelSelector : MonoBehaviour
{
    public GameObject[] panels; // assign the panels in the Inspector

    void Update()
    {
        ContextState currentState = ContextStateManager.Instance.GetCurrentState();
        for (int i = 0; i < panels.Length; i++)
        {
            if ((int)currentState == i)
            {
                panels[i].transform.SetAsLastSibling();
            }
        }
    }
}
