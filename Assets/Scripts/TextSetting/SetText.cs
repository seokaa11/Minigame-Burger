using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SetText : MonoBehaviour
{
    public Button[] buttons; // 폰트 변경 버튼. 작게, 중간, 크게
    TextMeshProUGUI[] texts;
    Dictionary<TextMeshProUGUI, float> originalFontSize;
    float currentSize;
    const string fontSizePrefs = "FontSize";
    int selectedIndex;

    void Start()
    {
        originalFontSize = new Dictionary<TextMeshProUGUI, float>();
        StoreText();

        // 저장된 폰트 크기 배율 불러오기 (기본값 1.0)
        currentSize = PlayerPrefs.GetFloat(fontSizePrefs, 1f);

        // 기존 폰트 크기 기준으로 배율 적용
        ApplyFontSize(currentSize);

        // 선택된 버튼 비활성화
        CheckSelectedButton(currentSize);
    }

    void StoreText()
    {
        // 씬 내 모든 텍스트 불러오기, 초기 폰트 크기 저장
        texts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts)
        {
            if (!originalFontSize.ContainsKey(text))
            {
                originalFontSize[text] = text.fontSize; // 원본 폰트 크기 저장
            }
        }
    }

    int CheckPressedButton(float currentSize)
    {
        if (currentSize == 0.75f) return 0;
        else if (currentSize == 1f) return 1;
        else return 2;
    }

    public void ClickTextSize()
    {
        // 클릭한 버튼의 폰트 사이즈 배율 가져오기
        float textSize = EventSystem.current.currentSelectedGameObject.GetComponent<TextSizeSelect>()
                            .TextSizeSO.GetTextSize();

        currentSize = textSize;

        // 원래 크기에서 배율 적용
        ApplyFontSize(currentSize);

        // 버튼 상태 갱신
        CheckSelectedButton(currentSize);
    }

    void CheckSelectedButton(float textSize)
    {
        selectedIndex = CheckPressedButton(textSize);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = (i != selectedIndex);
        }
    }

    void ApplyFontSize(float textSize)
    {
        foreach (KeyValuePair<TextMeshProUGUI, float> original in originalFontSize)
        {
            TextMeshProUGUI text = original.Key;
            float size = original.Value;

            if (text.transform.parent != null && text.transform.parent.GetComponent<TextSizeSelect>())
            {
                continue;
            }

            text.fontSize = size * textSize; // 원래 폰트 크기에 배율 적용
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(fontSizePrefs, currentSize);
        PlayerPrefs.Save(); // 저장 명시적 호출 (권장)
    }
}
