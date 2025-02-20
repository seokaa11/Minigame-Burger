using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public Droppable dropArea; // DropArea 스크립트가 붙은 오브젝트를 참조

    void OnMouseDown()
    {
        Debug.Log("Trash Can Clicked");
        if (dropArea != null)
        {
            dropArea.ClearAllDroppedItems();
        }
    }
}