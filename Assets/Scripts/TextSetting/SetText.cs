using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SetText : MonoBehaviour
{
    public Button[] buttons; // 폰트 변경 버튼. 작게, 중간, 크게
    TextMeshProUGUI[] texts;
    Dictionary<TextMeshProUGUI, float> originalFontSize;
    float currentSize=1f;
    int selectedIndex;
    
    void OnEnable()
    {        
        originalFontSize = new Dictionary<TextMeshProUGUI, float>();
        StoreText(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        // 선택된 버튼 비활성화
        CheckSelectedButton(currentSize);
    }

    void StoreText(Scene scene,LoadSceneMode mode)
    {
        print(SceneManager.GetActiveScene().name);
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

    //적용된 폰트사이즈 클릭 방지
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
    
    
}
