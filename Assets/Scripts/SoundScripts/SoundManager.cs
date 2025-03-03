using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioMixer audioMixer;
    //BGM ������
    public enum EBgm
    {
        BGM_MAIN,//�κ� ���
        BGM_GAME,//�ΰ��� ���
        BGM_LOWSCORE,//���� ���� �޼�
        BGM_NORMALSCORE,//�߰� ���� �޼�
        BGM_HIGHSCORE//�ְ� ���� �޼�
    }

    //SFX ������
    public enum ESfx
    {
        SFX_BUTTON,//��ư ���콺����
        SFX_PUT,//��� ���� ��
        SFX_SCORE,//���� �ö� ��
        SFX_PACKAGING,//���� �Ҹ�
        SFX_LOSTHEALTH,//��Ʈ ����� ��
        SFX_GAMEOVER//������
    }
    
    //audio clip ���� �� �ִ� �迭
    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] sfxs;
    [SerializeField] float[] sfxVol;
    Dictionary<ESfx,float> sfxVolumes= new Dictionary<ESfx,float>();
    //�÷����ϴ� AudioSource
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
        //���۽� ����� volume
        const string bgmVolumeKey = "BGMVolume";
        const string sfxVolumeKey = "SFXVolume";
        float bgmValue = PlayerPrefs.GetFloat(bgmVolumeKey, 0.5f);
        float sfxValue = PlayerPrefs.GetFloat(sfxVolumeKey, 0.5f);
        //value���� 0.0001 ~ 1 ����
        
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmValue) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxValue) * 20);
        foreach(ESfx esfx in System.Enum.GetValues(typeof(ESfx)))
        {
            sfxVolumes[esfx] = sfxVol[(int)esfx];
        }
       
    }
    //BGM ���
    public void PlayBGM(EBgm bgmIdx,bool looping=true)
    {
        StopBGM();
        audioBgm.clip = bgms[(int)bgmIdx];
        if(looping) audioBgm.Play();
        else audioBgm.PlayOneShot(audioBgm.clip);
    }

    // ���� ��� ���� ��� ���� ����
    public void StopBGM()
    {
        audioBgm.Stop();
    }

    // SFX ���
    public void PlaySFX(ESfx esfx)
    {
        audioSfx.PlayOneShot(sfxs[(int)esfx]);
        audioSfx.volume=sfxVolumes[esfx];
    }
}