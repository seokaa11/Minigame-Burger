using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;                   // ���� ����
    public float health;                // Player ���
    public float takenTime;             // ���� ���� �ҿ�ð�
    public string dialog;               // ���� ����� ��ȭ��
    [SerializeField] bool isGameOver;           // ���ӿ����Ǿ�? 

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

    //���� ������ �۵�
    void GameOver()
    {
        isGameOver = false;
    }

    //���� �����
    public void GameRestart()
    {
        SceneManager.LoadScene(1);//�ӽ� 1
        //�ٽ��ϱ�
    }

    //���� ���� �ʱ�ȭ
    void Init()
    {

    }
}
