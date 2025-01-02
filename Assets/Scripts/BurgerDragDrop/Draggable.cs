using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset; // 마우스 클릭 위치와 오브젝트 중심의 차이를 저장할 변수
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러 컴포넌트를 저장할 변수
    private bool isDragging = false; // 드래그 중인지 여부
    public bool isDraggable = true; // 드래그 가능 여부
    private bool isInDropZone = false; // 드롭 영역에 있는지 여부
    public Vector3 originalPosition; // 원본 위치를 저장할 변수
    public Vector3 originalScale; // 원본 스케일을 저장할 변수

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        // 시작 시 오브젝트를 투명하게 설정
        SetTransparency(0f);
    }
    private void SetTransparency(float alpha) // 투명도 설정 함수
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    void OnMouseDown()
    {
        if (isDraggable)
        {
            // 마우스를 클릭한 위치와 오브젝트 중심의 차이를 계산
            offset = transform.position - GetMouseWorldPosition();
            originalPosition = transform.position; // 드래그 시작 시 원본 위치 저장
            originalScale = transform.localScale; // 드래그 시작 시 원본 스케일 저장
            isDragging = true;
            SetTransparency(1f);
        }
    }

    void OnMouseDrag() // 마우스 드래그 시 호출되는 함수
    {
        if (isDragging && isDraggable)
        {
            // 마우스 위치를 따라 오브젝트를 이동
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private void OnMouseUp() // 마우스를 떼었을 때 호출되는 함수
    {
        if (isDragging && isDraggable)
        {
            isDragging = false;
            if (isInDropZone)
            {
                Droppable droppable = FindObjectOfType<Droppable>();
                if (droppable != null)
                {
                    droppable.OnDrop(this);
                }
            }
            else
            {
                transform.position = originalPosition;  // 드롭 영역에 있지 않다면 원래 위치로 되돌아감
                SetTransparency(0f);    // 투명도를 다시 0으로 설정   
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // 마우스 위치를 월드 좌표로 변환 (2D용)
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z; // z 값은 카메라와의 거리
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DropArea"))
        {
            isInDropZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DropArea"))
        {
            isInDropZone = false;
        }
    }
}
