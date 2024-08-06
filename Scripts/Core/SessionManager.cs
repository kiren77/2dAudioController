using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using ContextStateGroup;

public class SessionManager : MonoBehaviour
{
    public enum SessionState { Staging, GoLive }
    public SessionState CurrentState { get; private set; } = SessionState.Staging;
    private Dictionary<ContextState, bool> contextChanges = new Dictionary<ContextState, bool>();

    void Start()
    {
        InitializeContexts();
    }

    private void InitializeContexts()
    {
        foreach (ContextState context in Enum.GetValues(typeof(ContextState)))
        {
            contextChanges[context] = false;
        }
    }

    public void SetContextChanged(ContextState context)
    {
        if (CurrentState == SessionState.Staging)
        {
            contextChanges[context] = true;
        }
    }

    public void GoLive()
    {
        if (contextChanges.Values.Any(changed => !changed))
        {
            Debug.LogWarning("Not all contexts have been configured.");
            // Prompt user for action
        }
        else
        {
            CurrentState = SessionState.GoLive;
            Debug.Log("Transitioning to GoLive state.");
        }
    }
}
