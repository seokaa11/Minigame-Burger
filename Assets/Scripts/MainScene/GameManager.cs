using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action endGame; // 게임 종료시 실행
    public int score;  // 게임 점수
    public float health = 5; // Player 목숨
    public float takenTime; // 버거 제작 소요시간    
    public string _dialog; // 버거 제출시 대화문
    public bool isPaused = false;   // 게임이 멈춰있는가? 

    [SerializeField] bool isGameOver = false; //게임이 끝났음 을 알림.
    [SerializeField] Texture2D cursor;
    [SerializeField] GameObject GameOverPanel;
    public string Dialog
    {
        get { return _dialog; }
        set
        {
            _dialog = value;
            HUD.Submit();
        }
    }
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
    public bool IsLive
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }
    private void Update()
    {
        if (isPaused) { return; }
        takenTime += Time.deltaTime;
        IsGameOver();
    }

    void IsGameOver()
    {
        if (health <= 0 || IsLive)
        {
            Debug.Log("게임오버");
            isPaused = true;
            GameObject gameOverPanelInstance = Instantiate(GameOverPanel);
            gameOverPanelInstance.SetActive(true);
            endGame();
        }
    }
    public void CallGameOver()
    {
        health = -1;
        IsLive = true;
    }
    //게임 설정 초기화
    public void Init()
    {
        isPaused = false;
        IsLive = false;
        score = 0;
        health = 5;
        takenTime = 0;
    }
}
