using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    GameObject settingCanvas;
   

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
        settingCanvas = FindAnyObjectByType<SettingCanvas>().settingBorder;
    }
    
    public void GameStart()
    {
        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_GAME);
        Debug.Log("Game Start");
        Time.timeScale = 1;
        GameManager.instance.IsLive = true;
        SceneManager.LoadScene(1);
    }
    public void LoadSetting()
    {
        if (settingCanvas == null) { return; }
        settingCanvas.SetActive(!settingCanvas.activeSelf);
    }
    
    public void LoadHelp()
    {
        //게임 설명 씬?
    }

    public void GameExit()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
    public void Close(GameObject obj)
    {
       obj.SetActive(false);
    }

    // 게임 일시정지 버튼을 눌렀을 때
    public void GamePause()
    {
        Time.timeScale = 0;
    }
    public void ToMain()
    {
        SceneManager.LoadScene(0);
        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_MAIN);
    }
    //게임이 다시 재개됐을 때
    public void GameResume()
    {
        Time.timeScale = 1;
    }
}
