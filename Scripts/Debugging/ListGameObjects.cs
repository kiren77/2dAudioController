using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;
using System;

public class ListGameObjects : MonoBehaviour
{
    public string directoryPath = "DefaultDirectory";
    public bool listComponents = false;
    public bool useSpecificHierarchy = false;
    public Transform specificHierarchy;

    private StringBuilder sb = new StringBuilder();

    void Start()
    {
        sb.AppendLine("Legend:");
        sb.AppendLine("  - [GO]: Game Object");
        sb.AppendLine("  - [C]: Component");
        sb.AppendLine("  - [DA]: Disabled Component");
        sb.AppendLine(new string('-', 50));

        List<GameObject> rootObjects = new List<GameObject>();
        UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        foreach (GameObject obj in rootObjects)
        {
            sb.AppendLine("Root Object: " + obj.name);
            if (useSpecificHierarchy)
            {
                PrintSpecificHierarchy(obj.transform, specificHierarchy, "-");
            }
            else
            {
                PrintChildren(obj.transform, "-");
            }
        }

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string path = Path.Combine(directoryPath, "GameObjectList.txt");
        File.WriteAllText(path, sb.ToString());

        Debug.Log("Data exported to: " + path);
    }

    void PrintChildren(Transform parent, string indentation)
    {
        if (parent == null) return;

        foreach (Transform child in parent)
        {
            if (child == null) continue;

            sb.AppendLine(indentation + "[GO] " + child.gameObject.name);
            if (listComponents)
            {
                foreach (Component component in child.gameObject.GetComponents<Component>())
                {
                    if (component == null) continue;

                    string componentName = component.GetType().Name;
                    if (component is Behaviour behaviour && !behaviour.enabled)
                    {
                        sb.AppendLine(indentation + "  [DA] [C] " + componentName);
                    }
                    else
                    {
                        sb.AppendLine(indentation + "  [C] " + componentName);
                    }

                    // List the properties and fields if it's a MonoBehaviour
                    if (component is MonoBehaviour)
                    {
                        ListComponentValues(component, indentation + "    ");
                    }
                }
            }
            PrintChildren(child, indentation + "  ");
        }
    }

    void ListComponentValues(Component component, string indentation)
    {
        System.Type type = component.GetType();
        BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

        foreach (PropertyInfo property in type.GetProperties(flags))
        {
            try
            {
                // Exclude properties known to cause issues or that are deprecated
                if (property.Name == "rigidbody" || property.Name == "rigidbody2D" ||
                    property.Name == "camera" || property.Name == "light" ||
                    property.Name == "animation" || property.Name == "audio" ||
                    property.Name == "guiText" || property.Name == "guiTexture" ||
                    property.Name == "collider" || property.Name == "collider2D" ||
                    property.Name == "hingeJoint" || property.Name == "particleSystem")
                {
                    continue;
                }

                if (property.CanRead)
                {
                    object value = property.GetValue(component, null);
                    sb.AppendLine(indentation + property.Name + ": " + (value != null ? value.ToString() : "null"));
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine(indentation + property.Name + ": Exception - " + ex.Message);
            }
        }

        foreach (FieldInfo field in type.GetFields(flags))
        {
            try
            {
                object value = field.GetValue(component);
                sb.AppendLine(indentation + field.Name + ": " + (value != null ? value.ToString() : "null"));
            }
            catch (Exception ex)
            {
                sb.AppendLine(indentation + field.Name + ": Exception - " + ex.Message);
            }
        }
    }

    void PrintSpecificHierarchy(Transform parent, Transform specific, string indentation)
    {
        if (parent == null || specific == null) return;

        if (parent == specific)
        {
            int depth = CalculateDepth(specific);
            PrintHierarchyUp(parent, "", depth);
            PrintChildren(parent, indentation + "-");
        }
        else
        {
            foreach (Transform child in parent)
            {
                PrintSpecificHierarchy(child, specific, indentation + "-");
            }
        }
    }

    void PrintHierarchyUp(Transform child, string indentation, int depth)
    {
        if (child == null) return;

        if (child.parent != null)
        {
            PrintHierarchyUp(child.parent, indentation, depth - 1);
        }
        sb.AppendLine(new string('-', depth) + "[GO] " + child.gameObject.name);
    }

    int CalculateDepth(Transform transform)
    {
        int depth = 0;
        while (transform != null && transform.parent != null)
        {
            depth++;
            transform = transform.parent;
        }
        return depth;
    }
}
