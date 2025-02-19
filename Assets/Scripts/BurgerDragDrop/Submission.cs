using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submission : MonoBehaviour
{
    private Vector3 offset; // ���콺�� ������Ʈ �� �Ÿ�
    private Camera mainCamera; // ���� ī�޶�
    private bool isDropped = false; // ��� ���� ����
    private EvalueateBurger evaluateBurgerScript; // EvaluateBurger�� ���� ��ũ��Ʈ ����

    private void Start()
    {
        mainCamera = Camera.main; // ���� ī�޶� ��������
        evaluateBurgerScript = GetComponent<EvalueateBurger>();
    }

    private void OnMouseDown()
    {
        // ������Ʈ�� ���콺 ��ġ�� �Ÿ� ���
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        // �巡�� �� ������Ʈ ��ġ ����
        transform.position = GetMouseWorldPos() + offset;
    }

    private void OnMouseUp()
    {
        // ���콺�� ������ �� ��� ���� ���� Ȯ��
        if (isDropped)
        {
            Debug.Log("Burger�� Customer�� ��ӵǾ����ϴ�.");

            // ���⼭ �ʿ��� ó���� �����մϴ�.
            HandleBurgerDrop();
        }
        else
        {
            Debug.Log("Burger�� ��ȿ���� ���� ���� ��ӵǾ����ϴ�.");
            ResetPosition(); // ���� ��ġ�� �ǵ�����
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        // ���콺 ȭ�� ��ǥ�� ���� ��ǥ�� ��ȯ
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0; // Z���� 2D ȯ�濡�� ����
        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��� ����� �±װ� Customer���� Ȯ��
        if (collision.CompareTag("Customer"))
        {
            isDropped = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ��� ��󿡼� ����� ��
        if (collision.CompareTag("Customer"))
        {
            isDropped = false;
        }
    }

    private void ResetPosition()
    {
        // ���� ��ġ�� �ǵ����� (���� ��ġ�� �����ϴ� ��� �ʿ�)
        transform.position = new Vector3(0.08f, -3.33f, transform.position.z);
    }

    private void HandleBurgerDrop()
    {
        // Burger�� "Customer" ������ ��ӵǾ��� �� �ʿ��� �۾��� ����
        Debug.Log("Burger�� Customer�� ��ӵǾ����ϴ�. �������� �����մϴ�.");

        // ���⿡�� �ʿ��� �߰� �۾��� ó���մϴ�.
        // ��: ��ӵ� ������ ����, Burger ����, ��Ÿ �ļ� �۾� ��
        // ��: "Customer" ������ ��ӵ� ��, ��ӵ� �������� ��� �����ϴ� �ڵ�
        Droppable droppableScript = FindObjectOfType<Droppable>();
        if (droppableScript != null)
        {
            droppableScript.ClearAllDroppedItems(); // Droppable ��ũ��Ʈ�� ������ ���� �޼��� ȣ��
        }

        //������� �ڵ�

        if (evaluateBurgerScript != null)
        {

            evaluateBurgerScript.OnEvalue();
            HUD.Submit();


        }

    }
}
