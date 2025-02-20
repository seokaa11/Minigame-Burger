using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TextBold : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public void MouseOverEvent()
    {
        if (text == null) Debug.LogError("�ؽ�Ʈ ���� �� ��");
        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_BUTTON);
        text.fontStyle = FontStyles.Bold;
    }
    public void MouseExitEvent()
    {
        if (text == null) Debug.LogError("�ؽ�Ʈ ���� �� ��");
        text.fontStyle = FontStyles.Normal;    }

}
