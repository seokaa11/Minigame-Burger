using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public int score;  // ���� ����
    public int health; // Player ���

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
    //���� ���� �ʱ�ȭ
    void init()
    {

    }
}
