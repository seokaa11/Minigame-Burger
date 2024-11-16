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
        text.text = "스코어 " + GameManager.instance.score.ToString();// 점수 표시 , 코루틴으로 해결?
    }
}
