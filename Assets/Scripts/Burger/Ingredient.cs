using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Ingredient : MonoBehaviour, IPointerClickHandler
{
    public int amount; // 고유 속성

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        // 복제 동작
        GameObject clonedObject = Instantiate(gameObject, transform.parent); // 부모는 동일
        clonedObject.transform.localScale = transform.localScale; // 스케일 유지
        clonedObject.transform.position = transform.position + new Vector3(50, 0, 0); // 위치 약간 조정

        // 컴포넌트 복사 (DraggableUI 포함)
        CopyComponents(this, clonedObject);

        Debug.Log($"Cloned {gameObject.name} with amount: {amount}");
    }

    // 컴포넌트 복사 메서드
    private void CopyComponents(Ingredient original, GameObject clone)
    {
        Ingredient clonedIngredient = clone.GetComponent<Ingredient>();
        if (clonedIngredient != null)
        {
            clonedIngredient.amount = original.amount; // 수량 복사
        }

        // DraggableUI 컴포넌트 복사
        DraggableUI originalDraggable = original.GetComponent<DraggableUI>();
        if (originalDraggable != null)
        {
            DraggableUI clonedDraggable = clone.AddComponent<DraggableUI>();
            clonedDraggable.isDropped = originalDraggable.isDropped;
            clonedDraggable.allowReentry = originalDraggable.allowReentry;
        }
    }
}

