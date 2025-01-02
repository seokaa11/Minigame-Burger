using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable : MonoBehaviour
{
    private int stackCount = 0; // 쌓인 오브젝트 수
    public List<GameObject> originalIngredients; // 원본 재료 프리팹 리스트

    public void OnDrop(Draggable draggable)
    {
        Collider2D other = draggable.GetComponent<Collider2D>();
        if (other != null && other.CompareTag("Draggable"))
        {
            // 드래그 가능한 오브젝트가 드롭 영역에 들어왔을 때 실행할 코드
            Debug.Log("Draggable entered drop area");

            // 오브젝트를 드롭 영역의 자식으로 설정
            other.transform.SetParent(transform);

            // 쌓이는 위치를 조정
            Vector3 newPosition = transform.position;
            newPosition.y += stackCount * 0.15f; // 쌓이는 높이 조정
            other.transform.position = newPosition;

            // 쌓인 오브젝트 수 증가
            stackCount++;

            // 드래그 기능 비활성화
            draggable.isDraggable = false;

            // 오브젝트의 sortingOrder 설정
            SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = stackCount;
            }

            // 새로운 재료를 생성하여 원래 자리로 복귀
            InstantiateNewIngredient(draggable);
        }
    }

    private void InstantiateNewIngredient(Draggable draggable)
    {
        // 드롭된 오브젝트의 원본 프리팹을 찾음
        GameObject originalIngredient = FindOriginalIngredient(draggable.gameObject);
        if (originalIngredient != null)
        {
            // 원본 재료의 복제본을 생성
            GameObject newIngredient = Instantiate(originalIngredient, draggable.originalPosition, originalIngredient.transform.rotation);

            // 클론의 스케일을 원본 오브젝트의 스케일과 동일하게 설정
            newIngredient.transform.localScale = draggable.originalScale;

            newIngredient.GetComponent<Draggable>().isDraggable = true;
        }
    }

    private GameObject FindOriginalIngredient(GameObject droppedObject)
    {
        // 드롭된 오브젝트와 같은 종류의 원본 프리팹을 찾음
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