using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    Image image;
    float duration = 3.0f; // FadeOut �Ⱓ
    float time = 0; // ��� �ð�
    float alpha = 0; // ���İ�

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
