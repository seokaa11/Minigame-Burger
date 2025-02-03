using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingCanvas : MonoBehaviour
{
    public static SettingCanvas instance;
    void Awake()
    {
        if(instance == null)
        {
            instance= this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider bgmSlider;
    
    const string bgmVolume = "BGMVolume";
    const string sfxVolume = "SFXVolume";
   
    void OnEnable()
    {        
        //저장된 볼륨. 기본값 0.5
        sfxSlider.value = PlayerPrefs.GetFloat(sfxVolume, 0.5f);
        bgmSlider.value = PlayerPrefs.GetFloat(bgmVolume, 0.5f);        
    }
    void OnDisable()
    {
        PlayerPrefs.SetFloat(sfxVolume, sfxSlider.value);
        PlayerPrefs.SetFloat(bgmVolume, bgmSlider.value);
        
    }
    public void Close()
    {
        ButtonManager.instance.Close(gameObject);
    }    
    
}
