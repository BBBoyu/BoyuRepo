using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class NoDestroyOnLoad : MonoBehaviour
{
    public AudioMixer audioMixer;
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        AudioSource SFXSound = GetComponent<AudioSource>();
        float SFXVolume = SFXSound.volume;
        if (PauseMenu.GameIsPaused)
        {
            audioMixer.SetFloat("SFXVolume", -80f);
        }
        else
        {
            audioMixer.SetFloat("SFXVolume", SFXVolume);
        }
    }
}
