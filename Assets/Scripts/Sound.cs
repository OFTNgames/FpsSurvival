using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound  
{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup mixer;
    
    [Range(0f,1.5f)]
    public float volume;
    
    [Range(0.1f,3f)]
    public float pitch;

    [Range(0f,1f)]
    public float spatialBlend;

    [Range(0,256)]
    public int priority;

    [Range(0f, 1.1f)]
    public float reverbZoneMix;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
