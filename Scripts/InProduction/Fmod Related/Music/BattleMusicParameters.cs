using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMusicParameters : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter fmodEmitter; // Set this to the FMOD event emitter in the inspector

    [SerializeField][Range(0f, 7f)]
    private float intensity;

    [SerializeField][Range(-2f, 2f)]
    private float loseWin;

    void Update()
    {
        // Update the values of the parameters
        intensity = Mathf.InverseLerp(-50f, 50f, transform.localPosition.y) * 7f;
        loseWin = Mathf.InverseLerp(-50f, 50f, transform.localPosition.x) * 4.5f - 2.5f;

        fmodEmitter.SetParameter("Intensity", intensity);
        fmodEmitter.SetParameter("Lose-Win", loseWin);
    }
}
