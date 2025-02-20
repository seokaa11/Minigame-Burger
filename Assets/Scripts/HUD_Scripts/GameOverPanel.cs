using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    Image image;
    float duration = 3.0f; // FadeOut 기간
    float time = 0; // 경과 시간
    float alpha = 0; // 알파값

    private void OnEnable()
    {
        image = GetComponent<Image>();
        GameManager.endGame += OnFadeOut;
        GameManager.endGame += OnGameOver;
    }


    void OnFadeOut()
    {
        while (time <= duration)
        {
            time += Time.deltaTime;
            alpha = Mathf.Clamp01(time / duration);
            image.color = new Color(0, 0, 0, alpha);
        }
        image.color = new Color(0, 0, 0, 1);
    }

    void OnGameOver()
    {
        int bestScore = PlayerPrefs.GetInt("bestScore", 0);
        if (bestScore < GameManager.instance.score)
        {
            PlayerPrefs.SetInt("bestScore", GameManager.instance.score);
        }
        GameManager.instance.IsLive = true;
        GameManager.instance.isPaused = true;
    }
}
