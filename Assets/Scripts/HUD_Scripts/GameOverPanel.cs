using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�

public class GameOverPanel : MonoBehaviour
{
    Image image;
    TextMeshProUGUI scoreText; // TextMeshProUGUI�� ����
    [SerializeField] float duration = 5.0f; // FadeOut �Ⱓ
    [SerializeField] float time = 0; // ��� �ð�
    [SerializeField] float alpha = 1; // ���İ�

    private void Awake()
    {
        image = GetComponent<Image>();
        StartCoroutine(OnFadeOut());
        GameManager.endGame += OnGameOver;
    }

    IEnumerator OnFadeOut()
    {
        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_GAMEOVER);
        while (time <= duration)
        {
            time += Time.deltaTime;
            alpha = Mathf.Clamp01(time / duration);
            image.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        image.color = new Color(0, 0, 0, 1);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);

        scoreText = transform.GetChild(1).GetComponent<TextMeshProUGUI>(); // TextMeshProUGUI�� ����
        if (scoreText != null)
        {
            scoreText.text = "Score : " + GameManager.instance.score;
        }
        
        yield return new WaitForSeconds(2.0f);
        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_MAIN);
        SceneManager.LoadScene(0);
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
        GameManager.endGame -= OnGameOver;
    }
}
