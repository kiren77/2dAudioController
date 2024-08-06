using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ScriptUsageChecker : MonoBehaviour
{
    [MenuItem("Tools/Check Script Usage")]
    public static void CheckScriptUsage()
    {
        string scriptFolderPath = @"C:\Users\kiren\OneDrive\Documents\UNITY AND FMOD\Unity Projects\2D Audio Controller\Assets\2dAudioController\Scripts";
        string outputFilePath = @"C:\Users\kiren\OneDrive\Documents\UNITY AND FMOD\Unity Projects\2D Audio Controller\Assets\2dAudioController\Scripts\ScriptUsageReport.csv";

        // Get all scripts in the specified folder
        string[] scriptFiles = Directory.GetFiles(scriptFolderPath, "*.cs", SearchOption.AllDirectories);

        // Dictionary to store script usage information
        Dictionary<string, List<string>> scriptUsage = new Dictionary<string, List<string>>();

        // Iterate through all GameObjects in the scene
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject go in allGameObjects)
        {
            // Get all components attached to the GameObject
            Component[] components = go.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component == null) continue;

                // Get the script file path of the component
                MonoScript monoScript = MonoScript.FromMonoBehaviour(component as MonoBehaviour);
                if (monoScript != null)
                {
                    string scriptPath = AssetDatabase.GetAssetPath(monoScript);
                    if (scriptPath.StartsWith(scriptFolderPath))
                    {
                        string scriptName = Path.GetFileName(scriptPath);
                        if (!scriptUsage.ContainsKey(scriptName))
                        {
                            scriptUsage[scriptName] = new List<string>();
                        }
                        scriptUsage[scriptName].Add(go.name);
                    }
                }
            }
        }

        // Write the script usage information to a CSV file
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            writer.WriteLine("Script,GameObjects");
            foreach (var entry in scriptUsage)
            {
                string gameObjects = string.Join(";", entry.Value);
                writer.WriteLine($"{entry.Key},{gameObjects}");
            }
        }

        Debug.Log("Script usage report generated at: " + outputFilePath);
    }
}
