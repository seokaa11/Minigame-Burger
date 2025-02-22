using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    GameObject settingCanvas;
    GameObject ruleCanvas;


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
        ruleCanvas = FindAnyObjectByType<RuleCanvas>().ruleBorder;
    }
   
    public void GameStart()
    {
        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_GAME);
        Debug.Log("Game Start");
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        GameManager.instance.Init();
    }
    public void LoadSetting()
    {
        if (settingCanvas == null) { return; }
        settingCanvas.SetActive(!settingCanvas.activeSelf);
    }
    
    public void LoadHelp()
    {
        if (ruleCanvas == null) { return; }
        ruleCanvas.SetActive(!ruleCanvas.activeSelf);
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
