using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // TextMeshPro 네임스페이스 추가

public class GameOverPanel : MonoBehaviour
{
    Image image;
    TextMeshProUGUI scoreText; // TextMeshProUGUI로 변경
    TextMeshProUGUI gameOver;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject [] EndGameScene;
    
    [SerializeField] float duration = 5.0f; // FadeOut 기간
    [SerializeField] float time = 0; // 경과 시간
    [SerializeField] float alpha = 1; // 알파값
    bool gameoverTextEnable=false;
    private void Awake()
    {
        image = GetComponent<Image>();
        gameOver = gameOverText.GetComponent<TextMeshProUGUI>();
        StartCoroutine(OnFadeOut());
        GameManager.endGame += OnGameOver;
    }
    public void GameOverTextDisable(bool b)
    {
        gameoverTextEnable = !b;
        gameOverText.SetActive(gameoverTextEnable);
    }
    IEnumerator OnFadeOut()
    {
        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_GAMEOVER);
        transform.GetChild(0).gameObject.SetActive(gameoverTextEnable);
        while (time <= duration)
        {
            time += Time.deltaTime;
            alpha = Mathf.Clamp01(time / duration);
            image.color = new Color(0, 0, 0, alpha);
            gameOver.color = new Color(255, 255, 255,alpha);
            yield return null;
        }
        image.color = new Color(0, 0, 0, 1);
        
        yield return new WaitForSeconds(2.0f);
        image.color = new Color(0, 0, 0, 0);
        transform.GetChild(0).gameObject.SetActive(false);
        if (GameManager.instance.score <= 10)
        {
            Instantiate(EndGameScene[0]);
        }else if(GameManager.instance.score > 10 && GameManager.instance.score <= 70)
        {
            Instantiate(EndGameScene[1]);
        }
        else
        {
            Instantiate(EndGameScene[2]);
        }
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
