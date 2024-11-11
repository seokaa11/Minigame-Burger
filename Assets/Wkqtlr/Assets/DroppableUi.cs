using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    private Image image;
    private RectTransform rect;


    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }


    public void OnPointerEnter(PointerEventData eventData) //나중에 수정1
    {
        image.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData) //22
    {
        image.color = Color.white;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
        }
    }
}