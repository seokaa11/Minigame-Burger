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
            Debug.Log("�� �����մϴ�! �����ϼ���!");
        }
        else if(takenTime < 5)
        {
            GameManager.instance.score = 10;
            Debug.Log("��..! �ʹ� �Ϻ��ؿ�! �����մϴ�^-^");
        }
        else if(takenTime < 10)
        {
            GameManager.instance.score = 5;
            Debug.Log("�����մϴ�! ���ְ� �����Կ�!");
        }
        else
        {
            GameManager.instance.score = 1;
            Debug.Log("���� ���� �ɷȳ׿䤾��.. �����մϴ�..!");
        }
    }
}
