using UnityEngine;

public class OnionPlusButton : MonoBehaviour
{
    public Droppable droppable; // Droppable ��ũ��Ʈ�� �����ϱ� ���� ����

    public void SetOnionCount()
    {
        // "Tomato"�� ������ 10���� ����
        droppable.UpdateIngredientAmount("Onion", 10);

        // Ŭ�е��� �巡�� �����ϵ��� ����
        droppable.MakeClonesDraggable();
    }
}