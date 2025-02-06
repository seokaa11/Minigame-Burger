using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SetText : MonoBehaviour
{
    public Button[] buttons; // 폰트 변경 버튼. 작게, 중간, 크게
    TextMeshProUGUI[] texts;
    Dictionary<TextMeshProUGUI, float> originalFontSize;
    float currentSize = 1f;
    int selectedIndex;
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        originalFontSize = new Dictionary<TextMeshProUGUI, float>();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        print("씬");
        //새로운 씬 로딩 시 텍스트 딕셔너리 초기화
        originalFontSize.Clear();
        // 씬 로드 후 일정 시간 뒤에 StoreText 호출
        StartCoroutine(DelayedStoreText(scene, mode));
    }

    IEnumerator DelayedStoreText(Scene scene, LoadSceneMode mode)
    {       
        yield return new WaitForSeconds(0.1f); // 씬 로딩 완료 후 짧게 대기
        StoreText(scene, mode); // 씬 로드가 끝나면 StoreText 호출
        ApplyFontSize(currentSize);
        CheckSelectedButton(currentSize);
    }
    
    void StoreText(Scene scene, LoadSceneMode mode)//기본 폰트 사이즈 딕셔너리에 저장. 게임 종료시 사용됨.
    {
        texts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in texts)
        {
            if (text == null || originalFontSize.ContainsKey(text))
            {
                continue;
            }
            originalFontSize[text] = text.fontSize; // 원본 폰트 크기 저장
        }
    }
    
    
    public void ClickTextSize(Button btn)//버튼 클릭 함수
    {
        // 클릭한 버튼의 폰트 사이즈 배율 가져오기
        float textSize = btn.GetComponent<TextSizeSelect>().TextSizeSO.GetTextSize();

        currentSize = textSize;
        //사이즈 적용
        ApplyFontSize(currentSize);

        // 버튼 상태 갱신
        CheckSelectedButton(currentSize);
    }
    
    int CheckPressedButton(float currentSize)//적용된 폰트사이즈
    {
        if (currentSize == 0.75f) return 0;
        else if (currentSize == 1f) return 1;
        else return 2;
    }
   
    void CheckSelectedButton(float textSize) //적용된 폰트사이즈 클릭 방지
    {
        selectedIndex = CheckPressedButton(textSize);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = (i != selectedIndex);
        }
    }

    void ApplyFontSize(float textSize)//사이즈 변경 함수
    {
        foreach (KeyValuePair<TextMeshProUGUI, float> original in originalFontSize)
        {
            TextMeshProUGUI text = original.Key;
            float size = original.Value;

            if (text.transform.parent != null && text.transform.parent.GetComponent<TextSizeSelect>())
            {
                continue;
            }
            text.fontSize = size * textSize; 
        }
    }

    void SetOriginalTextSize()
    {
        //print("사이즈 초기화");
        foreach (TextMeshProUGUI text in texts)
        {
            text.fontSize = originalFontSize[text];
        }
    }
    void OnDisable()
    {
        SetOriginalTextSize();
    }
}
