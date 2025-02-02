using UnityEngine;
using UnityEngine.UI;

public class ButtonImageChanger : MonoBehaviour
{
    public Button myButton;       // 버튼 오브젝트
    public Image targetImage;     // 변경할 이미지
    public Sprite newSprite;      // 변경할 새로운 스프라이트

    void Start()
    {
        // 버튼에 클릭 이벤트 추가
        myButton.onClick.AddListener(ChangeImage);
    }

    void ChangeImage()
    {
        if (targetImage != null && newSprite != null)
        {
            targetImage.sprite = newSprite; // 이미지 변경
            Debug.Log("이미지가 변경되었습니다.");
        }
        else
        {
            Debug.LogWarning("타겟 이미지 또는 새로운 스프라이트가 설정되지 않았습니다.");
        }
    }
}
