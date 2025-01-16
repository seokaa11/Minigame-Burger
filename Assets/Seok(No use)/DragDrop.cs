using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public List<GameObject> originalIngredients; // 원본 재료 프리팹 리스트
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
            rigid.simulated = false; // 드래그 중에는 물리 시뮬레이션을 비활성화
            SetTransparency(1f); // 드래그 시작 시 오브젝트를 보이게 설정
        }
    }

    public void Drop()
    {
        isDrag = false;
        rigid.simulated = true; // 드래그 중지 후 물리 시뮬레이션 활성화

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("DropArea"))
        {
            isInDropArea = true;

            // 드래그 가능한 오브젝트가 드롭 영역에 들어왔을 때 실행할 코드
            Debug.Log("Draggable entered drop area");

            // 오브젝트를 드롭 영역의 자식으로 설정
            transform.SetParent(other.transform);

            // 쌓이는 위치를 조정
            Vector3 newPosition = other.transform.position;
            newPosition.y += stackCount * 0.2f; // 쌓이는 높이 조정
            transform.position = newPosition;

            // 쌓인 오브젝트 수 증가
            stackCount++;

            // 드래그 기능 비활성화
            isDrag = false;
            rigid.simulated = true;
            enabled = false;

            // 새로운 재료를 생성하여 원래 자리로 복귀
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
        // 드롭된 오브젝트의 원본 프리팹을 찾음
        GameObject originalIngredient = FindOriginalIngredient(gameObject);
        if (originalIngredient != null)
        {
            // 원본 재료의 복제본을 생성
            GameObject newIngredient = Instantiate(originalIngredient, originalPosition, originalIngredient.transform.rotation);

            // 클론의 스케일을 (0.666, 0.666, 0.666)으로 고정
            newIngredient.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            // 클론의 DragDrop 스크립트를 초기화
            DragDrop newDragDrop = newIngredient.GetComponent<DragDrop>();
            newDragDrop.originalIngredients = originalIngredients;
            newDragDrop.isDrag = false; // 초기화 시 드래그 상태를 false로 설정
            newDragDrop.rigid.simulated = true; // 물리 시뮬레이션 활성화
            newDragDrop.SetTransparency(0f); // 초기 투명 상태로 설정
            newDragDrop.enabled = true; // DragDrop 스크립트를 활성화
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
