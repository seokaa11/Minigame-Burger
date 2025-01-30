using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SetText : MonoBehaviour
{
    public Button[] buttons; // ��Ʈ ���� ��ư. �۰�, �߰�, ũ��
    TextMeshProUGUI[] texts;
    Dictionary<TextMeshProUGUI, float> originalFontSize;
    float currentSize;
    const string fontSizePrefs = "FontSize";
    int selectedIndex;

    void Start()
    {
        originalFontSize = new Dictionary<TextMeshProUGUI, float>();
        StoreText();

        // ����� ��Ʈ ũ�� ���� �ҷ����� (�⺻�� 1.0)
        currentSize = PlayerPrefs.GetFloat(fontSizePrefs, 1f);

        // ���� ��Ʈ ũ�� �������� ���� ����
        ApplyFontSize(currentSize);

        // ���õ� ��ư ��Ȱ��ȭ
        CheckSelectedButton(currentSize);
    }

    void StoreText()
    {
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

    void OnDisable()
    {
        PlayerPrefs.SetFloat(fontSizePrefs, currentSize);
        PlayerPrefs.Save(); // ���� ����� ȣ�� (����)
    }
}
