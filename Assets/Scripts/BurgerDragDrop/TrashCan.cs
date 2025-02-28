using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TrashCan : MonoBehaviour
{
    public Droppable dropArea; // DropArea 스크립트가 붙은 오브젝트를 참조

    void OnMouseDown()
    {
        if (GameManager.instance.IsLive) { return; }

        if (dropArea != null)
        {
            dropArea.ClearAllDroppedItems();
        }
    }
}
