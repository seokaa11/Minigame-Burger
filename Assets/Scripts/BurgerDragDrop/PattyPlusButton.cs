using UnityEngine;

public class PattyPlusButton : MonoBehaviour
{
    public Droppable droppable; // Droppable ��ũ��Ʈ�� �����ϱ� ���� ����

    void OnMouseDown()
    {
        Debug.Log("All ingredient amounts set to 10");

        // "Bun"�� ������ 10���� ����
        droppable.UpdateIngredientAmount("Patty", 10);

        // Ŭ�е��� �巡�� �����ϵ��� ����
        droppable.MakeClonesDraggable();
    }
}