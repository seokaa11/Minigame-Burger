using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TextSize",menuName ="Setting/TextSize")]
public class TextSizeSO : ScriptableObject
{
    [SerializeField] float textSize;
    public float GetTextSize()
    {
        return textSize;    
    }
    
}
