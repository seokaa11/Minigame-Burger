using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] GameObject currentCustomer;
    CustomerOrderSystem customerOrderSystem;

    public int burgerId; //버거 번호

    public int customerIndex;  //손님 id
    void Awake()
    {
        customerOrderSystem = GetComponent<CustomerOrderSystem>();
    }
    /*void Update()
    {
        if (Input.GetKeyUp(KeyCode.O)) {
            EnterCustomer();
        }
    }*/
    void Start()
    {
        EnterCustomer();
    }

    public void NewOrder()
    {
        StartCoroutine(WaitingtimeUntilNextOrder());
    }
    IEnumerator WaitingtimeUntilNextOrder()
    {
        currentCustomer.transform.position += new Vector3(4.5f,0,0);
        yield return new WaitForSeconds(timeUntilNextOrder);        
        EnterCustomer();
        timeUntilNextOrder -= reducedTime;
        timeUntilNextOrder = Mathf.Max(timeUntilNextOrder, 0);
    }
    //손님 입장
    void EnterCustomer()
    {
        customerIndex = Random.Range(0, customers.Length);

        // 손님 생성 및 위치 지정
        GameObject customer = Instantiate(customerPrefab, waitingRoom);
        if (currentCustomer != null)
        {
            Destroy(currentCustomer,2f);
            currentCustomer = customer;
        }
        else
        {
            currentCustomer = customer;
        }
        // 랜덤 손님 받아서 초기화 및 랜덤 버거도 초기화
        CustomerOrderInfo customerOrderInfo = customer.GetComponent<CustomerOrderInfo>();
        burgerId = Random.Range(0, burgers.Length);
        Debug.Log(burgerId);
        customerOrderInfo.Init(burgers[burgerId], customers[customerIndex]);
        customerOrderSystem.SetCustomerOrder(customerOrderInfo);
    }
}
