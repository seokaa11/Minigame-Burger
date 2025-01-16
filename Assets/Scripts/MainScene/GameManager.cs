using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;  // ���� ����
    public float health; // Player ���
    public float takenTime; // ���� ���� �ҿ�ð�
    public string dialog; // ���� ����� ��ȭ��
    public bool isGameOver = false; //������ ������ �� �˸�.
    public GameObject GameOverUI;

    [SerializeField]bool isLive;   // ������ �����ִ°�? 
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
        if (!isLive) { return; }
        takenTime += Time.deltaTime;
        if (health <= 0)
        {
            GameOver();
        }
    }

    //���� ������ �۵�
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
            GameOverUI.SetActive(true);
        }
    }

    //���� �����
    void GameRestart()
    {
    }

    //���� ���� �ʱ�ȭ
    void init()
    {

    }
}
