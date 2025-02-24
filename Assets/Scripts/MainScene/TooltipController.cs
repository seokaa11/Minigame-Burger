using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tooltipText;
    public Image background;

    void Start()
    {
        // 시작할 때 설명 텍스트를 숨깁니다.
        tooltipText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }

    public void ShowTooltip()
    {
        // 마우스가 물체 위에 올라갔을 때 설명 텍스트를 표시합니다.
        tooltipText.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        // 마우스가 물체에서 떨어졌을 때 설명 텍스트를 숨깁니다.
        tooltipText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }
}