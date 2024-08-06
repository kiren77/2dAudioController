using UnityEngine;

[CreateAssetMenu(fileName = "UIPathsConfig", menuName = "ScriptableObjects/UIPathsConfig", order = 1)]
public class UIPathsConfig : ScriptableObject
{
    public UIPath[] musicUIPaths;
    public UIPath[] ambienceUIPaths;
}

[System.Serializable]
public struct UIPath
{
    public string path;
}
