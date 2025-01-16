using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    Image image;
    float duration = 3.0f; // FadeOut 기간
    float time = 0; // 경과 시간
    float alpha = 0; // 알파값

    private void Start()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
        if (GameManager.instance.isGameOver && GameManager.instance.IsLive)
        {
            StartCoroutine(FadeOut());
        }
    }


    IEnumerator FadeOut()
    {
        GameManager.instance.IsLive = false;
        while (time <= duration)
        {
            time += Time.deltaTime;
            alpha = Mathf.Clamp01(time / duration);
            image.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        image.color = new Color(0, 0, 0, 1);
    }
}
