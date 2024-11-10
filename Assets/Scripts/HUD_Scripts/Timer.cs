using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text text;
    int minute;
    int second;
    [SerializeField] float curTime;
    [SerializeField] float time;

    private void Awake()
    {
        StartCoroutine(GameTimer());
    }

    IEnumerator GameTimer()
    {
        curTime = time;
        while(curTime > 0)
        {
            curTime -= Time.deltaTime;
            minute = (int) curTime / 60;
            second = (int) curTime % 60;
            text.text = minute.ToString("00") + " : " + second.ToString("00");
            if (curTime <= 0) { 
                Debug.Log("Game Over");
                curTime = 0;

            }
            yield return null;
        }
    }
}
