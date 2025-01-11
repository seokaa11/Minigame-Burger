using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void ShowDialog()
    {
        text.text = GameManager.instance.dialog;
    }
}
