using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        HUD.Submit += ShowDialog;
    }

    public void ShowDialog()
    {
        text.text = GameManager.instance.Dialog;
    }
}
