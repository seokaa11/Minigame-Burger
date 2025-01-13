using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    int bestScore;
    Text bestScoreText;
    private void Awake()
    {
        bestScoreText = GetComponentInChildren<Text>();
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
    }

    private void Start()
    {
        bestScoreText.text = "최고 점수 " + bestScore;
    }

    // 스코어 점수변화를 보고 Health 변동 구현?

}
