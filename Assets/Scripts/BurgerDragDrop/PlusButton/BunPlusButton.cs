using UnityEngine;

public class BunPlusButton : MonoBehaviour
{
    public Droppable droppable; // Droppable ��ũ��Ʈ�� �����ϱ� ���� ����

    void OnMouseDown()
    {
        // "Bun"�� ������ 10���� ����
        droppable.UpdateIngredientAmount("Bun", 10);

        // Ŭ�е��� �巡�� �����ϵ��� ����
        droppable.MakeClonesDraggable();
    }
}