using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasController : MonoBehaviour
{
    public void GameStart()
    {
        ButtonManager.instance.GameStart();
    }
    public void LoadHelp()
    {
        ButtonManager.instance.LoadHelp();
    }
    public void LoadSetting()
    {
        ButtonManager.instance.LoadSetting();
    }
    public void ExitGame()
    {
        ButtonManager.instance.GameExit();
    }
}
