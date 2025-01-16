using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Droppable : MonoBehaviour
{
    private int stackCount = 0; // ���� ������Ʈ ��
    public List<GameObject> originalIngredients; // ���� ��� ������ ����Ʈ

    // �� ����� �̸�, �ִ� ����, ���� ������ ������ ����Ʈ
    public List<string> ingredientNames;
    public List<int> ingredientMaxAmounts;
    public List<int> ingredientCurrentAmounts;

    // ��ӵ� ������Ʈ���� ������ ����Ʈ
    public List<GameObject> droppedItems = new List<GameObject>();
    private void Start()
    {
        // ���� ������ �ʱ�ȭ
        ingredientNames = new List<string> { "Bun", "Patty", "Lettuce", "Tomato", "Cheese","Pickle", "Ketchup2", "Mayo2", "Bulgogi2" };
        ingredientMaxAmounts = new List<int> { 4, 4, 4, 4, 4, 4, 9999, 9999, 9999 };
        ingredientCurrentAmounts = new List<int>(ingredientMaxAmounts);
    }

    public void OnDrop(Draggable draggable)
    {
        Collider2D other = draggable.GetComponent<Collider2D>();
        if (other != null && other.CompareTag("Draggable"))
        {
            // �巡�� ������ ������Ʈ�� ��� ������ ������ �� ������ �ڵ�
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

            // ��Ḧ �����Ͽ� ���� �ڸ��� ����
            if (ingredientCurrentAmounts[index] > 0)
            {
                InstantiateNewIngredient(draggable);
            }
        }
    }



    public void InstantiateNewIngredient(Draggable draggable) // ��Ḧ �����ϴ� �޼���
    {
        // ��ӵ� ������Ʈ�� ���� �������� ã��
        GameObject originalIngredient = FindOriginalIngredient(draggable.gameObject);
        if (originalIngredient != null)
        {
            // ���� ����� �������� ����
            GameObject newIngredient = Instantiate(originalIngredient, draggable.originalPosition, originalIngredient.transform.rotation);

            // Ŭ���� �������� ���� ������Ʈ�� �����ϰ� �����ϰ� ����
            newIngredient.transform.localScale = draggable.originalScale;
            newIngredient.GetComponent<Draggable>().isDraggable = true;


            // ���ο� ��ᰡ Bun�� ��� ���� ��������Ʈ�� ����
            if (newIngredient.name.Equals("Bun(Clone)"))
            {
                SpriteRenderer spriteRenderer = newIngredient.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && draggable.originalSprite != null)
                {
                    spriteRenderer.sprite = draggable.originalSprite;
                }
            }
        }
    }

    public GameObject FindOriginalIngredient(GameObject droppedObject)
    {
        // ��ӵ� ������Ʈ�� ���� ������ ���� �������� ã��
        foreach (GameObject ingredient in originalIngredients)
        {
            if (ingredient.name == droppedObject.name.Replace("(Clone)", "").Trim())
            {
                return ingredient;
            }
        }
        return null;
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