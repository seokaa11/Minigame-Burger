using UnityEngine;

public class PicklePlusButton : MonoBehaviour
{
    public Droppable droppable; // Droppable ��ũ��Ʈ�� �����ϱ� ���� ����

    public void SetPickleCount()
    {
        // "Pickle"�� ������ 10���� ����
        droppable.UpdateIngredientAmount("Pickle", 10);

        // Ŭ�е��� �巡�� �����ϵ��� ����
        droppable.MakeClonesDraggable();
    }
}