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
    public bool isGameOver = false; //게임이 끝났음 을 알림.
    public bool isPaused = false;   // 게임이 멈춰있는가? 
    [SerializeField] Texture2D cursor;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_MAIN);
    }
    private void Update()
    {
        if (isPaused) { return; }

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
        if (!isGameOver)
        {
            int bestScore = PlayerPrefs.GetInt("bestScore", 0);
            if (bestScore < score)
            {
                PlayerPrefs.SetInt("bestScore", score);
            }
            isGameOver = true;            
        }
    }

    //게임 재시작
    public void GameRestart()
    {



    }

    //게임 설정 초기화
    void Init()
    {

    }
}
