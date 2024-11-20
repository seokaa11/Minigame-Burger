using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Debug.Log("Game Start");
        Time.timeScale = 0;
        isLive = true;
        SceneManager.LoadScene(1);
        
    }

    // ���� ����� ����
    public void GameExit()
    {
        Debug.Log("Game Quit");

        Application.Quit();
    }



    //���� ���� �ʱ�ȭ
    void init()
    {

    }
}
