using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable : MonoBehaviour
{
    private int stackCount = 0; // ���� ������Ʈ ��
    public List<GameObject> originalIngredients; // ���� ��� ������ ����Ʈ

    public void OnDrop(Draggable draggable)
    {
        Collider2D other = draggable.GetComponent<Collider2D>();
        if (other != null && other.CompareTag("Draggable"))
        {
            // �巡�� ������ ������Ʈ�� ��� ������ ������ �� ������ �ڵ�
            Debug.Log("Draggable entered drop area");

            // ������Ʈ�� ��� ������ �ڽ����� ����
            other.transform.SetParent(transform);

            // ���̴� ��ġ�� ����
            Vector3 newPosition = transform.position;
            newPosition.y += stackCount * 0.15f; // ���̴� ���� ����
            other.transform.position = newPosition;

            // ���� ������Ʈ �� ����
            stackCount++;

            // �巡�� ��� ��Ȱ��ȭ
            draggable.isDraggable = false;

            // ������Ʈ�� sortingOrder ����
            SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = stackCount;
            }

            // ���ο� ��Ḧ �����Ͽ� ���� �ڸ��� ����
            InstantiateNewIngredient(draggable);
        }
    }

    private void InstantiateNewIngredient(Draggable draggable)
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
        }
    }

    private GameObject FindOriginalIngredient(GameObject droppedObject)
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
}