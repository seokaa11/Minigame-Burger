using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    public string dropType = "default"; // ��� ���� Ÿ�� (��: "trash", "burgerOutput", "materials")
    private Image image;
    private RectTransform rect;

    private List<GameObject> stackedIngredients = new List<GameObject>(); // ���� ��� ����Ʈ
    public float stackHeight = 30f; // ��� ���̴� ���� ����

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
                // �������� ó��
                if (dropType == "trash")
                {
                    Destroy(eventData.pointerDrag);
                    return;
                }

                // �ܹ��� ���� ó��
                if (dropType == "burgerOutput")
                {
                    if (stackedIngredients.Count > 0)
                    {
                        ClearBurgerStack();
                    }
                    return;
                }

                // ���ĭ���� ������ ����
                if (dropType == "materials" && draggable.isDropped)
                {
                    return;
                }

                // �̹� ���մ뿡 �ִ� ������� Ȯ��
                if (stackedIngredients.Contains(eventData.pointerDrag))
                {
                    return; // �ƹ� ���۵� ���� ����
                }

                // �⺻ ��� ���� ó�� (��� �ױ�)
                draggable.allowReentry = false;
                draggable.isDropped = true;

                // �巡�׵� ������Ʈ�� ���� ��� ������ �ڽ����� ����
                eventData.pointerDrag.transform.SetParent(transform);

                // ���� ����: ��ӵ� ������Ʈ�� ���ÿ� �߰�
                stackedIngredients.Add(eventData.pointerDrag);

                // ��ġ ���: �� ��ᰡ ���� ���� ���̵��� ����
                Vector3 newPosition = rect.position;
                newPosition.y += stackedIngredients.Count * stackHeight;
                eventData.pointerDrag.GetComponent<RectTransform>().position = newPosition;
            }
        }
    }

    private void ClearBurgerStack()
    {
        // �ܹ��� ���� �ʱ�ȭ
        foreach (GameObject ingredient in stackedIngredients)
        {
            Destroy(ingredient); // ��� ����
        }
        stackedIngredients.Clear(); // ����Ʈ �ʱ�ȭ
    }
}
