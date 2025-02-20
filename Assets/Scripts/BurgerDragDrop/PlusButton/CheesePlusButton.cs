using UnityEngine;

public class CheesePlusButton : MonoBehaviour
{
    public Droppable droppable; // Droppable ��ũ��Ʈ�� �����ϱ� ���� ����

    public void SetCheeseCount()
    {
        // "Cheese"�� ������ 10���� ����
        droppable.UpdateIngredientAmount("Cheese", 10);

        // Ŭ�е��� �巡�� �����ϵ��� ����
        droppable.MakeClonesDraggable();
    }
}