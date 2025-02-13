using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrderSystem : MonoBehaviour
{
    
    
    [SerializeField] Button acceptButton;          // 수락 버튼
    [SerializeField] GameObject orderPrefab;       // 주문서 프리팹
    [SerializeField] Transform orderContainer;     // 주문서를 걸 컨테이너
    [SerializeField] GameObject orderDetailsUI;    // 주문 내용을 표시할 UI
    [SerializeField]Button recipeCanvas;
    [SerializeField] TextMeshProUGUI customerOrderText;       // 현재 손님의 주문 텍스트
    [SerializeField] TextMeshProUGUI orderDetailsText;        // 주문 내용 텍스트

    private BurgerRecipe currentOrder;          // 현재 손님의 주문

    void Start()
    {
        // 수락 버튼 클릭 이벤트 추가
        recipeCanvas.onClick.AddListener(CheckoverOrder);
        acceptButton.onClick.AddListener(AcceptOrder);
        if (orderDetailsUI != null)
        {
            orderDetailsUI.SetActive(false); // 초기에는 주문 내용 UI 숨김
        }       
        
    }
    void CheckoverOrder()
    {
        orderDetailsUI.SetActive(false);
    }
    // 손님 주문을 설정하는 메서드
    public void SetCustomerOrder(CustomerSO customer)
    {
        currentOrder = customer.GetBurgerRecipe();
        customerOrderText.text = $"{customer.GetCustomerName()}님의 주문:";
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
            orderText.text = customerOrderText.text;
        }

        if (orderButton != null)
        {
            // 주문서 클릭 시 주문 내용 표시
            orderButton.onClick.AddListener(() => ShowOrderDetails(currentOrder));
        }

        Debug.Log($"{customerOrderText.text}님의 주문서가 생성되었습니다.");
    }

    // 주문 내용을 표시하는 메서드
    void ShowOrderDetails(BurgerRecipe details)
    {
        orderDetailsUI.SetActive(true);       // 주문 내용 UI 활성화
        orderDetailsText.text = "";
        foreach (var recipe in details.GetRecipe())
        {
            orderDetailsText.text += recipe.name+'\n';
        }
        Debug.Log("주문 내용: " + details);
    }
}
