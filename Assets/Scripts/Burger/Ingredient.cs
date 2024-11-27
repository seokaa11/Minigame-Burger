using UnityEngine.EventSystems;
using UnityEngine;

public abstract class Ingredient : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int amount; // ���� �Ӽ�

    private GameObject clonedObject; // ������ ������Ʈ ����

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        // 1. ���� ������Ʈ ����
        clonedObject = Instantiate(gameObject, transform.parent); // �θ�� ����
        clonedObject.transform.localScale = transform.localScale; // ������ ����
        clonedObject.transform.position = transform.position; // ��ġ ����

        // 2. ������ ������Ʈ�� DraggableUI �߰�
        DraggableUI draggable = clonedObject.GetComponent<DraggableUI>();
        if (draggable == null)
        {
            draggable = clonedObject.AddComponent<DraggableUI>(); // DraggableUI ������ �߰�
        }

        // 3. ������ ������Ʈ�� DraggableUI �ʱ�ȭ
        draggable.Initialize(); // DraggableUI �ʱ�ȭ

        // 4. ������ ������Ʈ�� �巡�� ������ ���·� ����
        draggable.OnBeginDrag(eventData); // �巡�� ���� ����
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        // ������ �巡�׵��� ����
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        // ������ �巡�� ���� ���� ����
    }
}
