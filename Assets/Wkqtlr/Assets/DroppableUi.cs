using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    public string dropType = "default"; // 드롭 영역 타입 (예: "trash", "burgerOutput", "materials")
    private Image image;
    private RectTransform rect;

    private List<GameObject> stackedIngredients = new List<GameObject>(); // 쌓인 재료 리스트
    public float stackHeight = 30f; // 재료 쌓이는 높이 간격

    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DraggableUI draggable = eventData.pointerDrag.GetComponent<DraggableUI>();
            if (draggable != null)
            {
                // 쓰레기통 처리
                if (dropType == "trash")
                {
                    Destroy(eventData.pointerDrag);
                    return;
                }

                // 햄버거 제출 처리
                if (dropType == "burgerOutput")
                {
                    if (stackedIngredients.Count > 0)
                    {
                        ClearBurgerStack();
                    }
                    return;
                }

                // 재료칸으로 재진입 금지
                if (dropType == "materials" && draggable.isDropped)
                {
                    return;
                }

                // 이미 조합대에 있는 재료인지 확인
                if (stackedIngredients.Contains(eventData.pointerDrag))
                {
                    return; // 아무 동작도 하지 않음
                }

                // 기본 드롭 영역 처리 (재료 쌓기)
                draggable.allowReentry = false;
                draggable.isDropped = true;

                // 드래그된 오브젝트를 현재 드롭 영역의 자식으로 설정
                eventData.pointerDrag.transform.SetParent(transform);

                // 스택 관리: 드롭된 오브젝트를 스택에 추가
                stackedIngredients.Add(eventData.pointerDrag);

                // 위치 계산: 새 재료가 스택 위로 쌓이도록 설정
                Vector3 newPosition = rect.position;
                newPosition.y += stackedIngredients.Count * stackHeight;
                eventData.pointerDrag.GetComponent<RectTransform>().position = newPosition;
            }
        }
    }

    private void ClearBurgerStack()
    {
        // 햄버거 스택 초기화
        foreach (GameObject ingredient in stackedIngredients)
        {
            Destroy(ingredient); // 재료 삭제
        }
        stackedIngredients.Clear(); // 리스트 초기화
    }
}
