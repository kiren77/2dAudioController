using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;

public class UIDiagnosticTool : MonoBehaviour
{
    [Tooltip("Specify a custom file path to save the diagnostics log. Ensure the directory has write permissions.")]
    public string customFilePath;

    private StringBuilder logBuilder;
    private string defaultFilePath;

    void Start()
    {
        logBuilder = new StringBuilder();
        defaultFilePath = Path.Combine(Application.persistentDataPath, "UIDiagnosticLog.txt");

        LogInitialHierarchy();
        LogCanvasSetup();
        CheckRaycastTargets();
        CheckOverlap();

        Application.quitting += SaveLogToFile;
    }

    void LogInitialHierarchy()
    {
        logBuilder.AppendLine("[Initial Hierarchy]");
        foreach (GameObject rootObj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            AppendGameObjectHierarchy(rootObj.transform, "");
        }
    }

    void AppendGameObjectHierarchy(Transform transform, string indent)
    {
        logBuilder.AppendLine($"{indent}{transform.name} (Active: {transform.gameObject.activeSelf})");
        foreach (Transform child in transform)
        {
            AppendGameObjectHierarchy(child, indent + "  ");
        }
    }

    void LogCanvasSetup()
    {
        logBuilder.AppendLine("[Canvas Setup]");
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            logBuilder.AppendLine($"Canvas: {canvas.name}");
            logBuilder.AppendLine($"  Render Mode: {canvas.renderMode}");
            logBuilder.AppendLine($"  Sorting Layer: {canvas.sortingLayerName}, Order: {canvas.sortingOrder}");
            
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                logBuilder.AppendLine($"  Camera: {canvas.worldCamera?.name ?? "None"}");
            }

            CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
            if (scaler != null)
            {
                logBuilder.AppendLine($"  Reference Resolution: {scaler.referenceResolution}");
                logBuilder.AppendLine($"  Match Width Or Height: {scaler.matchWidthOrHeight}");
            }
        }
    }

    void CheckRaycastTargets()
    {
        logBuilder.AppendLine("[Raycast Targets Enabled]");
        foreach (var canvas in FindObjectsOfType<Canvas>())
        {
            foreach (var graphic in canvas.GetComponentsInChildren<Graphic>())
            {
                if (graphic.raycastTarget)
                {
                    logBuilder.AppendLine($"Raycast target enabled: {graphic.name} on Canvas: {canvas.name}");
                }
            }
        }
    }

    void CheckOverlap()
    {
        logBuilder.AppendLine("[Overlap Detected]");
        var uiElements = FindObjectsOfType<RectTransform>();
        for (int i = 0; i < uiElements.Length; i++)
        {
            for (int j = i + 1; j < uiElements.Length; j++)
            {
                if (RectOverlaps(uiElements[i], uiElements[j]))
                {
                    logBuilder.AppendLine($"Overlap detected between {uiElements[i].name} and {uiElements[j].name}");
                }
            }
        }
    }

    bool RectOverlaps(RectTransform rect1, RectTransform rect2)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rect1, rect2.position) ||
               RectTransformUtility.RectangleContainsScreenPoint(rect2, rect1.position);
    }

    void SaveLogToFile()
    {
        string path = string.IsNullOrEmpty(customFilePath) ? defaultFilePath : customFilePath;
        string directoryPath = Path.GetDirectoryName(path);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        try
        {
            File.WriteAllText(path, logBuilder.ToString());
            Debug.Log($"Diagnostic log saved to {path}");
        }
        catch (UnauthorizedAccessException e)
        {
            Debug.LogError($"Access to the path '{path}' is denied. Error: {e.Message}");
        }
        catch (IOException e)
        {
            Debug.LogError($"An I/O error occurred while writing to the path '{path}'. Error: {e.Message}");
        }
        catch (Exception e)
        {
            Debug.LogError($"An unexpected error occurred: {e.Message}");
        }
    }

    void OnDestroy()
    {
        SaveLogToFile();
    }
}
