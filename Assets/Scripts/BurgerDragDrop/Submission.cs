using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submission : MonoBehaviour
{
    private Vector3 offset; // 마우스와 오브젝트 간 거리
    private Camera mainCamera; // 메인 카메라
    private bool isDropped = false; // 드랍 성공 여부
    private EvalueateBurger evaluateBurgerScript; // EvaluateBurger를 가진 스크립트 참조

    private void Start()
    {
        mainCamera = Camera.main; // 메인 카메라 가져오기
        evaluateBurgerScript = GetComponent<EvalueateBurger>();
    }

    private void OnMouseDown()
    {
        // 오브젝트와 마우스 위치의 거리 계산
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        // 드래그 중 오브젝트 위치 갱신
        transform.position = GetMouseWorldPos() + offset;
    }

    private void OnMouseUp()
    {
        // 마우스를 놓았을 때 드랍 가능 여부 확인
        if (isDropped)
        {
            Debug.Log("Burger가 Customer에 드롭되었습니다.");

            // 여기서 필요한 처리를 진행합니다.
            HandleBurgerDrop();
        }
        else
        {
            Debug.Log("Burger가 유효하지 않은 곳에 드롭되었습니다.");
            ResetPosition(); // 원래 위치로 되돌리기
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        // 마우스 화면 좌표를 월드 좌표로 변환
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0; // Z축은 2D 환경에서 무시
        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 드랍 대상의 태그가 Customer인지 확인
        if (collision.CompareTag("Customer"))
        {
            isDropped = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 드랍 대상에서 벗어났을 때
        if (collision.CompareTag("Customer"))
        {
            isDropped = false;
        }
    }

    private void ResetPosition()
    {
        // 원래 위치로 되돌리기 (원래 위치를 저장하는 방식 필요)
        transform.position = new Vector3(0.08f, -3.33f, transform.position.z);
    }

    private void HandleBurgerDrop()
    {
        // Burger가 "Customer" 영역에 드롭되었을 때 필요한 작업을 수행
        Debug.Log("Burger가 Customer에 드롭되었습니다. 아이템을 제거합니다.");

        // 여기에서 필요한 추가 작업을 처리합니다.
        // 예: 드롭된 아이템 삭제, Burger 생성, 기타 후속 작업 등
        // 예: "Customer" 영역에 드롭된 후, 드롭된 아이템을 모두 제거하는 코드
        Droppable droppableScript = FindObjectOfType<Droppable>();
        if (droppableScript != null)
        {
            droppableScript.ClearAllDroppedItems(); // Droppable 스크립트의 아이템 제거 메서드 호출
        }

        //점수계산 코드

        if (evaluateBurgerScript != null)
        {

            evaluateBurgerScript.OnEvalue();
            HUD.Submit();


        }

    }
}
