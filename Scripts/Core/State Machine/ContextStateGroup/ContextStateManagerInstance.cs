using UnityEngine;

namespace ContextStateGroup
{
    public class ContextStateManagerInstance : MonoBehaviour
    {
        private static ContextStateManagerInstance _instance;
        public static ContextStateManagerInstance Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ContextStateManagerInstance>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject();
                        go.name = "ContextStateManager";
                        _instance = go.AddComponent<ContextStateManagerInstance>();
                        DontDestroyOnLoad(go);
                    }
                }
                
                return _instance;
            }
        }
    }
}