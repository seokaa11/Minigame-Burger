using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCanvas : MonoBehaviour
{
    public void Close()
    {
        ButtonManager.instance.Close(gameObject);
    }
    public void RePlay()
    {
        GameManager.instance.GameRestart();
    }
    public void Setting()
    {
        ButtonManager.instance.LoadSetting();
    }
    public void Quit()
    {
        ButtonManager.instance.GameExit();
    }
}
