using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//테스트용 타이머

public class Timer : MonoBehaviour
{
    public Text text;
    int minute;
    int second;
    [SerializeField] public float curTime;
    [SerializeField] float time;

     void Update()
    {
        curTime = time;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            minute = (int)curTime / 60;
            second = (int)curTime % 60;
            text.text = minute.ToString("00") + " : " + second.ToString("00");
            if (curTime <= 0)
            {
                Debug.Log("Game Over");
                curTime = 0;
            }

        }
    }
}
