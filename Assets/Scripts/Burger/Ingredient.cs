using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Ingredient : MonoBehaviour, IPointerClickHandler
{
    public int amount; // ���� �Ӽ�

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        // ���� ����
        GameObject clonedObject = Instantiate(gameObject, transform.parent); // �θ�� ����
        clonedObject.transform.localScale = transform.localScale; // ������ ����
        clonedObject.transform.position = transform.position + new Vector3(50, 0, 0); // ��ġ �ణ ����

        // ������Ʈ ���� (DraggableUI ����)
        CopyComponents(this, clonedObject);

        Debug.Log($"Cloned {gameObject.name} with amount: {amount}");
    }

    // ������Ʈ ���� �޼���
    private void CopyComponents(Ingredient original, GameObject clone)
    {
        Ingredient clonedIngredient = clone.GetComponent<Ingredient>();
        if (clonedIngredient != null)
        {
            clonedIngredient.amount = original.amount; // ���� ����
        }

        // DraggableUI ������Ʈ ����
        DraggableUI originalDraggable = original.GetComponent<DraggableUI>();
        if (originalDraggable != null)
        {
            DraggableUI clonedDraggable = clone.AddComponent<DraggableUI>();
            clonedDraggable.isDropped = originalDraggable.isDropped;
            clonedDraggable.allowReentry = originalDraggable.allowReentry;
        }
    }
}

