using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    Image image;

    [SerializeField] float duration = 5.0f; // FadeOut 기간
    [SerializeField] float time = 0; // 경과 시간
    [SerializeField] float alpha =1; // 알파값

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
        yield return new WaitForSeconds(2.0f);
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
