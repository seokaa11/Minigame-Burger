using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public Droppable dropArea; // DropArea ��ũ��Ʈ�� ���� ������Ʈ�� ����

    void OnMouseDown()
    {
        Debug.Log("Trash Can Clicked");
        if (dropArea != null)
        {
            dropArea.ClearAllDroppedItems();
        }
    }
}