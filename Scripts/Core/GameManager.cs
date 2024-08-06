using UnityEngine;
using EmotionSystem;
using ContextStateGroup;
using CampaignSession.DayNightCycle;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static readonly object _lock = new object();
    public static GameManager Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        _instance = go.AddComponent<GameManager>();
                        DontDestroyOnLoad(go);
                    }
                }
                return _instance;
            }
        }
    }

    [SerializeField] private bool enableLogging = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManagers();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeManagers()
    {
        InitializeManager<EmotionalStateManager>("EmotionalStateManager");
        InitializeManager<DayNightManager>("DayNightManager");
        InitializeManager<DayNightUIManager>("DayNightUIManager");
        InitializeManager<ContextStateManager>("ContextStateManager");
    }

    private void InitializeManager<T>(string managerName) where T : Component
    {
        T existingInstance = FindObjectOfType<T>();
        if (existingInstance == null)
        {
            if (enableLogging)
            {
                Debug.Log($"{managerName} instance is being created.");
            }
            GameObject managerObject = new GameObject(managerName);
            managerObject.AddComponent<T>();
            DontDestroyOnLoad(managerObject);
        }
        else
        {
            if (enableLogging)
            {
                Debug.Log($"{managerName} instance already exists, using existing instance.");
            }
        }
    }
}
