using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // Master Mixer
    public AudioMixer Mixer;

    // Set mixer volume to savd volume
    private void Start()
    {
        Mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("musicVolume", 0));
        Mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("sfxVolume", 0));
    }

    //set volume for exposed music volume 
    public void setMusicVolume(float volume)
    {
        Mixer.SetFloat("MusicVolume", volume);
    }

    //set volume for exposed sfx 
    public void setSFXVolume(float volume)
    {
        Mixer.SetFloat("SFXVolume", volume);
    }
}
