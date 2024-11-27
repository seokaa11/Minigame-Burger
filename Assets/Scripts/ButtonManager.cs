using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
        

    public void GameStart()
    {
        Debug.Log("Game Start");
        Time.timeScale = 0;
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
        Debug.Log("Click-Setting");
    }
    public void LoadHowToPlay()
    {
        Debug.Log("Click-HowToPlay");
    } 
    
}
