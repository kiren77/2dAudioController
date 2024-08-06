using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;

public class FmodEventManager : MonoBehaviour
{
    public Dictionary<string, List<EventDescription>> FmodEventsByType { get; private set; } = new Dictionary<string, List<EventDescription>>();
    private string bankName = "Master Bank";
    private string userPropertyName = "Type";

    private void Awake()
    {
        RuntimeManager.LoadBank(bankName, true);
        StartCoroutine(LoadEventPaths());
    }

    private IEnumerator LoadEventPaths()
    {
        if (RuntimeManager.StudioSystem.getBank(bankName, out var bank) == FMOD.RESULT.OK && bank.isValid())
        {
            bank.getEventCount(out int eventCount);
            var eventDescriptions = new EventDescription[eventCount];
            bank.getEventList(out eventDescriptions);

            foreach (var eventDescription in eventDescriptions)
            {
                if (eventDescription.isValid() && eventDescription.getPath(out string eventPath) == FMOD.RESULT.OK)
                {
                    if (eventDescription.getUserProperty(userPropertyName, out var userProperty) == FMOD.RESULT.OK)
                    {
                        string eventType = userProperty.stringValue();
                        if (!FmodEventsByType.ContainsKey(eventType))
                        {
                            FmodEventsByType[eventType] = new List<EventDescription>();
                        }
                        FmodEventsByType[eventType].Add(eventDescription);
                    }
                }
            }
        }
        else
        {
            Debug.LogError($"Failed to load FMOD bank: {bankName}");
        }
        yield return null;
    }

    public List<EventDescription> GetFmodEventsByType(string eventType)
    {
        return FmodEventsByType.TryGetValue(eventType, out var events) ? events : new List<EventDescription>();
    }
}
