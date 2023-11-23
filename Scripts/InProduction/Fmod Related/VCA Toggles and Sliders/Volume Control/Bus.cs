using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus: MonoBehaviour {
    
  FMOD.Studio.EventInstance instance;
  public FMOD.Studio.Bus bus;
  

  public FMODUnity.EventReference m_EventPath;
  
  [SerializeField][Range(-80f, 10f)] 
  private float busVolume;
  private float volume;
  
  void Start() 
  {
    instance = FMODUnity.RuntimeManager.CreateInstance(m_EventPath);
    instance.start();
    bus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
  }
  
  void Update() 
  {
    volume = Mathf.Pow(10.0f, busVolume / 20f);
    bus.setVolume(volume);
  }
}