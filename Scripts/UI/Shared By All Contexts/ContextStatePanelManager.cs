using UnityEngine;
using System.Collections.Generic;
using ContextStateGroup;

public class ContextStatePanelManager : MonoBehaviour
{
    public static ContextStatePanelManager Instance { get; private set; }

    public List<StatePanelMapping> panelMappings; // List of panels mapped to context states
    private GameObject currentActivePanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePanels();
            if (ContextStateManager.Instance != null)
            {
                ContextStateManager.Instance.OnContextStateChanged += UpdatePanelVisibility;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (ContextStateManager.Instance != null)
        {
            ContextStateManager.Instance.OnContextStateChanged -= UpdatePanelVisibility;
        }
    }

    private void InitializePanels()
    {
        // Initially set all panels inactive
        foreach (var mapping in panelMappings)
        {
            mapping.panel.SetActive(false);
        }

        // Set initial panel active if current state is set in ContextStateManager
        UpdatePanelVisibility(ContextStateManager.Instance.GetCurrentState());
    }

    private void UpdatePanelVisibility(ContextState newState)
    {
        if (currentActivePanel != null)
        {
            currentActivePanel.SetActive(false);
        }

        foreach (var mapping in panelMappings)
        {
            if (mapping.state == newState)
            {
                currentActivePanel = mapping.panel;
                currentActivePanel.SetActive(true);
                Debug.Log($"ContextStatePanelManager: Activated panel for state {newState}");
                return;
            }
        }

        Debug.LogWarning($"ContextStatePanelManager: No panel found for state {newState}");
    }

    public GameObject GetCurrentPanel()
    {
        return currentActivePanel;
    }
}
