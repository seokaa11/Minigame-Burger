using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoint : MonoBehaviour
{
    public Image[] healthPoints;

    private void Update()
    {
        HealthFiller();
    }


    IEnumerator ass()
    {
        yield return null;
    }

    // Hp 칸을 조절하는 함수입니다. Health는 최대 5, 0.5 단위 조절
    void HealthFiller() 
    {
        for(int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].enabled = DisplayHealthPoint(GameManager.instance.health,i);
        }
    }

    // Health 값에 따라 Hp 이미지 on/off 조절하는 함수입니다.
    bool DisplayHealthPoint(float _health, int pointNumber) 
    {
        return pointNumber <= (_health * 2) -1; 
    }
}
