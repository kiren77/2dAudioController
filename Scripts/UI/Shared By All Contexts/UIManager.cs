using System.Collections.Generic;
using UnityEngine;
using ContextStateGroup;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [System.Serializable]
    public struct ContextPanelMapping
    {
        public ContextState state;
        public GameObject panel;
        public GameObject ambiencePrefabParent; // GameObject for the Ambience prefab parent
        public GameObject musicPrefabParent;    // GameObject for the Music prefab parent
    }

    public List<ContextPanelMapping> panelMappings;
    public GameObject ambienceUIPrefab;
    public GameObject musicUIPrefab;
    public bool enableLogging = false;

    private Dictionary<ContextState, GameObject> panelDictionary;
    private Dictionary<GameObject, GameObject> instantiatedAmbienceUI = new Dictionary<GameObject, GameObject>();
    private Dictionary<GameObject, GameObject> instantiatedMusicUI = new Dictionary<GameObject, GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializePanelDictionary();

        if (ContextStateManager.Instance != null)
        {
            ContextStateManager.Instance.OnContextStateChanged += UpdatePanelVisibility;
        }
    }

    private void InitializePanelDictionary()
    {
        panelDictionary = new Dictionary<ContextState, GameObject>();

        foreach (var mapping in panelMappings)
        {
            if (!panelDictionary.ContainsKey(mapping.state))
            {
                panelDictionary.Add(mapping.state, mapping.panel);
                if (mapping.panel != null)
                {
                    mapping.panel.SetActive(false);
                }
            }
            else
            {
                if (enableLogging)
                {
                    Debug.LogWarning($"Duplicate panel mapping for state {mapping.state} found in {gameObject.name}");
                }
            }
        }
        
        // Initialize the Audio UI
        InitializeAudioUI();
    }

    private void InitializeAudioUI()
    {
        foreach (var mapping in panelMappings)
        {
            if (mapping.panel.activeSelf)
            {
                var ambienceUIParent = mapping.ambiencePrefabParent != null ? mapping.ambiencePrefabParent.transform : null;
                var musicUIParent = mapping.musicPrefabParent != null ? mapping.musicPrefabParent.transform : null;

                if (ambienceUIParent != null && ambienceUIPrefab != null)
                {
                    var ambienceUIInstance = Instantiate(ambienceUIPrefab, ambienceUIParent);
                    instantiatedAmbienceUI[mapping.panel] = ambienceUIInstance;
                    if (enableLogging)
                    {
                        Debug.Log("Ambience UI prefab instantiated in panel: " + mapping.panel.name);
                    }
                }
                else
                {
                    if (enableLogging)
                    {
                        Debug.LogWarning("AmbiencePrefabParent is not assigned in panel: " + mapping.panel.name);
                    }
                }

                if (musicUIParent != null && musicUIPrefab != null)
                {
                    var musicUIInstance = Instantiate(musicUIPrefab, musicUIParent);
                    instantiatedMusicUI[mapping.panel] = musicUIInstance;
                    if (enableLogging)
                    {
                        Debug.Log("Music UI prefab instantiated in panel: " + mapping.panel.name);
                    }
                }
                else
                {
                    if (enableLogging)
                    {
                        Debug.LogWarning("MusicPrefabParent is not assigned in panel: " + mapping.panel.name);
                    }
                }
            }
            else
            {
                if (enableLogging)
                {
                    Debug.Log("Panel is not active, skipping UI initialization for panel: " + mapping.panel.name);
                }
            }
        }
    }

    private void DestroyCurrentUI()
    {
        foreach (var kvp in instantiatedAmbienceUI)
        {
            Destroy(kvp.Value);
        }
        instantiatedAmbienceUI.Clear();

        foreach (var kvp in instantiatedMusicUI)
        {
            Destroy(kvp.Value);
        }
        instantiatedMusicUI.Clear();
    }

    public void UpdatePanelVisibility(ContextState newState)
    {
        DestroyCurrentUI(); // Destroy UI elements before updating panel visibility

        foreach (var kvp in panelDictionary)
        {
            if (kvp.Value != null)
            {
                kvp.Value.SetActive(kvp.Key == newState);
                if (kvp.Key == newState)
                {
                    InitializeAudioUI();
                }
            }
        }

        if (enableLogging)
        {
            Debug.Log($"UIManager: Panels updated for state {newState}");
        }
    }

    private void OnDestroy()
    {
        if (ContextStateManager.Instance != null)
        {
            ContextStateManager.Instance.OnContextStateChanged -= UpdatePanelVisibility;
        }
    }
}
