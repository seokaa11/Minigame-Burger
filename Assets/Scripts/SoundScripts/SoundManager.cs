using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioMixer audioMixer;
    //BGM 종류들
    public enum EBgm
    {
        BGM_MAIN,//로비 브금
        BGM_GAME,//인게임 브금
        BGM_LOWSCORE,//낮은 점수 달성
        BGM_NORMALSCORE,//중간 점수 달성
        BGM_HIGHSCORE//최고 점수 달성
    }

    //SFX 종류들
    public enum ESfx
    {
        SFX_BUTTON,//버튼 마우스오버
        SFX_PUT,//재료 놓을 때
        SFX_SCORE,//점수 올라갈 때
        SFX_PACKAGING,//포장 소리
        SFX_LOSTHEALTH,//하트 사라질 때
        SFX_GAMEOVER//종료음
    }
    
    //audio clip 담을 수 있는 배열
    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] sfxs;
    [SerializeField] float[] sfxVol;
    Dictionary<ESfx,float> sfxVolumes= new Dictionary<ESfx,float>();
    //플레이하는 AudioSource
    [SerializeField] AudioSource audioBgm;
    [SerializeField] AudioSource audioSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //시작시 저장된 volume
        const string bgmVolumeKey = "BGMVolume";
        const string sfxVolumeKey = "SFXVolume";
        float bgmValue = PlayerPrefs.GetFloat(bgmVolumeKey, 0.5f);
        float sfxValue = PlayerPrefs.GetFloat(sfxVolumeKey, 0.5f);
        //value값은 0.0001 ~ 1 사이
        
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmValue) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxValue) * 20);
        foreach(ESfx esfx in System.Enum.GetValues(typeof(ESfx)))
        {
            sfxVolumes[esfx] = sfxVol[(int)esfx];
        }
       
    }
    //BGM 재생
    public void PlayBGM(EBgm bgmIdx,bool looping=true)
    {
        StopBGM();
        audioBgm.clip = bgms[(int)bgmIdx];
        if(looping) audioBgm.Play();
        else audioBgm.PlayOneShot(audioBgm.clip);
    }

    // 현재 재생 중인 배경 음악 정지
    public void StopBGM()
    {
        audioBgm.Stop();
    }

    // SFX 재생
    public void PlaySFX(ESfx esfx)
    {
        audioSfx.PlayOneShot(sfxs[(int)esfx]);
        audioSfx.volume=sfxVolumes[esfx];
    }
}