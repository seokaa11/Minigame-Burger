using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public int score;  // ���� ����
    public int health; // Player ���

    bool isLive;   // ������ �����ִ°�? 


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        
    }


    //���� ���۽� ����
    public void GameStart()
    {
        Time.timeScale = 0;
        isLive = true;
    }

    // ���� ����� ����
    public void GameExit()
    {
        Application.Quit();
    }



    //���� ���� �ʱ�ȭ
    void init()
    {

    }
}
