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
    public Button[] buttons; // ��Ʈ ���� ��ư. �۰�, �߰�, ũ��
    TextMeshProUGUI[] texts;
    Dictionary<TextMeshProUGUI, float> originalFontSize;
    float currentSize=1f;
    int selectedIndex;
    
    void OnEnable()
    {        
        originalFontSize = new Dictionary<TextMeshProUGUI, float>();
        StoreText(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        // ���õ� ��ư ��Ȱ��ȭ
        CheckSelectedButton(currentSize);
    }

    void StoreText(Scene scene,LoadSceneMode mode)
    {
        print(SceneManager.GetActiveScene().name);
        // �� �� ��� �ؽ�Ʈ �ҷ�����, �ʱ� ��Ʈ ũ�� ����
        texts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts)
        {
            if (!originalFontSize.ContainsKey(text))
            {
                originalFontSize[text] = text.fontSize; // ���� ��Ʈ ũ�� ����
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
        // Ŭ���� ��ư�� ��Ʈ ������ ���� ��������
        float textSize = EventSystem.current.currentSelectedGameObject.GetComponent<TextSizeSelect>()
                            .TextSizeSO.GetTextSize();

        currentSize = textSize;

        // ���� ũ�⿡�� ���� ����
        ApplyFontSize(currentSize);

        // ��ư ���� ����
        CheckSelectedButton(currentSize);
    }

    //����� ��Ʈ������ Ŭ�� ����
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

            text.fontSize = size * textSize; // ���� ��Ʈ ũ�⿡ ���� ����
        }
    }
    
    
}
