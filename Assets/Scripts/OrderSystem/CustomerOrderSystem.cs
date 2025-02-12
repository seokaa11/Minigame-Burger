using UnityEngine;
using UnityEngine.UI;

public class CustomerOrderSystem : MonoBehaviour
{
    [System.Serializable]
    public class Order
    {
        public string customerName;  // 손님 이름
        public string orderDetails; // 주문 내용
    }

    [SerializeField] Button acceptButton;          // 수락 버튼
    public GameObject orderPrefab;       // 주문서 프리팹
    public Transform orderContainer;     // 주문서를 걸 컨테이너
    public GameObject orderDetailsUI;    // 주문 내용을 표시할 UI
    public Text orderDetailsText;        // 주문 내용 텍스트
    public Text customerOrderText;       // 현재 손님의 주문 텍스트

    private Order currentOrder;          // 현재 손님의 주문

    void Start()
    {
        // 수락 버튼 클릭 이벤트 추가
        acceptButton.onClick.AddListener(AcceptOrder);
        if (orderDetailsUI != null)
        {
            orderDetailsUI.SetActive(false); // 초기에는 주문 내용 UI 숨김
        }

        // 예제: 임의의 주문 설정
        SetCustomerOrder(new Order { customerName = "홍길동", orderDetails = "커피 2잔, 케이크 1개" });
    }

    // 손님 주문을 설정하는 메서드
    public void SetCustomerOrder(Order order)
    {
        currentOrder = order;
        customerOrderText.text = $"{order.customerName}님의 주문: {order.orderDetails}";
    }

    // 주문을 수락하여 주문서를 생성하는 메서드
    void AcceptOrder()
    {
        if (currentOrder == null)
        {
            Debug.LogWarning("현재 설정된 주문이 없습니다.");
            return;
        }

        // 주문서 생성
        GameObject newOrder = Instantiate(orderPrefab, orderContainer);
        Text orderText = newOrder.GetComponentInChildren<Text>();
        Button orderButton = newOrder.GetComponent<Button>();

        if (orderText != null)
        {
            orderText.text = $"{currentOrder.customerName}님의 주문";
        }

        if (orderButton != null)
        {
            // 주문서 클릭 시 주문 내용 표시
            orderButton.onClick.AddListener(() => ShowOrderDetails(currentOrder.orderDetails));
        }

        Debug.Log($"{currentOrder.customerName}님의 주문서가 생성되었습니다.");
    }

    // 주문 내용을 표시하는 메서드
    void ShowOrderDetails(string details)
    {
        orderDetailsUI.SetActive(true);       // 주문 내용 UI 활성화
        orderDetailsText.text = details;     // 주문 내용을 텍스트로 설정
        Debug.Log("주문 내용: " + details);
    }
}
