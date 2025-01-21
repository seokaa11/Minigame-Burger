using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class SetText : MonoBehaviour
{
    public Button[] buttons;
    TextMeshProUGUI[] texts;
    // Start is called before the first frame update
    void Start()
    {
        texts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickTextSize()
    {
        TextSizeSO textSize =EventSystem.current.currentSelectedGameObject.GetComponent<TextSizeSelect>().TextSizeSO;
        GetTextSizeSO(textSize);
    }
    void GetTextSizeSO(TextSizeSO textSize)
    {          
            
        
        foreach(TextMeshProUGUI text in texts)
        {
            if (text.transform.parent != null && text.transform.parent.GetComponent<TextSizeSelect>())
            {
                continue; 
            }            

            text.fontSize= textSize.GetTextSize();
        }
    }
}
