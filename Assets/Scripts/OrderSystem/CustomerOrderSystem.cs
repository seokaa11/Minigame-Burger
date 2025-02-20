using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//주문 접수
public class CustomerOrderSystem : MonoBehaviour
{
    [SerializeField] Button acceptButton;          // 수락 버튼
    [SerializeField] GameObject orderDisplay;
    [SerializeField] GameObject orderPrefab;       // 주문서 프리팹
    [SerializeField] Transform orderContainer;     // 주문서를 걸 컨테이너
    [SerializeField] GameObject orderDetailsUI;    // 주문 내용을 표시할 UI
    [SerializeField] Button recipeCanvas;
    [SerializeField] TextMeshProUGUI customerOrderText;       // 현재 손님의 주문 텍스트
    [SerializeField] TextMeshProUGUI orderDetailsText;        // 주문 내용 텍스트
    CustomerOrderInfo customer;
    [SerializeField] float time = 0;
    [SerializeField] bool isOrderWaiting = false;

    public static event Action OnOrderTimeout;
    void Awake()
    {
        recipeCanvas.onClick.AddListener(CheckoverOrder);
        acceptButton.onClick.AddListener(AcceptOrder);
        if (orderDetailsUI != null)
        {
            orderDetailsUI.SetActive(false);
        }
        if (orderDetailsUI != null)
        {
            orderDisplay.SetActive(false);
        }

    }
    //상세 주문서 배경 클릭시 상세 주문 비활성
    void CheckoverOrder()
    {
        orderDetailsUI.SetActive(false);
    }

    // 손님 주문을 설정
    public void SetCustomerOrder(CustomerOrderInfo order)
    {
        orderDisplay.SetActive(true);
        customer = order;
        customerOrderText.text = $"{customer.GetCustomerName()}\n님의 주문";
        orderDisplay.GetComponentInChildren<TextMeshProUGUI>().text = customer.GetCustomerOrderText();
        isOrderWaiting = true;
        if (GameManager.instance.score >= 100)
        {
            StartCoroutine(WaitingCount());
        }
    }
    IEnumerator WaitingCount()
    {
        time = 0;

        while (isOrderWaiting)
        {
            if (time > 10)
            {
                GameManager.instance.score -= 5;
                orderDisplay.SetActive(false);                
                OnOrderTimeout?.Invoke();
                break;
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
    // 주문 수락 및 버거 제조 시작
    void AcceptOrder()
    {
        GameManager.instance.takenTime = 0;
        isOrderWaiting = false;
        //주문창 비활성화
        orderDisplay.SetActive(false);
        //손님 콜라이더 활성화. 주문 받았을 때에만 전달 가능하게
        customer.GetComponent<Collider2D>().enabled = true;
        // 주문서 활성화
        if (orderPrefab == null) return;
        orderPrefab.SetActive(true);
        TextMeshProUGUI orderText = orderPrefab.GetComponentInChildren<TextMeshProUGUI>();
        Button orderButton = orderPrefab.GetComponent<Button>();

        if (orderText != null)
        {
            orderText.text = customerOrderText.text;
        }

        if (orderButton != null)
        {
            // 주문서 클릭 시 주문 내용 표시
            orderButton.onClick.AddListener(() => ShowOrderDetails(customer));
        }
        Debug.Log($"{customerOrderText.text}님의 주문서가 생성되었습니다.");
    }

    // 주문 내용을 표시하는 메서드
    void ShowOrderDetails(CustomerOrderInfo customer)
    {
        orderDetailsUI.SetActive(true);       // 주문 내용 UI 활성화
        orderDetailsText.text = "";
        orderDetailsText.text = customer.GetBurgerName();
    }
    public void DestroyOrderPrefab()
    {
        orderPrefab.SetActive(false);
    }
}
