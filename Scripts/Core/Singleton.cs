// --- Assets/2dAudioController/Scripts/Core/Singleton.cs ---
using UnityEngine;

/// <summary>
/// Generic Singleton base class.
/// </summary>
/// <typeparam name="T">Type of the Singleton class.</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();

    /// <summary>
    /// Accessor for the Singleton instance.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();
                        if (_instance == null)
                        {
                            GameObject singletonObject = new GameObject(typeof(T).Name);
                            _instance = singletonObject.AddComponent<T>();
                            DontDestroyOnLoad(singletonObject);
                        }
                    }
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// Ensures only one instance of the Singleton exists.
    /// </summary>
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = this as T;
                    DontDestroyOnLoad(gameObject);
                }
                else if (_instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}
