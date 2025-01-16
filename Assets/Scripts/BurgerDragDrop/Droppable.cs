using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Droppable : MonoBehaviour
{
    private int stackCount = 0; // 쌓인 오브젝트 수
    public List<GameObject> originalIngredients; // 원본 재료 프리팹 리스트

    // 각 재료의 이름, 최대 수량, 현재 수량을 저장할 리스트
    public List<string> ingredientNames;
    public List<int> ingredientMaxAmounts;
    public List<int> ingredientCurrentAmounts;

    // 드롭된 오브젝트들을 저장할 리스트
    public List<GameObject> droppedItems = new List<GameObject>();
    private void Start()
    {
        // 예제 데이터 초기화
        ingredientNames = new List<string> { "Bun", "Patty", "Lettuce", "Tomato", "Cheese","Pickle", "Ketchup2", "Mayo2", "Bulgogi2" };
        ingredientMaxAmounts = new List<int> { 4, 4, 4, 4, 4, 4, 9999, 9999, 9999 };
        ingredientCurrentAmounts = new List<int>(ingredientMaxAmounts);
    }

    public void OnDrop(Draggable draggable)
    {
        Collider2D other = draggable.GetComponent<Collider2D>();
        if (other != null && other.CompareTag("Draggable"))
        {
            // 드래그 가능한 오브젝트가 드롭 영역에 들어왔을 때 실행할 코드
            Debug.Log("Draggable entered drop area");

            // Bun인지 확인하고 처음 놓는 경우 UnderBun으로 변환
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

            // 오브젝트를 드롭 영역의 자식으로 설정
            other.transform.SetParent(transform);

            // 쌓이는 위치를 조정
            Vector3 newPosition = transform.position;
            newPosition.y += stackCount * 0.15f; // 쌓이는 높이 조정
            other.transform.position = newPosition;

            // 쌓인 오브젝트 수 증가
            stackCount++;

            // 드롭된 오브젝트를 리스트에 추가
            droppedItems.Add(other.gameObject);

            // 현재 재료 수량 감소
            string ingredientName = draggable.name.Replace("(Clone)", "").Trim();
            int index = ingredientNames.IndexOf(ingredientName);
            if (index != -1)
            {
                ingredientCurrentAmounts[index]--;
            }

            // 드래그 기능 비활성화
            draggable.isDraggable = false;

            // 오브젝트의 sortingOrder 설정
            SpriteRenderer spriteRenderer2 = other.GetComponent<SpriteRenderer>();
            if (spriteRenderer2 != null)
            {
                spriteRenderer2.sortingOrder = stackCount;
            }

            // 재료를 복제하여 원래 자리로 복귀
            if (ingredientCurrentAmounts[index] > 0)
            {
                InstantiateNewIngredient(draggable);
            }
        }
    }



    public void InstantiateNewIngredient(Draggable draggable) // 재료를 복제하는 메서드
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


            // 새로운 재료가 Bun인 경우 원래 스프라이트로 설정
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

    // DropArea 안에 들어간 모든 오브젝트를 제거하는 메서드
    public void ClearAllDroppedItems()
    {

        foreach (GameObject item in droppedItems)
        {
            Destroy(item);
        }
        droppedItems.Clear();
        stackCount = 0; // 쌓인 오브젝트 수 초기화
    }
}