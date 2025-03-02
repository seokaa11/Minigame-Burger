using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // TextMeshPro 네임스페이스 추가

public class GameOverPanel : MonoBehaviour
{
    Image image;
    TextMeshProUGUI scoreText; // TextMeshProUGUI로 변경
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject [] EndGameScene;
    
    [SerializeField] float duration = 5.0f; // FadeOut 기간
    [SerializeField] float time = 0; // 경과 시간
    [SerializeField] float alpha = 1; // 알파값
    bool gameoverTextEnable=false;
    private void Awake()
    {
        image = GetComponent<Image>();
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
        while (time <= duration)
        {
            time += Time.deltaTime;
            alpha = Mathf.Clamp01(time / duration);
            image.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        image.color = new Color(0, 0, 0, 1);
        transform.GetChild(0).gameObject.SetActive(gameoverTextEnable);
        transform.GetChild(1).gameObject.SetActive(true);

        scoreText = transform.GetChild(1).GetComponent<TextMeshProUGUI>(); // TextMeshProUGUI로 변경
        if (scoreText != null)
        {
            scoreText.text = "Score : " + GameManager.instance.score;
        }
        
        yield return new WaitForSeconds(2.0f);
        image.color = new Color(0, 0, 0, 0);
        scoreText.text = "";
        gameOverText.SetActive(false);
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
        yield return new WaitForSeconds(4.0f);
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
