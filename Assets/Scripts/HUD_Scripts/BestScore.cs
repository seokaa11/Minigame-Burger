using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BestScore : MonoBehaviour
{
    int bestScore;
    TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        bestScoreText = GetComponent<TextMeshProUGUI>();
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
    }

    private void Start()
    {
        bestScoreText.text = "최고 점수 " + bestScore;
    }
}
