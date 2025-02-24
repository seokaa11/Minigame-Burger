using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score_Ui : MonoBehaviour
{
    TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        HUD.Submit += UpdateScore;
    }
    void Update()
    {
        UpdateScore();
    }
    void UpdateScore()
    {
        text.text = GameManager.instance.score.ToString();// 점수 표시

    }
}
