using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleCanvas : MonoBehaviour
{
    public static RuleCanvas instance;
    public GameObject ruleBorder;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {

    }

    public void Close()
    {
        ButtonManager.instance.Close(ruleBorder);
    }
}