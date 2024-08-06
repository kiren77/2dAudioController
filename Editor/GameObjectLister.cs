using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;
using System.Threading.Tasks;

public class GameObjectLister : EditorWindow
{
    public string outputPath = "DefaultDirectory"; // Directory path for saving the output file
    public bool listComponents = false;
    public bool useSpecificHierarchy = false;
    public Transform specificHierarchy; // The specific GameObject to list hierarchy from
    public bool detailedOutput = false; // Toggle for detailed output
    public bool customScriptsOnly = false; // Toggle for detailed output for custom scripts only
    public string outputFileName = "GameObjectList.txt"; // Custom filename

    private StringBuilder sb = new StringBuilder();

    private static readonly HashSet<string> commonUnityComponents = new HashSet<string>
    {
        "Transform", "RectTransform", "Camera", "Canvas", "CanvasScaler", "CanvasRenderer",
        "GraphicRaycaster", "HorizontalLayoutGroup", "VerticalLayoutGroup", "Image", "Button",
        "TextMeshProUGUI", "Physics2DRaycaster", "LayoutElement"
    };

    [MenuItem("Tools/GameObject Lister")]
    public static void ShowWindow()
    {
        GetWindow<GameObjectLister>("GameObject Lister");
    }

    void OnGUI()
    {
        GUILayout.Label("Settings", EditorStyles.boldLabel);

        outputPath = EditorGUILayout.TextField("Output Path", outputPath); // Directory path input
        listComponents = EditorGUILayout.Toggle("List Components", listComponents); // Toggle for listing components
        useSpecificHierarchy = EditorGUILayout.Toggle("Use Specific Hierarchy", useSpecificHierarchy); // Toggle for specific hierarchy
        specificHierarchy = (Transform)EditorGUILayout.ObjectField("Specific Hierarchy", specificHierarchy, typeof(Transform), true); // GameObject input for specific hierarchy
        detailedOutput = EditorGUILayout.Toggle("Detailed Output", detailedOutput); // Toggle for detailed output

        if (detailedOutput)
        {
            customScriptsOnly = EditorGUILayout.Toggle("Custom Scripts Only", customScriptsOnly); // Toggle for detailed output for custom scripts only
        }

        outputFileName = EditorGUILayout.TextField("Output File Name", outputFileName); // Custom filename input

        if (GUILayout.Button("List GameObjects"))
        {
            ListGameObjects();
        }
    }

    async void ListGameObjects()
    {
        sb.Clear();
        sb.AppendLine("Legend:");
        sb.AppendLine("  - [GO]: Game Object");
        sb.AppendLine("  - [C]: Component");
        sb.AppendLine("  - [A]: Active");
        sb.AppendLine("  - [DA]: Disabled");
        sb.AppendLine(new string('-', 50));

        LogCaptureMode();

        if (useSpecificHierarchy && specificHierarchy != null)
        {
            AppendHierarchy(specificHierarchy, 0);
        }
        else
        {
            AppendRootObjects();
        }

        await WriteToFileAsync(Path.Combine(outputPath, outputFileName));
    }

    void LogCaptureMode()
    {
        if (Application.isPlaying)
        {
            Debug.Log("Capturing hierarchy in Play Mode");
        }
        else
        {
            Debug.Log("Capturing hierarchy in Edit Mode");
        }
    }

    void AppendRootObjects()
    {
        List<GameObject> rootObjects = new List<GameObject>();
        UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        foreach (GameObject obj in rootObjects)
        {
            sb.AppendLine("Root Object: " + obj.name);
            AppendChildren(obj.transform, 0); // Start with depth 0 for children
        }
    }

    void AppendHierarchy(Transform obj, int depth)
    {
        AppendParentHierarchy(obj, depth);
        AppendChildren(obj, depth + 1); // Start with depth 1 for children
    }

    void AppendParentHierarchy(Transform obj, int depth)
    {
        if (obj.parent != null)
        {
            AppendParentHierarchy(obj.parent, depth + 1);
        }
        sb.AppendLine(new string(' ', depth * 2) + $"-[GO] {(obj.gameObject.activeInHierarchy ? "[A]" : "[DA]")} " + obj.gameObject.name);
    }

    void AppendChildren(Transform parent, int depth)
    {
        if (parent == null) return;

        sb.AppendLine(new string(' ', depth * 2) + $"-[GO] {(parent.gameObject.activeInHierarchy ? "[A]" : "[DA]")} " + parent.gameObject.name);
        if (listComponents)
        {
            AppendComponents(parent, depth);
        }

        foreach (Transform child in parent)
        {
            AppendChildren(child, depth + 1);
        }
    }

    void AppendComponents(Transform parent, int depth)
    {
        foreach (Component component in parent.gameObject.GetComponents<Component>())
        {
            if (component == null) continue;

            string componentName = component.GetType().Name;
            string status = component is Behaviour behaviour && !behaviour.enabled ? "[DA]" : "[A]";

            sb.AppendLine(new string(' ', (depth * 2) + 2) + $"-[C] {status} " + componentName);

            if (detailedOutput && ShouldListComponentDetails(component))
            {
                AppendComponentValues(component, depth + 2);
            }
        }
    }

    bool ShouldListComponentDetails(Component component)
    {
        if (!detailedOutput)
            return false;

        if (customScriptsOnly)
        {
            return !IsBuiltInComponent(component);
        }

        return true;
    }

    bool IsBuiltInComponent(Component component)
    {
        return commonUnityComponents.Contains(component.GetType().Name);
    }

    void AppendComponentValues(Component component, int depth)
    {
        System.Type type = component.GetType();
        BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

        foreach (PropertyInfo property in type.GetProperties(flags))
        {
            try
            {
                if (IsExcludedProperty(property)) continue;

                if (property.CanRead)
                {
                    object value = property.GetValue(component, null);
                    if (IsIncompleteOrOldReference(value))
                    {
                        sb.AppendLine(new string(' ', depth * 2) + "** " + property.Name + ": " + (value != null ? value.ToString() : "null"));
                    }
                    else
                    {
                        sb.AppendLine(new string(' ', depth * 2) + property.Name + ": " + (value != null ? value.ToString() : "null"));
                    }
                }
            }
            catch (System.Exception)
            {
                sb.AppendLine(new string(' ', depth * 2) + property.Name + ": ???");
            }
        }

        foreach (FieldInfo field in type.GetFields(flags))
        {
            try
            {
                object value = field.GetValue(component);
                if (IsIncompleteOrOldReference(value))
                {
                    sb.AppendLine(new string(' ', depth * 2) + "** " + field.Name + ": " + (value != null ? value.ToString() : "null"));
                }
                else
                {
                    sb.AppendLine(new string(' ', depth * 2) + field.Name + ": " + (value != null ? value.ToString() : "null"));
                }
            }
            catch (System.Exception)
            {
                sb.AppendLine(new string(' ', depth * 2) + field.Name + ": ???");
            }
        }
    }

    bool IsIncompleteOrOldReference(object value)
    {
        if (value == null) return true;
        return false;
    }

    bool IsExcludedProperty(PropertyInfo property)
    {
        string[] excludedProperties = {
            "rigidbody", "rigidbody2D", "camera", "light", "animation", "audio",
            "guiText", "guiTexture", "collider", "collider2D", "hingeJoint", "particleSystem"
        };

        foreach (string excluded in excludedProperties)
        {
            if (property.Name == excluded)
                return true;
        }

        return false;
    }

    async Task WriteToFileAsync(string path)
    {
        try
        {
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            using (StreamWriter writer = new StreamWriter(path))
            {
                await writer.WriteAsync(sb.ToString());
            }

            Debug.Log("Data exported to: " + path);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to write to file: " + e.Message);
        }
    }
}
