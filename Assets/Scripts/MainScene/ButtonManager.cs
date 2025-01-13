using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject settingWindow;  
    [SerializeField] GameObject helpWindow;


    public void GameStart()
    {
        Debug.Log("Game Start");
<<<<<<< Updated upstream
        Time.timeScale = 0;
=======
        Time.timeScale = 1;
>>>>>>> Stashed changes
        GameManager.instance.IsLive = true;
        SceneManager.LoadScene(1);
    }
    public void GameExit()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }

    public void LoadSetting()
    {
        settingWindow.SetActive(true);        
    }
    public void LoadHelp()
    {
        helpWindow.SetActive(true);
    } 
    
}
