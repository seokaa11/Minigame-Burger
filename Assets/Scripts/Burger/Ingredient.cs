using UnityEngine.EventSystems;
using UnityEngine;

public abstract class Ingredient : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int amount; // 고유 속성

    private GameObject clonedObject; // 복제된 오브젝트 참조

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        // 1. 현재 오브젝트 복제
        clonedObject = Instantiate(gameObject, transform.parent); // 부모는 동일
        clonedObject.transform.localScale = transform.localScale; // 스케일 유지
        clonedObject.transform.position = transform.position; // 위치 유지

        // 2. 복제된 오브젝트에 DraggableUI 추가
        DraggableUI draggable = clonedObject.GetComponent<DraggableUI>();
        if (draggable == null)
        {
            draggable = clonedObject.AddComponent<DraggableUI>(); // DraggableUI 없으면 추가
        }

        // 3. 복제된 오브젝트의 DraggableUI 초기화
        draggable.Initialize(); // DraggableUI 초기화

        // 4. 복제된 오브젝트가 드래그 가능한 상태로 설정
        draggable.OnBeginDrag(eventData); // 드래그 동작 시작
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        // 원본은 드래그되지 않음
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        // 원본은 드래그 종료 동작 없음
    }
}
