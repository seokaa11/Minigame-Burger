using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerScore : MonoBehaviour
{
    public float takenTime;



    void EvaluateBurger()
    {
        if (takenTime <= 1)
        {
            GameManager.instance.score = 100;
            Debug.Log("… 감사합니다! 수고하세요!");
        }
        else if(takenTime < 5)
        {
            GameManager.instance.score = 10;
            Debug.Log("헙..! 너무 완벽해요! 감사합니다^-^");
        }
        else if(takenTime < 10)
        {
            GameManager.instance.score = 5;
            Debug.Log("감사합니다! 맛있게 먹을게요!");
        }
        else
        {
            GameManager.instance.score = 1;
            Debug.Log("조금 오래 걸렸네요ㅎㅎ.. 감사합니다..!");
        }
    }
}
