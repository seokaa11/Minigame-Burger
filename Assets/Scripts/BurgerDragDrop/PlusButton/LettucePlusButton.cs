using UnityEngine;

public class LettucePlusButton : MonoBehaviour
{
    public Droppable droppable; // Droppable ��ũ��Ʈ�� �����ϱ� ���� ����

    public void SetLettuceCount()
    {
        // "Patty"�� ������ 10���� ����
        droppable.UpdateIngredientAmount("Lettuce", 10);

        // Ŭ�е��� �巡�� �����ϵ��� ����
        droppable.MakeClonesDraggable();
    }
}