using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset; // ���콺 Ŭ�� ��ġ�� ������Ʈ �߽��� ���̸� ������ ����
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������ ������Ʈ�� ������ ����
    private bool isDragging = false; // �巡�� ������ ����
    public bool isDraggable = true; // �巡�� ���� ����
    private bool isInDropZone = false; // ��� ������ �ִ��� ����
    public Vector3 originalPosition; // ���� ��ġ�� ������ ����
    public Vector3 originalScale; // ���� �������� ������ ����

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        // ���� �� ������Ʈ�� �����ϰ� ����
        SetTransparency(0f);
    }
    private void SetTransparency(float alpha) // ���� ���� �Լ�
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    void OnMouseDown()
    {
        if (isDraggable)
        {
            // ���콺�� Ŭ���� ��ġ�� ������Ʈ �߽��� ���̸� ���
            offset = transform.position - GetMouseWorldPosition();
            originalPosition = transform.position; // �巡�� ���� �� ���� ��ġ ����
            originalScale = transform.localScale; // �巡�� ���� �� ���� ������ ����
            isDragging = true;
            SetTransparency(1f);
        }
    }

    void OnMouseDrag() // ���콺 �巡�� �� ȣ��Ǵ� �Լ�
    {
        if (isDragging && isDraggable)
        {
            // ���콺 ��ġ�� ���� ������Ʈ�� �̵�
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private void OnMouseUp() // ���콺�� ������ �� ȣ��Ǵ� �Լ�
    {
        if (isDragging && isDraggable)
        {
            isDragging = false;
            if (isInDropZone)
            {
                Droppable droppable = FindObjectOfType<Droppable>();
                if (droppable != null)
                {
                    droppable.OnDrop(this);
                }
            }
            else
            {
                transform.position = originalPosition;  // ��� ������ ���� �ʴٸ� ���� ��ġ�� �ǵ��ư�
                SetTransparency(0f);    // ������ �ٽ� 0���� ����   
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ (2D��)
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z; // z ���� ī�޶���� �Ÿ�
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DropArea"))
        {
            isInDropZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DropArea"))
        {
            isInDropZone = false;
        }
    }
}
