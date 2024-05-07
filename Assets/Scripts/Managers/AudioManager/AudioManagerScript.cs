using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{

    public static AudioManagerScript Instance { get; private set; }

    public AudioSource musicSource;
    public AudioSource effectSource;

    public float musicVolume;
    public float effectVolume;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        musicSource.volume = musicVolume;
        effectSource.volume = effectVolume;
    }

    public void PlayEffect(AudioClip clip)
    {
        //effectSource.volume = volume;
        effectSource.PlayOneShot(clip);
    }

    public void ChangeMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
    }

    public void ChangeEffectVolume(float volume)
    {
        effectVolume = volume;
        effectSource.volume = effectVolume;
    }

    
}
