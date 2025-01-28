using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Droppable : MonoBehaviour
{
    public int stackCount = 0; // 쌓인 오브젝트 수
    public List<GameObject> originalIngredients; // 원본 재료 프리팹 리스트

    // 각 재료의 이름, 최대 수량, 현재 수량을 저장할 리스트
    public List<string> ingredientNames;
    public List<int> ingredientMaxAmounts;
    public List<int> ingredientCurrentAmounts;

    // 드롭된 오브젝트들을 저장할 리스트
    public List<GameObject> droppedItems = new List<GameObject>();

    // 재료별 위치를 저장할 리스트
    public List<Vector3> ingredientPositions;
    private void Start()
    {
        // 예제 데이터 초기화
        ingredientNames = new List<string> { "Bun", "Patty", "Lettuce", "Tomato", "Cheese", "Pickle", "Ketchup2", "Mayo2", "Bulgogi2" };
        ingredientMaxAmounts = new List<int> { 10, 10, 10, 10, 10, 10, 9999, 9999, 9999 };
        ingredientCurrentAmounts = new List<int>(ingredientMaxAmounts);

        // 재료별 위치 초기화
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

            // Bun인지 확인하고 처음 놓는 경우 UnderBun으로 변환
            if (draggable.name.Equals("Bun") && stackCount == 0)
            {
                SpriteRenderer spriteRenderer = draggable.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && draggable.replacementSprite != null)
                {
                    spriteRenderer.sprite = draggable.replacementSprite;
                }
            }
            // Bun의 stackCount가 0이 아니고 Bun의 클론인 경우 원래 이미지로 변경
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

            // Instantiate 메서드 호출
            Instantiate(draggable.gameObject);
            GameObject clone = Instantiate(draggable.gameObject);

            // 클론 오브젝트 이름에서 (Clone) 제거
            clone.name = draggable.name.Replace("(Clone)", "").Trim();

            // 클론 오브젝트에 Draggable 컴포넌트 추가 및 초기화
            Draggable cloneDraggable = clone.GetComponent<Draggable>();
            if (cloneDraggable != null)
            {
                cloneDraggable.isDraggable = true;
                // 추가적인 초기화 작업이 필요하면 여기에 추가
            }

            // 클론 위치 및 스케일 설정
            if (index != -1 && index < ingredientPositions.Count)
            {
                clone.transform.position = ingredientPositions[index];
            }
            clone.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            // 재료의 수량이 0이면 드래그 불가능하게 설정
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
        // 모든 Draggable 객체를 검색
        var allDraggables = GameObject.FindObjectsOfType<Draggable>();

        foreach (var cloneDraggable in allDraggables)
        {
            if (cloneDraggable != null)
            {
                // 드롭 영역에 있는 클론들은 드래그 불가능하게 설정
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