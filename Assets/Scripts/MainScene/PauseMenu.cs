using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void BackToGame()
    {
        ButtonManager.instance.GameResume();
    }
    public void LoadSetting()
    {
        ButtonManager.instance.LoadSetting();
    }
    public void ReStartGame()
    {
        ButtonManager.instance.GameStart();
    }
    public void ToMain()
    {
        ButtonManager.instance.ToMain();
    }
}
