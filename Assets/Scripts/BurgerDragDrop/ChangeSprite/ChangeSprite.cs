using UnityEngine;

// �⺻ Ŭ���� ����
public abstract class ChangeSprite : MonoBehaviour
{
    public Droppable droppable; // Droppable ��ũ��Ʈ�� ����
    public Sprite manySprite;
    public Sprite halfSprite;
    public Sprite noneSprite;
    public int ingredientIndex; // �ش� ����� �ε���

    private bool isNoneSpriteSet = false; // ��������Ʈ�� noneSprite�� ����Ǿ����� ����
    private bool isHalfSpriteSet = false; // ��������Ʈ�� halfSprite�� ����Ǿ����� ����

    // ��������Ʈ�� �����ϴ� �޼���
    public void Update()
    {
        if (droppable != null && droppable.ingredientCurrentAmounts != null)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // �� ����� ������ ���� ��������Ʈ�� ����
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
                        isHalfSpriteSet = true; // ��ᰡ �ٽ� �߰��Ǹ� ���¸� �ʱ�ȭ
                        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                    }
                }
                else
                {
                    spriteRenderer.sprite = manySprite;
                    isNoneSpriteSet = false; // ��ᰡ �ٽ� �߰��Ǹ� ���¸� �ʱ�ȭ
                    isHalfSpriteSet = false; // ��ᰡ �ٽ� �߰��Ǹ� ���¸� �ʱ�ȭ
                }
            }
        }
    }
}