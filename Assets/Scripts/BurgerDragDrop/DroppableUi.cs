using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    public string dropType = "default"; // ��� ���� Ÿ��
    private Image image;
    private RectTransform rect;

    private List<GameObject> stackedIngredients = new List<GameObject>(); // ���� ��� ����Ʈ
    float Height = 7; // ��� ���̴� ���� ����

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
            string itemName = eventData.pointerDrag.name; // ��ӵ� ������ �̸�

            if (animator != null)
            {
                switch (itemName)
                {
                    case "Bread":
                        animator.SetTrigger("PlayBread2");
                        eventData.pointerDrag.transform.localScale = new Vector3(8f, 1.5f, 1.0f); // Bread ũ�� ����
                        break;

                    case "Meat":
                        animator.SetTrigger("PlayMeat2");
                        eventData.pointerDrag.transform.localScale = new Vector3(7f, 1.3f, 1.0f); // Meat ũ�� ����
                        break;

                    case "Cheese":
                        animator.SetTrigger("PlayCheese2");
                        eventData.pointerDrag.transform.localScale = new Vector3(7f, 1.4f, 1.0f); // Cheese ũ�� ����
                        break;
                    case "Tomato":
                        animator.SetTrigger("PlayTomato2");
                        eventData.pointerDrag.transform.localScale = new Vector3(7f, 1.2f, 1.0f); // Tomato ũ�� ����
                        break;

                    case "Pickle":
                        animator.SetTrigger("PlayPickle2");
                        eventData.pointerDrag.transform.localScale = new Vector3(6f, 1.2f, 1.0f); // Pickle ũ�� ����
                        break;
                    case "Lettuce":
                        animator.SetTrigger("PlayLettuce2");
                        eventData.pointerDrag.transform.localScale = new Vector3(7f, 1.2f, 1.0f); // Lettuse ũ�� ����
                        break;
                    
                    default:
                        Debug.LogWarning($"No animation or scale configuration for {itemName}");
                        break;
                }
            }
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

                // �̹� ���մ뿡 �ִ� ������� Ȯ��
                if (stackedIngredients.Contains(eventData.pointerDrag))
                {
                    return;
                }

                // �⺻ ��� ���� ó�� (��� �ױ�)
                draggable.allowReentry = false;
                draggable.isDropped = true;

                // �巡�׵� ������Ʈ�� ���� ��� ������ �ڽ����� ����
                eventData.pointerDrag.transform.SetParent(transform);

                // ���� ����: ��ӵ� ������Ʈ�� ���ÿ� �߰�
                stackedIngredients.Add(eventData.pointerDrag);

                // ��ġ ���: �� ��ᰡ ���� ���� ���̵��� ����
                UpdateStackedPositions();
            }
        }
    }


    private void UpdateStackedPositions()
    {
        for (int i = 0; i < stackedIngredients.Count; i++)
        {
            RectTransform ingredientRect = stackedIngredients[i].GetComponent<RectTransform>();
            Vector3 newPosition = rect.position; // �θ�(���մ�)�� ���� ��ġ
            newPosition.y += i * Height; // ������ ���� ���
            ingredientRect.position = newPosition;
            Debug.Log(newPosition);
        }
    }

    private void ClearBurgerStack()
    {
        foreach (GameObject ingredient in stackedIngredients)
        {
            Destroy(ingredient); // ��� ����
        }
        stackedIngredients.Clear(); // ����Ʈ �ʱ�ȭ
    }
}