using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageOpacityController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<Image> images; // 모든 이미지를 리스트에 추가합니다.
    public float dimmedOpacity = 0.5f; // 다른 이미지들의 불투명도 값

    public Image currentImage; // 현재 마우스가 올려진 이미지
    public float originalOpacity; // 원래 이미지의 불투명도 값

    void Start()
    {
        // 초기화 작업: 이미지들의 기본 불투명도 값을 저장합니다.
        if (images == null || images.Count == 0)
        {
            return;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentImage = GetComponent<Image>();
        if (currentImage != null)
        {
            originalOpacity = currentImage.color.a;
            SetImagesOpacity(dimmedOpacity);
            SetImageOpacity(currentImage, originalOpacity);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentImage != null)
        {
            SetImagesOpacity(1.0f); // 모든 이미지를 원래 불투명도로 되돌립니다.
        }
    }

    public void SetImagesOpacity(float opacity)
    {
        foreach (Image img in images)
        {
            SetImageOpacity(img, opacity);
        }
    }

    public void SetImageOpacity(Image img, float opacity)
    {
        Color color = img.color;
        color.a = opacity;
        img.color = color;
    }
}