using UnityEngine;

public class TomatoPlusButton : MonoBehaviour
{
    public Droppable droppable; // Droppable ��ũ��Ʈ�� �����ϱ� ���� ����

    public void SetTomatoCount()
    { 
        // "Tomato"�� ������ 10���� ����
        droppable.UpdateIngredientAmount("Tomato", 10);

        // Ŭ�е��� �巡�� �����ϵ��� ����
        droppable.MakeClonesDraggable();
    }
}