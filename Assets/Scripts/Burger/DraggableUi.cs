using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform canvas;
    private Transform previousParent;
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    public bool isDropped = false; // 드롭 상태를 나타내는 플래그
    public bool allowReentry = false; // 특정 드롭 영역에서만 재진입 허용

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        canvas = FindObjectOfType<Canvas>().transform; // Canvas 탐색
        rect = GetComponent<RectTransform>(); // RectTransform 설정
        canvasGroup = GetComponent<CanvasGroup>(); // CanvasGroup 설정

        if (canvasGroup == null)
        {
            // CanvasGroup이 없으면 추가
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDropped && !allowReentry) return; // 드롭 상태지만 특정 조건이 허용되지 않으면 드래그 불가

        previousParent = transform.parent;

        transform.SetParent(canvas); // Canvas의 자식으로 설정
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f; // 투명도 조정 (드래그 중 시각적 효과)
        canvasGroup.blocksRaycasts = false; // Raycast를 비활성화하여 다른 UI와 충돌 방지
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDropped && !allowReentry) return; // 드래그 제한
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == canvas)
        {
            transform.SetParent(previousParent); // 부모를 원래 위치로 되돌림
            rect.position = previousParent.GetComponent<RectTransform>().position; // 원래 위치로 복귀
        }

        canvasGroup.alpha = 1.0f; // 투명도 복구
        canvasGroup.blocksRaycasts = true; // Raycast를 다시 활성화
    }
}
