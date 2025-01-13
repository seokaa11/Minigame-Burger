using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;  // ���� ����
    public float health; // Player ���
    public float takenTime; // ���� ���� �ҿ�ð�
    public string dialog; // ���� ����� ��ȭ��

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
        takenTime += Time.deltaTime;
        if (health <= 0)
        {
            GameOver();
        }
    }

    //���� ������ �۵�
    void GameOver()
    {
        isLive = false;
        int bestScore = PlayerPrefs.GetInt("bestScore", 0);
        if(bestScore < score)
        {
            PlayerPrefs.SetInt("bestScore", score);
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
