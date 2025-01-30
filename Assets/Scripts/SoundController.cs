using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public AudioMixer audioMixer;
    const string bgm = "BGM";
    const string sfx = "SFX";
    
    public void SetBGMVolume(float volume)
    {
        //volume=0.0001 ~ 1
        audioMixer.SetFloat(bgm, Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(sfx, Mathf.Log10(volume) * 20);
    }
}
