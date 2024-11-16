using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public int score;  // 게임 점수
    public int health; // Player 목숨

    bool isLive;   // 게임이 멈춰있는가? 


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


    //게임 시작시 설정
    public void GameStart()
    {
        Time.timeScale = 0;
        isLive = true;
    }

    // 게임 종료시 설정
    public void GameExit()
    {
        Application.Quit();
    }



    //게임 설정 초기화
    void init()
    {

    }
}
