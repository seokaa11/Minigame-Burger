using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void BackToGame()
    {
        GameManager.instance.isPaused=false;
    }
    public void LoadSetting()
    {
        ButtonManager.instance.LoadSetting();
    }
    public void ReStartGame()
    {
        ButtonManager.instance.GameStart();
    }
    public void ExitGame()
    {
        ButtonManager.instance.GameExit();
    }
}
