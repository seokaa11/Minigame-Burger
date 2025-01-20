using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TitleText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public void MouseOverEvent()
    {
        if (text == null) Debug.LogError("텍스트 연결 안 됨");
        text.fontStyle = FontStyles.Bold;
    }
    public void MouseExitEvent()
    {
        if (text == null) Debug.LogError("텍스트 연결 안 됨");
        text.fontStyle = FontStyles.Normal;    }

}
