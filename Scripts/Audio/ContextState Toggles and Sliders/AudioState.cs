using UnityEngine;

public class AudioState
{
    public float Volume { get; set; }
    public bool IsMuted { get; set; }

    public AudioState(float volume, bool isMuted)
    {
        Volume = volume;
        IsMuted = isMuted;
    }
}
