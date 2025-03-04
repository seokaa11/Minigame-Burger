using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


//주문 접수
public class CustomerOrderSystem : MonoBehaviour
{
    [SerializeField] Button yesButton;          // 수락 버튼
    [SerializeField] Button noButton;          // 거절 버튼
    [SerializeField] bool prevRefuseOrder = false;//전에 거절을 눌렀는지
    [SerializeField] GameObject orderDisplay;
    [SerializeField] GameObject orderPrefab;       // 주문서 프리팹
    [SerializeField] Transform orderContainer;     // 주문서를 걸 컨테이너
    [SerializeField] GameObject orderDetailsUI;    // 주문 내용을 표시할 UI
    [SerializeField] Button recipeCanvas;
    [SerializeField] TextMeshProUGUI customerOrderText;       // 현재 손님의 주문 텍스트
    [SerializeField] TextMeshProUGUI orderDetailsText;        // 주문 내용 텍스트    
    [SerializeField] bool isOrderWaiting = false;
    [SerializeField] bool isMaking = false;
    [SerializeField] float _timeUntilNextOrder = 7f; //다음 주문까지 남은 시간
    [SerializeField] float makingTime = 60f;//주문 후 손님 대기시간 초깃값
    [HideInInspector] public float ReducedTime { get; } = 0.2f;   //다음 주문까지 남은 시간 감소

    float reducedMakingTime = 0.2f;//손님 대기시간 감솟값
    float reducedMakingTimeStandard = 5f;//감소 기준
    float time = 0;//reducedMakingTimeStandard 시간 체크를 위한 변수
    CustomerOrderInfo customer;
    public static event Action OnOrderTimeout;//손님 퇴장, 새로운 손님 입장
    [SerializeField] EvalueateBurger evalueateBurger;
    OrderController orderController;
    public bool IsMaking
    {
        get => isMaking;
        set => isMaking = value;
    }
    public float TimeUntilNextOrder
    {
        get { return _timeUntilNextOrder; }
        set
        {
            _timeUntilNextOrder -= value;
            if (_timeUntilNextOrder - value < 0)
            {
                _timeUntilNextOrder = 0;
            }
        }
    }

    void Awake()
    {
        recipeCanvas.onClick.AddListener(CheckoverOrder);
        yesButton.onClick.AddListener(AcceptOrder);
        noButton.onClick.AddListener(RefuseOrder);
        if (orderDetailsUI != null)
        {
            orderDetailsUI.SetActive(false);
        }
        if (orderDetailsUI != null)
        {
            orderDisplay.SetActive(false);
        }

        orderController = FindObjectOfType<OrderController>();
    }



    void Update()
    {
        UpdateCustomerPatience();
    }

    void UpdateCustomerPatience()
    {
        time += Time.deltaTime;
        if (time > reducedMakingTimeStandard)
        {
            makingTime -= reducedMakingTime;
            time = 0;
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
        float time = 0;
        while (isOrderWaiting)
        {
            if (time > 10)
            {
                GameManager.instance.score -= 5;

                orderDisplay.SetActive(false);
                int num = orderController.GetCustomerIndex();
                GameManager.instance.Dialog = evalueateBurger.GetScoredatas(3).dialog[num];
                OnOrderTimeout?.Invoke();
                break;
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
    //주문 거절
    void RefuseOrder()
    {

        OnOrderTimeout?.Invoke();
        int num = orderController.GetCustomerIndex();
        GameManager.instance.Dialog = "...";
        orderDisplay.SetActive(false);//주문창 비활성화
        if (prevRefuseOrder)
        {
            GameManager.instance.score += evalueateBurger.GetScoredatas(3).score[num];
            prevRefuseOrder = false;
        }
        if (Timer.curTime >= 30)
        {
            GameManager.instance.score += evalueateBurger.GetScoredatas(0).score[num];
            prevRefuseOrder = false;
        }
        prevRefuseOrder = true;        
    }
    // 주문 수락 및 버거 제조 시작
    void AcceptOrder()
    {
        prevRefuseOrder = false;
        StartMakingTimer();

        orderDisplay.SetActive(false);//주문창 비활성화
        customer.GetComponent<Collider2D>().enabled = true;//손님 콜라이더 활성화. 주문 받았을 때에만 전달 가능하게        
        if (orderPrefab != null) orderPrefab.SetActive(true);// 주문서 활성화
        TextMeshProUGUI orderText = orderPrefab.GetComponentInChildren<TextMeshProUGUI>();
        Button orderButton = orderPrefab.GetComponent<Button>();

        if (orderText != null)
            orderText.text = customerOrderText.text;


        if (orderButton != null)
            orderButton.onClick.AddListener(() => ShowOrderDetails(customer));    // 주문서 클릭 시 주문 내용 표시    
        //Debug.Log($"{customerOrderText.text}님의 주문서가 생성되었습니다.");
    }
    //버거 제조 타이머 시작.
    void StartMakingTimer()
    {
        GameManager.instance.takenTime = 0;
        IsMaking = true;
        isOrderWaiting = false;
        StartCoroutine(StartMakingTimerCoroutine());
    }
    IEnumerator StartMakingTimerCoroutine()
    {
        float time = 0;
        while (IsMaking)
        {
            time += Time.deltaTime;
            if (time > makingTime)
            {
                OnOrderTimeout?.Invoke();
                GameManager.instance.score += evalueateBurger.GetScoredatas(3).score[orderController.GetCustomerIndex()];
                GameManager.instance.Dialog = evalueateBurger.GetScoredatas(3).dialog[orderController.GetCustomerIndex()];

                break;
            }
            yield return null;
        }

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
