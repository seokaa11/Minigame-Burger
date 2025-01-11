using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurgerScore : MonoBehaviour
{
    public Scoredata [] scoredata;


    // 버거 상태에 따라 점수가 주어지고 대화문 출력
    public void EvaluateBurger()
    {
        for (int i = 0; i < scoredata.Length / 2; i++)
        {
            if (GameManager.instance.takenTime > 20) { i = 4; }
            if (scoredata[i].Time >= GameManager.instance.takenTime)
            {
                if (scoredata[i].isPerfectBurger && i != 4) { i += 5; }
                GameManager.instance.score += scoredata[i].score;
                GameManager.instance.health += scoredata[i].health;
                GameManager.instance.dialog = scoredata[i].dialog;
                GameManager.instance.takenTime = 0;
                break;
            }
        }
    }
}
