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
    public Button[] buttons; // ��Ʈ ���� ��ư. �۰�, �߰�, ũ��
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
        //���ο� �� �ε� �� ���� �ؽ�Ʈ ��ųʸ� �ʱ�ȭ
        originalFontSize.Clear();
        // �� �ε� �� ���� �ð� �ڿ� StoreText ȣ��
        StartCoroutine(DelayedStoreText(scene, mode));
    }

    IEnumerator DelayedStoreText(Scene scene, LoadSceneMode mode)
    {
        // �� �ε� �Ϸ� �� ª�� ���
        yield return new WaitForSeconds(0.1f);
        StoreText(scene, mode); // �� �ε尡 ������ StoreText ȣ��
        ApplyFontSize(currentSize);
        CheckSelectedButton(currentSize);
    }      

    void StoreText(Scene scene, LoadSceneMode mode)
    {        
        texts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in texts)
        {
            if (text == null || originalFontSize.ContainsKey(text))
            {
                continue;
            }
            originalFontSize[text] = text.fontSize; // ���� ��Ʈ ũ�� ����
        }        
    }

    int CheckPressedButton(float currentSize)
    {
        if (currentSize == 0.75f) return 0;
        else if (currentSize == 1f) return 1;
        else return 2;
    }

    public void ClickTextSize(Button btn)
    {
        // Ŭ���� ��ư�� ��Ʈ ������ ���� ��������
        float textSize = btn.GetComponent<TextSizeSelect>().TextSizeSO.GetTextSize();

        currentSize = textSize;
        //������ ����
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
