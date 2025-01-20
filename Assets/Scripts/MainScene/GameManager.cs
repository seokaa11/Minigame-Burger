using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;                   // 게임 점수
    public float health;                // Player 목숨
    public float takenTime;             // 버거 제작 소요시간
    public string dialog;               // 버거 제출시 대화문
    [SerializeField] bool isGameOver;           // 게임오버되었? 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    private void Update()
    {        

        takenTime += Time.deltaTime;
        if (health <= 0)
        {
            GameOver();
        }
    }

    public bool IsLive
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

    //게임 오버시 작동
    void GameOver()
    {
        isGameOver = false;
    }

    //게임 재시작
    public void GameRestart()
    {
        SceneManager.LoadScene(1);//임시 1
        //다시하기
    }

    //게임 설정 초기화
    void Init()
    {

    }
}
