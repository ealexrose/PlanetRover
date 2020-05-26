using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string name)
    {
        Sound playSound = Array.Find(sounds, sound => sound.name == name);
        if (playSound == null)
        {
            Debug.LogWarning("Sound:" + name + " cannot be found, it either doesn't exist or there is a typo");
            return;
        }
        //set volume
        playSound.source.volume = playSound.volume;
        //set pitch
        float pitchVariance = 0;
        if (UnityEngine.Random.Range(0.0f, .99f) < playSound.pitchRandomizerChance)
        {
            pitchVariance = playSound.pitchVariance * (float)UnityEngine.Random.Range(-playSound.pitchStep, playSound.pitchStep);
        }
        playSound.source.pitch = Mathf.Clamp(playSound.pitch + pitchVariance,0.01f,3f);

        if (playSound.interrupt)
        {
            playSound.source.Play();
        }
        else if (!playSound.source.isPlaying)
        {
            playSound.source.Play();
        }
        
    }
}
