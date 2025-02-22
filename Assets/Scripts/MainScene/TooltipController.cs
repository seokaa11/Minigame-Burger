using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tooltipText;
    public Image background;

    void Start()
    {
        // ������ �� ���� �ؽ�Ʈ�� ����ϴ�.
        tooltipText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }

    public void ShowTooltip()
    {
        // ���콺�� ��ü ���� �ö��� �� ���� �ؽ�Ʈ�� ǥ���մϴ�.
        tooltipText.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        // ���콺�� ��ü���� �������� �� ���� �ؽ�Ʈ�� ����ϴ�.
        tooltipText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }
}