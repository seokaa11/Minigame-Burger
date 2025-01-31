using UnityEngine;

public class PattyPlusButton : MonoBehaviour
{
    public Droppable droppable; // Droppable ��ũ��Ʈ�� �����ϱ� ���� ����

    void OnMouseDown()
    {
        // "Patty"�� ������ 10���� ����
        droppable.UpdateIngredientAmount("Patty", 10);

        // Ŭ�е��� �巡�� �����ϵ��� ����
        droppable.MakeClonesDraggable();
    }
}