using UnityEngine;
using System;
using ContextStateGroup; // Add this line to include the ContextStateGroup namespace

public class ContextStateManager : MonoBehaviour
{
    private static bool _isDay;
    public static bool isDay
    {
        get { return _isDay; }
        set
        {
            _isDay = value;
         //   Debug.Log("isDay changed to: " + _isDay);
        }
    }

    private static ContextStateManager _instance;
    public static ContextStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ContextStateManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "ContextStateManager";
                    _instance = go.AddComponent<ContextStateManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

void Awake()
{
    _isDay = true;
}


    public ContextState GetCurrentState()
    {
        return ContextStateManagerFields.Instance.currentState; // Update this line
    }

    public void SetCurrentState(ContextState state)
    {
        Debug.Log("Setting current state to: " + state); // Debug statement
        ContextStateManagerFields.Instance.currentState = state; // Update this line
        ContextStateManagerEvents.Instance.InvokeOnContextStateChanged(ContextStateManagerFields.Instance.currentState); // Update this line
    }

    public string GetCurrentInteriorType()
{
    return ContextStateManagerFields.Instance.currentInteriorType; // Update this line
}

public void SetCurrentInteriorType(string interiorType)
{
    Debug.Log("Setting current interior type to: " + interiorType); // Debug statement
    ContextStateManagerFields.Instance.currentInteriorType = interiorType; // Update this line
    ContextStateManagerEvents.Instance.InvokeOnInteriorTypeChanged(ContextStateManagerFields.Instance.currentInteriorType); // Update this line
}

public string GetCurrentManMadeType()
{
    return ContextStateManagerFields.Instance.currentManMadeType; // Update this line
}


public void SetCurrentManMadeType(string manMadeType)
{
    Debug.Log("Setting current man made type to: " + manMadeType); // Debug statement
    ContextStateManagerFields.Instance.currentManMadeType = manMadeType; // Update this line
    ContextStateManagerEvents.Instance.InvokeOnManMadeTypeChanged(ContextStateManagerFields.Instance.currentManMadeType); // Update this line
}

public string GetCurrentUWType()
{
    return ContextStateManagerFields.Instance.currentUWType; // Update this line
}

public void SetCurrentUWType(string uwtype)
{
    Debug.Log("Setting current uw type to: " + uwtype); // Debug statement
    ContextStateManagerFields.Instance.currentUWType = uwtype; // Update this line
    ContextStateManagerEvents.Instance.InvokeOnUWTypeChanged(ContextStateManagerFields.Instance.currentUWType); // Update this line
}

public string GetCurrentEmoType()
{
    return ContextStateManagerFields.Instance.currentEmoType; // Update this line
}

public string GetCurrentBattleType()
{
    return ContextStateManagerFields.Instance.currentBattleType; // Update this line
}


public void SetCurrentBattleType(string battleType)
{
    Debug.Log("Setting current battle type to: " + battleType); // Debug statement
    ContextStateManagerFields.Instance.currentBattleType = battleType; // Update this line
    ContextStateManagerEvents.Instance.InvokeOnBattleTypeChanged(ContextStateManagerFields.Instance.currentBattleType); // Update this line
}

public string GetCurrentBiomeType()
{
    return ContextStateManagerFields.Instance.currentBiomeType; // Update this line
}

public void SetCurrentBiomeType(string biomeType)
{
    Debug.Log("Setting current Biome type to: " + biomeType); // Debug statement
    ContextStateManagerFields.Instance.currentBiomeType = biomeType; // Update this line
    ContextStateManagerEvents.Instance.InvokeOnBiomeTypeChanged(ContextStateManagerFields.Instance.currentBiomeType); // Update this line
}

public string GetCurrentTransportationType()
{
    return ContextStateManagerFields.Instance.currentTransportationType; // Update this line
}

public void SetCurrentTransportationType(string transportationType)
{
    Debug.Log("Setting current transportation type to: " + transportationType); // Debug statement
    ContextStateManagerFields.Instance.currentTransportationType = transportationType; // Update this line
    ContextStateManagerEvents.Instance.InvokeOnTransportationTypeChanged(ContextStateManagerFields.Instance.currentTransportationType); // Update this line
}

public void SetCurrentEmoType(string emoType)
{
    Debug.Log("Setting current emo type to: " + emoType); // Debug statement
    ContextStateManagerFields.Instance.currentEmoType = emoType; // Update this line
    ContextStateManagerEvents.Instance.InvokeOnEmoTypeChanged(ContextStateManagerFields.Instance.currentEmoType); // Update this line
}

}
