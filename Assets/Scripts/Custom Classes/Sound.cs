using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public bool loop;
    public bool interrupt;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume = 1f;
    [Range(0.1f,3f)]
    public float pitch = 1f;

    public float pitchVariance = 0;
    public int pitchStep = 0;
    public float pitchRandomizerChance = 0;

    [HideInInspector]
    public AudioSource source;
}
