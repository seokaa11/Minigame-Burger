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
    }

    void Update()
    {
        text.text = GameManager.instance.score.ToString();// ���� ǥ�� , �ڷ�ƾ���� �ذ�?
    }
}
