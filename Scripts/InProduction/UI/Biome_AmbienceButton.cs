using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ContextStateGroup;

public class Biome_AmbienceButton : MonoBehaviour
{
    [SerializeField]
    public StudioEventEmitter eventEmitter; // Assign this in the Inspector

    [SerializeField]
    public ContextState requiredContextState;

    [SerializeField]
    public string requiredBiomeType;

    public static Biome_AmbienceButton currentlyPlayingButton;

    private void Start()
    {
        ContextStateManagerEvents.Instance.OnContextStateChanged += HandleContextStateChanged;
        ContextStateManagerEvents.Instance.OnBiomeTypeChanged += HandleBiomeTypeChanged;
        CheckRequirements();
    }

    private void OnDestroy()
    {
        ContextStateManagerEvents.Instance.OnContextStateChanged -= HandleContextStateChanged;
        ContextStateManagerEvents.Instance.OnBiomeTypeChanged -= HandleBiomeTypeChanged;
    }

    private void HandleContextStateChanged(ContextState newState)
    {
        CheckRequirements();
    }

    private void HandleBiomeTypeChanged(string newBiomeType)
    {
        CheckRequirements();
    }

    public void OnButtonPressed()
{
    UnityEngine.Debug.Log("Button pressed for biome: " + requiredBiomeType);

    CheckRequirements();
}

    private void CheckRequirements()
{
    // Log that the CheckRequirements method was called
    //    UnityEngine.Debug.Log("CheckRequirements called");

    var currentState = ContextStateManager.Instance.GetCurrentState();
    var currentBiomeType = ContextStateManager.Instance.GetCurrentBiomeType();

    if (currentState == requiredContextState && currentBiomeType == requiredBiomeType)
    {
        // Log that the PlaySound method is about to be called
        UnityEngine.Debug.Log("Calling PlaySound");
        PlaySound();
    }
    else
    {
        StopSound();
    }

    // Make the button non-interactable if it is the currentlyPlayingButton
    GetComponent<UnityEngine.UI.Button>().interactable = (currentlyPlayingButton != this);
}


    private void PlaySound()
{
    if (currentlyPlayingButton != null)
    {
        currentlyPlayingButton.eventEmitter.Stop();
    }

    if (eventEmitter != null)
    {
        // Log the path of the FMOD event being played
      //  UnityEngine.Debug.Log("Playing FMOD event: " + eventEmitter.EventReference);

        eventEmitter.Play();
    }
    else
    {
       // UnityEngine.Debug.LogError("eventEmitter is not assigned in the Inspector");
    }

    currentlyPlayingButton = this;
}
private void StopSound()
{
    // Check if eventEmitter is not null before calling Stop
    if (eventEmitter != null)
    {
        // Stop this button's sound (if it's playing)
        eventEmitter.Stop();
    }
    else
    {
       // UnityEngine.Debug.LogError("eventEmitter is not assigned in the Inspector");
    }

    // Clear the currently playing button (if it's this button)
    if (currentlyPlayingButton == this)
    {
        currentlyPlayingButton = null;
    }
}
}
