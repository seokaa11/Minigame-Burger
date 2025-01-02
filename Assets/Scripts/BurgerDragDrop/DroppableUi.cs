using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    public string dropType = "default"; // 드롭 영역 타입
    private Image image;
    private RectTransform rect;

    private List<GameObject> stackedIngredients = new List<GameObject>(); // 쌓인 재료 리스트
    float Height = 7; // 재료 쌓이는 높이 간격

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
            Animator animator = eventData.pointerDrag.GetComponent<Animator>();
            string itemName = eventData.pointerDrag.name; // 드롭된 아이템 이름

            if (animator != null)
            {
                switch (itemName)
                {
                    case "Bread":
                        animator.SetTrigger("PlayBread2");
                        eventData.pointerDrag.transform.localScale = new Vector3(8f, 1.5f, 1.0f); // Bread 크기 변경
                        break;

                    case "Meat":
                        animator.SetTrigger("PlayMeat2");
                        eventData.pointerDrag.transform.localScale = new Vector3(7f, 1.3f, 1.0f); // Meat 크기 변경
                        break;

                    case "Cheese":
                        animator.SetTrigger("PlayCheese2");
                        eventData.pointerDrag.transform.localScale = new Vector3(7f, 1.4f, 1.0f); // Cheese 크기 변경
                        break;
                    case "Tomato":
                        animator.SetTrigger("PlayTomato2");
                        eventData.pointerDrag.transform.localScale = new Vector3(7f, 1.2f, 1.0f); // Tomato 크기 변경
                        break;

                    case "Pickle":
                        animator.SetTrigger("PlayPickle2");
                        eventData.pointerDrag.transform.localScale = new Vector3(6f, 1.2f, 1.0f); // Pickle 크기 변경
                        break;
                    case "Lettuce":
                        animator.SetTrigger("PlayLettuce2");
                        eventData.pointerDrag.transform.localScale = new Vector3(7f, 1.2f, 1.0f); // Lettuse 크기 변경
                        break;
                    
                    default:
                        Debug.LogWarning($"No animation or scale configuration for {itemName}");
                        break;
                }
            }
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

                // 이미 조합대에 있는 재료인지 확인
                if (stackedIngredients.Contains(eventData.pointerDrag))
                {
                    return;
                }

                // 기본 드롭 영역 처리 (재료 쌓기)
                draggable.allowReentry = false;
                draggable.isDropped = true;

                // 드래그된 오브젝트를 현재 드롭 영역의 자식으로 설정
                eventData.pointerDrag.transform.SetParent(transform);

                // 스택 관리: 드롭된 오브젝트를 스택에 추가
                stackedIngredients.Add(eventData.pointerDrag);

                // 위치 계산: 새 재료가 스택 위로 쌓이도록 설정
                UpdateStackedPositions();
            }
        }
    }


    private void UpdateStackedPositions()
    {
        for (int i = 0; i < stackedIngredients.Count; i++)
        {
            RectTransform ingredientRect = stackedIngredients[i].GetComponent<RectTransform>();
            Vector3 newPosition = rect.position; // 부모(조합대)의 기준 위치
            newPosition.y += i * Height; // 스택의 높이 계산
            ingredientRect.position = newPosition;
            Debug.Log(newPosition);
        }
    }

    private void ClearBurgerStack()
    {
        foreach (GameObject ingredient in stackedIngredients)
        {
            Destroy(ingredient); // 재료 삭제
        }
        stackedIngredients.Clear(); // 리스트 초기화
    }
}