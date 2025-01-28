using UnityEngine;

public class PattyPlusButton : MonoBehaviour
{
    public Droppable droppable; // Droppable 스크립트를 참조하기 위한 변수

    void OnMouseDown()
    {
        // "Patty"의 수량을 10으로 설정
        droppable.UpdateIngredientAmount("Patty", 10);

        // 클론들이 드래그 가능하도록 설정
        droppable.MakeClonesDraggable();
    }
}