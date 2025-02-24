using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageOpacityController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<Image> images; // ��� �̹����� ����Ʈ�� �߰��մϴ�.
    public float dimmedOpacity = 0.5f; // �ٸ� �̹������� ������ ��

    public Image currentImage; // ���� ���콺�� �÷��� �̹���
    public float originalOpacity; // ���� �̹����� ������ ��

    void Start()
    {
        // �ʱ�ȭ �۾�: �̹������� �⺻ ������ ���� �����մϴ�.
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
            SetImagesOpacity(1.0f); // ��� �̹����� ���� �������� �ǵ����ϴ�.
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