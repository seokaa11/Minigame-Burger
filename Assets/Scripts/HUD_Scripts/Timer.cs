using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI text;
    int minute;
    int second;
    [SerializeField] float curTime;
    [SerializeField] float time;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

    }
    void Start()
    {
        StartCoroutine(GameTimer());
    }
    IEnumerator GameTimer()
    {
        curTime = time;
        while(curTime >= 0)
        {
            curTime += Time.deltaTime;
            minute = (int) curTime / 60;
            second = (int) curTime % 60;
            text.text = minute.ToString("00") + " : " + second.ToString("00");
            yield return null;
        }
    }
}
