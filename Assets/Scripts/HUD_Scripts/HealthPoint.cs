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

    // Hp ĭ�� �����ϴ� �Լ��Դϴ�. Health�� �ִ� 5, 0.5 ���� ����
    void HealthFiller() 
    {
        for(int i = 0; i < healthPoints.Length; i++)
        {
            // healthPoints[i]�� null���� Ȯ��
            if (healthPoints[i] != null)
            {
                healthPoints[i].SetActive(DisplayHealthPoint(GameManager.instance.health, i));
            }
        }
    }

    // GameManager Health ���� ���� Hp �̹��� on/off �����ϴ� �Լ��Դϴ�.
    bool DisplayHealthPoint(float _health, int pointNumber) 
    {
        return pointNumber <= (_health * 2) -1; 
    }

}
