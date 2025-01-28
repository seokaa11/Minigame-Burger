using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Droppable : MonoBehaviour
{
    public int stackCount = 0; // ���� ������Ʈ ��
    public List<GameObject> originalIngredients; // ���� ��� ������ ����Ʈ

    // �� ����� �̸�, �ִ� ����, ���� ������ ������ ����Ʈ
    public List<string> ingredientNames;
    public List<int> ingredientMaxAmounts;
    public List<int> ingredientCurrentAmounts;

    // ��ӵ� ������Ʈ���� ������ ����Ʈ
    public List<GameObject> droppedItems = new List<GameObject>();

    // ��Ằ ��ġ�� ������ ����Ʈ
    public List<Vector3> ingredientPositions;
    private void Start()
    {
        // ���� ������ �ʱ�ȭ
        ingredientNames = new List<string> { "Bun", "Patty", "Lettuce", "Tomato", "Cheese", "Pickle", "Ketchup2", "Mayo2", "Bulgogi2" };
        ingredientMaxAmounts = new List<int> { 10, 10, 10, 10, 10, 10, 9999, 9999, 9999 };
        ingredientCurrentAmounts = new List<int>(ingredientMaxAmounts);

        // ��Ằ ��ġ �ʱ�ȭ
        ingredientPositions = new List<Vector3>
        {
            new Vector3(-7.4f, 0.22f, 0f), // Bun
            new Vector3(-5.15f, 0.16f, 0f), // Patty
            new Vector3(-2.77f, 0.12f, 0f), // Lettuce
            new Vector3(-0.47f, 0.13f, 0f), // Tomato
            new Vector3(1.87f, 0.19f, 0f), // Cheese
            new Vector3(4.26f, 0.14f, 0f), // Pickle
            new Vector3(5.95f, -0.09f, 0f), // Ketchup2
            new Vector3(6.96f, -0.09f, 0f), // Mayo2
            new Vector3(8f, -0.09f, 0f)  // Bulgogi2
        };
    }

    public void OnDrop(Draggable draggable)
    {
        Collider2D other = draggable.GetComponent<Collider2D>();
        if (other != null && other.CompareTag("Draggable"))
        {
            Debug.Log("Draggable entered drop area");

            // Bun���� Ȯ���ϰ� ó�� ���� ��� UnderBun���� ��ȯ
            if (draggable.name.Equals("Bun") && stackCount == 0)
            {
                SpriteRenderer spriteRenderer = draggable.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && draggable.replacementSprite != null)
                {
                    spriteRenderer.sprite = draggable.replacementSprite;
                }
            }
            // Bun�� stackCount�� 0�� �ƴϰ� Bun�� Ŭ���� ��� ���� �̹����� ����
            else if (draggable.name.Equals("Bun") && stackCount != 0)
            {
                SpriteRenderer spriteRenderer = draggable.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && draggable.originalSprite != null)
                {
                    spriteRenderer.sprite = draggable.originalSprite;
                }
            }

            if (new[] { "Ketchup2", "Mayo2", "Bulgogi2" }.Contains(draggable.name))
            {
                SpriteRenderer spriteRenderer = draggable.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && draggable.SauceSprite != null)
                {
                    spriteRenderer.sprite = draggable.SauceSprite;
                }
            }

            // ������Ʈ�� ��� ������ �ڽ����� ����
            other.transform.SetParent(transform);

            // ���̴� ��ġ�� ����
            Vector3 newPosition = transform.position;
            newPosition.y += stackCount * 0.15f; // ���̴� ���� ����
            other.transform.position = newPosition;

            // ���� ������Ʈ �� ����
            stackCount++;

            // ��ӵ� ������Ʈ�� ����Ʈ�� �߰�
            droppedItems.Add(other.gameObject);

            // ���� ��� ���� ����
            string ingredientName = draggable.name.Replace("(Clone)", "").Trim();
            int index = ingredientNames.IndexOf(ingredientName);
            if (index != -1)
            {
                ingredientCurrentAmounts[index]--;
            }

            // �巡�� ��� ��Ȱ��ȭ
            draggable.isDraggable = false;

            // ������Ʈ�� sortingOrder ����
            SpriteRenderer spriteRenderer2 = other.GetComponent<SpriteRenderer>();
            if (spriteRenderer2 != null)
            {
                spriteRenderer2.sortingOrder = stackCount;
            }

            // Instantiate �޼��� ȣ��
            Instantiate(draggable.gameObject);
            GameObject clone = Instantiate(draggable.gameObject);

            // Ŭ�� ������Ʈ �̸����� (Clone) ����
            clone.name = draggable.name.Replace("(Clone)", "").Trim();

            // Ŭ�� ������Ʈ�� Draggable ������Ʈ �߰� �� �ʱ�ȭ
            Draggable cloneDraggable = clone.GetComponent<Draggable>();
            if (cloneDraggable != null)
            {
                cloneDraggable.isDraggable = true;
                // �߰����� �ʱ�ȭ �۾��� �ʿ��ϸ� ���⿡ �߰�
            }

            // Ŭ�� ��ġ �� ������ ����
            if (index != -1 && index < ingredientPositions.Count)
            {
                clone.transform.position = ingredientPositions[index];
            }
            clone.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            // ����� ������ 0�̸� �巡�� �Ұ����ϰ� ����
            if (index != -1 && ingredientCurrentAmounts[index] <= 0)
            {
                cloneDraggable.isDraggable = false;
                cloneDraggable.GetComponent<Collider2D>().enabled = false;
            }
            else if (index != -1 && ingredientCurrentAmounts[index] > 0)
            {
                cloneDraggable.isDraggable = true;
                cloneDraggable.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    public void UpdateIngredientAmount(string ingredientName, int newAmount)
    {
        int index = ingredientNames.IndexOf(ingredientName);
        if (index != -1)
        {
            ingredientCurrentAmounts[index] = newAmount;
        }
    }
    public void MakeClonesDraggable()
    {
        // ��� Draggable ��ü�� �˻�
        var allDraggables = GameObject.FindObjectsOfType<Draggable>();

        foreach (var cloneDraggable in allDraggables)
        {
            if (cloneDraggable != null)
            {
                // ��� ������ �ִ� Ŭ�е��� �巡�� �Ұ����ϰ� ����
                if (cloneDraggable.transform.parent == transform)
                {
                    cloneDraggable.isDraggable = false;
                    cloneDraggable.GetComponent<Collider2D>().enabled = false;
                }
                else
                {
                    cloneDraggable.isDraggable = true;
                    cloneDraggable.GetComponent<Collider2D>().enabled = true;
                }
            }
        }
    }

    // DropArea �ȿ� �� ��� ������Ʈ�� �����ϴ� �޼���
    public void ClearAllDroppedItems()
    {
        foreach (GameObject item in droppedItems)
        {
            Destroy(item);
        }
        droppedItems.Clear();
        stackCount = 0; // ���� ������Ʈ �� �ʱ�ȭ
    }
}