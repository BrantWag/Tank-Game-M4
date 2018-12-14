using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
   
    public AudioSource soundSource;
    public AudioClip pressed;

    // set audio source 
    private void Start()
    {
        soundSource = GetComponentInParent<AudioSource>();
    }
   
    // play sound on pressed using event trigger
    public void playPressed()
    {
        soundSource.clip = pressed;
        if (soundSource.isPlaying == true)
        {
            return;
        }
        else
        {
            soundSource.Play();
        }
    }
}
