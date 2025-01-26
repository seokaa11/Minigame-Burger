using UnityEngine;

// 기본 클래스 정의
public abstract class ChangeSprite : MonoBehaviour
{
    public Droppable droppable; // Droppable 스크립트를 참조
    public Sprite manySprite;
    public Sprite halfSprite;
    public Sprite noneSprite;
    public int ingredientIndex; // 해당 재료의 인덱스

    private bool isNoneSpriteSet = false; // 스프라이트가 noneSprite로 변경되었는지 추적
    private bool isHalfSpriteSet = false; // 스프라이트가 halfSprite로 변경되었는지 추적

    // 스프라이트를 변경하는 메서드
    public void Update()
    {
        if (droppable != null && droppable.ingredientCurrentAmounts != null)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // 각 재료의 수량에 따라 스프라이트를 변경
                if (droppable.ingredientCurrentAmounts[ingredientIndex] == 0)
                {
                    if (!isNoneSpriteSet)
                    {
                        spriteRenderer.sprite = noneSprite;
                        isNoneSpriteSet = true;
                    }
                }
                else if(droppable.ingredientCurrentAmounts[ingredientIndex] <= droppable.ingredientMaxAmounts[ingredientIndex]/2)
                {
                    if(!isHalfSpriteSet)
                    {
                        spriteRenderer.sprite = halfSprite;
                        isHalfSpriteSet = true; // 재료가 다시 추가되면 상태를 초기화
                        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                    }
                }
                else
                {
                    spriteRenderer.sprite = manySprite;
                    isNoneSpriteSet = false; // 재료가 다시 추가되면 상태를 초기화
                    isHalfSpriteSet = false; // 재료가 다시 추가되면 상태를 초기화
                }
            }
        }
    }
}