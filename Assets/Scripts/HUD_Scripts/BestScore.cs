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
        bestScoreText.text = "�ְ� ���� " + bestScore;
    }

    // ���ھ� ������ȭ�� ���� Health ���� ����?

}
