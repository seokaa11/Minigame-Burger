using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//주문 시간 관리, 고객 시간 관리
public class OrderController : MonoBehaviour
{
    [SerializeField] BurgerRecipe[] burgers;//버거 종류
    [SerializeField] CustomerSO[] customers;//손님 종류
    [SerializeField] GameObject customerPrefab;//손님 프리펩
    [SerializeField] float timeUntilNextOrder = 7f; //다음 주문까지 남은 시간
    [SerializeField] Transform waitingRoom; //손님 입장 위치
    [SerializeField] float reducedTime = 0.2f;   //주문 텀 감소
    [SerializeField]GameObject currentCustomer;
    CustomerOrderSystem customerOrderSystem;
    float time = 7f;    //게임 시작시 바로 주문 들어오게

    void Awake()
    {
        customerOrderSystem = GetComponent<CustomerOrderSystem>();
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O)) {
            EnterCustomer();
        }
    }
    void Start()
    {
        EnterCustomer();
    }

    public void NewOrder()
    {
        time += Time.deltaTime;
        if (time > timeUntilNextOrder)
        {
            EnterCustomer();
            time = 0f;
            timeUntilNextOrder -= reducedTime;
            timeUntilNextOrder = Mathf.Max(timeUntilNextOrder, 0);
        }
    }
    //손님 입장
    void EnterCustomer()
    {
        int customerIndex = Random.Range(0, customers.Length);       

        // 손님 생성 및 위치 지정
        GameObject customer = Instantiate(customerPrefab, waitingRoom);
        if (currentCustomer != null)
        {
            Destroy(currentCustomer);
            currentCustomer = customer;
        }
        else
        {
            currentCustomer = customer;

        }
        // 랜덤 손님 받아서 초기화 및 랜덤 버거도 초기화
        CustomerOrderInfo customerOrderInfo = customer.GetComponent<CustomerOrderInfo>();
        customerOrderInfo.Init(burgers[Random.Range(0, burgers.Length)], customers[customerIndex]);  
        customerOrderSystem.SetCustomerOrder(customerOrderInfo);
    }    
}
