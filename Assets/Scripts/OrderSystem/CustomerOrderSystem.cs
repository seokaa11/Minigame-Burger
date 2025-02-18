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
    private int currentOrder = -1;          // 현재 손님의 주문

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
        currentOrder = customer.GetBurgerNum();
        customerOrderText.text = $"{customer.GetCustomerName()}님의 주문:";
    }

    // 주문 수락 및 버거 제조 시작
    void AcceptOrder()
    {

        if (currentOrder < 0)
        {
            Debug.LogWarning("현재 설정된 주문이 없습니다.");
            return;
        }

        //주문창 비활성화
        orderDisplay.SetActive(false);

        // 주문서 생성
        GameObject newOrder = Instantiate(orderPrefab, orderContainer.transform.position, Quaternion.identity ,orderContainer);
        newOrder.GetComponentInChildren<TextMeshProUGUI>().text = customer.GetCustomerOrderText();
        Text orderText = newOrder.GetComponentInChildren<Text>();
        Button orderButton = newOrder.GetComponent<Button>();

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
}
