using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    Image image;
    float duration = 3.0f; // FadeOut �Ⱓ
    [SerializeField] float time = 0; // ��� �ð�

    [SerializeField] float alpha =1; // ���İ�

    private void Awake()
    {
        image = GetComponent<Image>();
        GameManager.endGame += ()=>StartCoroutine(OnFadeOut());
        GameManager.endGame += OnGameOver;
    }    

    IEnumerator OnFadeOut()
    {
        while (time <= duration)
        {
            time += Time.deltaTime;
            alpha = Mathf.Clamp01(time / duration);
            image.color = new Color(0, 0, 0, alpha);
            yield return null;
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
    void OnDisable()
    {
        GameManager.endGame -= () => StartCoroutine(OnFadeOut());
        GameManager.endGame -= OnGameOver;
    }
}
