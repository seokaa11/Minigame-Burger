using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        text.text = "���ھ� " + GameManager.instance.score.ToString();// ���� ǥ�� , �ڷ�ƾ���� �ذ�?
    }
}
