using UnityEngine;

public class CheesePlusButton : MonoBehaviour
{
    public Droppable droppable; // Droppable 스크립트를 참조하기 위한 변수

    public void SetCheeseCount()
    {
        // "Cheese"의 수량을 10으로 설정
        droppable.UpdateIngredientAmount("Cheese", 10);

        // 클론들이 드래그 가능하도록 설정
        droppable.MakeClonesDraggable();
    }
}