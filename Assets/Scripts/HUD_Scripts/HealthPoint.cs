using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoint : MonoBehaviour
{
    public GameObject [] healthPoints;
    public Action DecreaseHealth;
    private void Awake()
    {
        HUD.Submit += HealthFiller;
    }

    // Hp 칸을 조절하는 함수입니다. Health는 최대 5, 0.5 단위 조절
    void HealthFiller() 
    {
        for(int i = 0; i < healthPoints.Length; i++)
        {
            // healthPoints[i]가 null인지 확인
            if (healthPoints[i] != null)
            {
                healthPoints[i].SetActive(DisplayHealthPoint(GameManager.instance.health, i));
            }
        }
    }

    // GameManager Health 값에 따라 Hp 이미지 on/off 조절하는 함수입니다.
    bool DisplayHealthPoint(float _health, int pointNumber) 
    {
        return pointNumber <= (_health * 2) -1; 
    }

}
