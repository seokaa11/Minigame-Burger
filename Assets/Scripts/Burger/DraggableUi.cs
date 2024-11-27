using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform canvas;
    private Transform previousParent;
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    public bool isDropped = false; // ��� ���¸� ��Ÿ���� �÷���
    public bool allowReentry = false; // Ư�� ��� ���������� ������ ���

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        canvas = FindObjectOfType<Canvas>().transform; // Canvas Ž��
        rect = GetComponent<RectTransform>(); // RectTransform ����
        canvasGroup = GetComponent<CanvasGroup>(); // CanvasGroup ����

        if (canvasGroup == null)
        {
            // CanvasGroup�� ������ �߰�
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDropped && !allowReentry) return; // ��� �������� Ư�� ������ ������ ������ �巡�� �Ұ�

        previousParent = transform.parent;

        transform.SetParent(canvas); // Canvas�� �ڽ����� ����
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f; // ���� ���� (�巡�� �� �ð��� ȿ��)
        canvasGroup.blocksRaycasts = false; // Raycast�� ��Ȱ��ȭ�Ͽ� �ٸ� UI�� �浹 ����
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDropped && !allowReentry) return; // �巡�� ����
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == canvas)
        {
            transform.SetParent(previousParent); // �θ� ���� ��ġ�� �ǵ���
            rect.position = previousParent.GetComponent<RectTransform>().position; // ���� ��ġ�� ����
        }

        canvasGroup.alpha = 1.0f; // ���� ����
        canvasGroup.blocksRaycasts = true; // Raycast�� �ٽ� Ȱ��ȭ
    }
}
