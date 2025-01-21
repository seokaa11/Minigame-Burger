using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingCanvas : MonoBehaviour
{
    public void Close()
    {
        ButtonManager.instance.Close(gameObject);
    }
    
    
}
