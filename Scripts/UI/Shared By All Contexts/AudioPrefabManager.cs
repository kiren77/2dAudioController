using UnityEngine;
using ContextStateGroup;

public class AudioPrefabManager : MonoBehaviour
{
    private static AudioPrefabManager _instance;
    public static AudioPrefabManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioPrefabManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("AudioPrefabManager");
                    _instance = go.AddComponent<AudioPrefabManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    public GameObject ambienceUIPrefab;
    public GameObject musicUIPrefab;

    public void InitializeAudioUI(GameObject panel, ContextState state)
    {
        Transform standardAudioUI = panel.transform.Find("Standard Audio UI");
        if (standardAudioUI == null)
        {
            Debug.LogError("Standard Audio UI not found in panel");
            return;
        }

        // Instantiate Ambience UI if available
        Transform ambienceAudioUI = standardAudioUI.Find("Ambience Audio UI");
        if (ambienceAudioUI != null)
        {
            Instantiate(ambienceUIPrefab, ambienceAudioUI);
        }

        // Instantiate Music UI if available
        Transform musicAudioUI = standardAudioUI.Find("Music Audio UI");
        if (musicAudioUI != null)
        {
            Instantiate(musicUIPrefab, musicAudioUI);
        }
    }
}
