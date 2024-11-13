using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    Slider HP_bar;     
    GameObject timer;
    [SerializeField] float waitTime;

    void Start()
    {
        timer = GameObject.Find("Timer_Text");
        HP_bar = GetComponent<Slider>();
    }

    void Update()
    {
        waitTime -= Time.deltaTime;
        if(waitTime <= 0)
        {
            HP_bar.value -= (float)0.005 * Time.deltaTime;
        }
    }
}
