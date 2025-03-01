using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TrashCan : MonoBehaviour
{
    public Droppable dropArea; // DropArea ��ũ��Ʈ�� ���� ������Ʈ�� ����

    void OnMouseDown()
    {
        if (GameManager.instance.IsLive) { return; }

        if (dropArea != null)
        {
            dropArea.ClearAllDroppedItems();
        }
    }
}
