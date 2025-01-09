using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public List<GameObject> originalIngredients; // ���� ��� ������ ����Ʈ
    private Vector3 originalPosition;
    private Vector3 originalScale;

    public static int stackCount = 0;
    public bool isDrag;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private bool isInDropArea = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        originalScale = transform.localScale;
    }

    void Start()
    {
        SetTransparency(0f);
    }

    void Update()
    {
        if (isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f);
        }
    }

    public void Drag()
    {
        if (!isInDropArea)
        {
            isDrag = true;
            rigid.simulated = false; // �巡�� �߿��� ���� �ùķ��̼��� ��Ȱ��ȭ
            SetTransparency(1f); // �巡�� ���� �� ������Ʈ�� ���̰� ����
        }
    }

    public void Drop()
    {
        isDrag = false;
        rigid.simulated = true; // �巡�� ���� �� ���� �ùķ��̼� Ȱ��ȭ

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("DropArea"))
        {
            isInDropArea = true;

            // �巡�� ������ ������Ʈ�� ��� ������ ������ �� ������ �ڵ�
            Debug.Log("Draggable entered drop area");

            // ������Ʈ�� ��� ������ �ڽ����� ����
            transform.SetParent(other.transform);

            // ���̴� ��ġ�� ����
            Vector3 newPosition = other.transform.position;
            newPosition.y += stackCount * 0.2f; // ���̴� ���� ����
            transform.position = newPosition;

            // ���� ������Ʈ �� ����
            stackCount++;

            // �巡�� ��� ��Ȱ��ȭ
            isDrag = false;
            rigid.simulated = true;
            enabled = false;

            // ���ο� ��Ḧ �����Ͽ� ���� �ڸ��� ����
            InstantiateNewIngredient();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.CompareTag("DropArea"))
        {
            isInDropArea = false;
        }
    }

    private void SetTransparency(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    private void InstantiateNewIngredient()
    {
        // ��ӵ� ������Ʈ�� ���� �������� ã��
        GameObject originalIngredient = FindOriginalIngredient(gameObject);
        if (originalIngredient != null)
        {
            // ���� ����� �������� ����
            GameObject newIngredient = Instantiate(originalIngredient, originalPosition, originalIngredient.transform.rotation);

            // Ŭ���� �������� (0.666, 0.666, 0.666)���� ����
            newIngredient.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            // Ŭ���� DragDrop ��ũ��Ʈ�� �ʱ�ȭ
            DragDrop newDragDrop = newIngredient.GetComponent<DragDrop>();
            newDragDrop.originalIngredients = originalIngredients;
            newDragDrop.isDrag = false; // �ʱ�ȭ �� �巡�� ���¸� false�� ����
            newDragDrop.rigid.simulated = true; // ���� �ùķ��̼� Ȱ��ȭ
            newDragDrop.SetTransparency(0f); // �ʱ� ���� ���·� ����
            newDragDrop.enabled = true; // DragDrop ��ũ��Ʈ�� Ȱ��ȭ
        }
    }
    private GameObject FindOriginalIngredient(GameObject droppedObject)
    {
        string originalName = droppedObject.name.Replace("(Clone)", "").Trim();
        foreach (GameObject ingredient in originalIngredients)
        {
            if (ingredient.name == originalName)
            {
                return ingredient;
            }
        }
        return null;
    }
}
