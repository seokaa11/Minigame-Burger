using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Droppable : MonoBehaviour
{

    public GameObject burgerPrefab; // Burger 프리팹을 할당할 변수
    private GameObject instantiatedBurger; // 생성된 Burger 오브젝트를 참조

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
        ingredientNames = new List<string> { "Bun", "Patty", "Lettuce", "Tomato", "Cheese", "Pickle", "Onion", "Ketchup2", "Mayo2", "Bulgogi2" };
        ingredientMaxAmounts = new List<int> { 10, 10, 10, 10, 10, 10, 10, 9999, 9999, 9999 };
        ingredientCurrentAmounts = new List<int>(ingredientMaxAmounts);

        // 재료별 위치 초기화
        ingredientPositions = new List<Vector3>
        {
            new Vector3(-7.54f, -0.76f, 0f), // Bun
            new Vector3(-5.04f, -0.91f, 0f), // Patty
            new Vector3(-2.49f, -0.89f, 0f), // Lettuce
            new Vector3(0.01f, -0.89f, 0f), // Tomato
            new Vector3(2.53f, -0.84f, 0f), // Cheese
            new Vector3(5.02f, -0.88f, 0f), // Pickle
            new Vector3(7.59f, -0.9f, 0f), // Onion
            new Vector3(5.9f, 1.7f, 0f), // Ketchup2
            new Vector3(7.01f, 1.7f, 0f), // Mayo2
            new Vector3(8.09f, 1.7f, 0f)  // Bulgogi2
        };


    }
 

    private void OnMouseDown()
    {
        // Hamburger의 자식 오브젝트들 처리
        if (transform.parent != null && transform.parent.name == "Hamburger")
        {
            Transform parent = transform.parent;

            // Hamburger 투명화
            SetTransparencyRecursively(parent, 0f);

            DisableCollidersRecursively(gameObject);


            // Burger 프리팹 생성
            if (burgerPrefab != null)
            {
                if (instantiatedBurger == null) // 중복 생성 방지
                {
                    instantiatedBurger = Instantiate(
                        burgerPrefab,
                        parent.position,
                        Quaternion.identity
                    );
                    instantiatedBurger.name = "Burger"; // 생성된 이름 명확히 설정
                    //Debug.Log("Burger 프리팹 생성됨: " + instantiatedBurger.name);

                    instantiatedBurger.transform.position = new Vector3(0, -3.2f, 0.2f);

                    // 생성된 버거의 스케일을 0.2f, 0.2f, 0.2f로 조정
                    instantiatedBurger.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                }
            }

            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_PACKAGING);

        }
    }

    private void SetTransparencyRecursively(Transform parent, float alpha)
    {
        SetObjectTransparency(parent.gameObject, alpha);

        foreach (Transform child in parent)
        {
            SetTransparencyRecursively(child, alpha);
        }
    }

    private void SetObjectTransparency(GameObject obj, float alpha)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = renderer.material;

            if (material.HasProperty("_Color"))
            {
                Color color = material.color;
                color.a = alpha;
                material.color = color;

                SetMaterialRenderingMode(material, alpha < 1.0f ? "Transparent" : "Opaque");
            }
        }
        else
        {
            CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = alpha;
            }
        }
    }

    private void SetMaterialRenderingMode(Material material, string mode)
    {
        if (mode == "Transparent")
        {
            material.SetFloat("_Mode", 3);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
        }
        else if (mode == "Opaque")
        {
            material.SetFloat("_Mode", 0);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
        }
    }

    private void CheckAndDisableDraggableItems()
    {
        // DropArea 내에 있는 모든 "Bun" 개수 확인
        int bunCount = droppedItems.Count(item => item.name == "Bun");

        if (bunCount >= 3)
        {
            // 새로운 부모 오브젝트 생성
            GameObject hamburger = new GameObject("Hamburger");
            hamburger.transform.position = transform.position;

            // 드롭 영역을 새로운 부모의 자식으로 설정
            transform.SetParent(hamburger.transform);

            // 모든 재료의 드래그 기능 비활성화
            DisableAllDraggableItems();
            Debug.Log("Bun 개수가 2개 이상입니다. 모든 Draggable을 비활성화합니다.");
        }
    }

    public void OnDrop(Draggable draggable)
    {
        Collider2D other = draggable.GetComponent<Collider2D>();
        if (other != null && other.CompareTag("Draggable"))
        {
            //Debug.Log("Draggable entered drop area");


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

            // 드롭된 오브젝트 리스트에 추가
            droppedItems.Add(other.gameObject);

            // 추가: Bun 개수 확인 후 모든 드래그 비활성화 여부 결정
            CheckAndDisableDraggableItems();

            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_PUT);

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
    public void MakeClonesDraggable() // 클론이 드래그 가능하도록 설정하는 메서드
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

    private void DisableAllDraggableItems()
    {
        // 드롭 영역 내의 모든 드래그 가능한 오브젝트를 비활성화
        foreach (var draggable in FindObjectsOfType<Draggable>())
        {
            draggable.isDraggable = false;
        }
    }

    private void AbleAllDraggableItems()
    {
        // 드롭 영역 내의 모든 드래그 가능한 오브젝트를 활성화
        foreach (var draggable in FindObjectsOfType<Draggable>())
        {
            draggable.isDraggable = true;
        }
    }


    public void DisableCollidersRecursively(GameObject parentObject)
    {
        Collider2D[] colliders2D = parentObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider2D in colliders2D)
        {
            collider2D.enabled = false;
        }
    }

    public void ableCollidersRecursively(GameObject parentObject)
    {
        Collider2D[] colliders2D = parentObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider2D in colliders2D)
        {
            collider2D.enabled = true;
        }
    }

    // DropArea 안에 들어간 모든 오브젝트를 제거하는 메서드
    public void ClearAllDroppedItems() // 쓰레기통 구현 메서드
    {
        // 드롭된 아이템 제거
        foreach (GameObject item in droppedItems)
        {
            Destroy(item);
        }
        droppedItems.Clear();
        stackCount = 0; // 쌓인 오브젝트 수 초기화

        // DropArea의 부모가 Hamburger인지 확인하고 삭제
        if (transform.parent != null && transform.parent.name == "Hamburger")
        {
            GameObject hamburger = transform.parent.gameObject;
            transform.SetParent(null); // DropArea를 분리
            Destroy(hamburger); // Hamburger 객체 제거
        }

        if (instantiatedBurger != null)
        {
            Destroy(instantiatedBurger);
            instantiatedBurger = null; // 참조 초기화
        }

        Transform dropArea = FindObjectOfType<Droppable>().transform; // DropArea 참조
        SetObjectTransparency(dropArea.gameObject, 1.0f); // DropArea를 불투명 상태로 변경

        ableCollidersRecursively(gameObject);

        // 모든 Draggable 아이템 다시 활성화
        AbleAllDraggableItems();

    }

}