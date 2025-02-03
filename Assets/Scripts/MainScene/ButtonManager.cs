using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    [SerializeField] GameObject settingWindow;
   

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
        
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
        settingWindow.SetActive(!settingWindow.activeSelf);
    }
    
    public void LoadHelp()
    {
        //∞‘¿” º≥∏Ì æ¿?
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
}
