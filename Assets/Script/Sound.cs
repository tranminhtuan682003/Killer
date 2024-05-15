using System;
using UnityEngine;
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioclip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    [HideInInspector]
    public AudioSource soundsource;
}
