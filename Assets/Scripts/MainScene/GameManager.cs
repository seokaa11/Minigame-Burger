using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;  // 게임 점수
    public float health; // Player 목숨
    public float takenTime; // 버거 제작 소요시간
    public string dialog; // 버거 제출시 대화문

    [SerializeField]bool isLive;   // 게임이 멈춰있는가? 
    public bool IsLive
    {
        get { return isLive; }
        set { isLive = value; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else { Destroy(gameObject); }
    }

    void Start()
    {

    }

    private void Update()
    {
        takenTime += Time.deltaTime;
        if (health <= 0)
        {
            GameOver();
        }
    }

    //게임 오버시 작동
    void GameOver()
    {
        isLive = false;
        int bestScore = PlayerPrefs.GetInt("bestScore", 0);
        if(bestScore < score)
        {
            PlayerPrefs.SetInt("bestScore", score);
        }
    }

    //게임 재시작
    void GameRestart()
    {

    }

    //게임 설정 초기화
    void init()
    {

    }
}
