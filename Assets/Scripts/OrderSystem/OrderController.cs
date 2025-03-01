using System.Collections;
using UnityEngine;
//주문 시간 관리, 고객 시간 관리
public class OrderController : MonoBehaviour
{
    [SerializeField] BurgerRecipe[] burgers;//버거 종류
    [SerializeField] CustomerSO[] customers;//손님 종류
    [SerializeField] GameObject customerPrefab;//손님 프리펩
    [SerializeField] Transform waitingRoom; //손님 입장 위치
    [SerializeField] GameObject dialog;
    [SerializeField] int burgerId; //버거 번호
    [SerializeField] int customerIndex;  //손님 id
    CustomerOrderSystem customerOrderSystem;
    [SerializeField] GameObject currentCustomer;
    [SerializeField] GameObject prevCustomer;

    private Coroutine exitCoroutine = null; // 퇴장 코루틴

    public int GetburgerId()
    {
        return burgerId;
    }
    public int GetCustomerIndex()
    {
        return customerIndex;
    }
    void Awake()
    {
        customerOrderSystem = GetComponent<CustomerOrderSystem>();
        CustomerOrderSystem.OnOrderTimeout += NewOrder;
    }
    void Start()
    {
        dialog.SetActive(false);
        Invoke("EnterCustomer", 0.5f);
    }
    public void NewOrder()
    {
        StartCoroutine(WaitingtimeUntilNextOrder());
        // 기존 exitCoroutine 중단하고 새로 시작
        if (exitCoroutine != null)
        {
            StopCoroutine(exitCoroutine);
        }
        exitCoroutine = StartCoroutine(ExitCustomer());
    }
    IEnumerator WaitingtimeUntilNextOrder()
    {
        float time = customerOrderSystem.TimeUntilNextOrder;
        yield return new WaitForSeconds(time);
        EnterCustomer();
        customerOrderSystem.TimeUntilNextOrder = customerOrderSystem.ReducedTime;
    }
    IEnumerator ExitCustomer()
    {
        customerOrderSystem.DestroyOrderPrefab();
        dialog.SetActive(true);

        // 이전 손님이 있으면 먼저 제거
        if (prevCustomer != null)
        {
            Destroy(prevCustomer);
            prevCustomer = null;
        }

        // 현재 손님을 이전 손님으로 이동
        if (currentCustomer != null)
        {
            prevCustomer = currentCustomer;
            currentCustomer = null;
            prevCustomer.transform.position += new Vector3(4.5f, 0, 0);
        }

        yield return new WaitForSeconds(2);

        // 이전 손님 파괴
        if (prevCustomer != null)
        {
            Destroy(prevCustomer);
            prevCustomer = null;
        }

        dialog.SetActive(false);
        exitCoroutine = null;
    }

    //손님 입장
    void EnterCustomer()
    {
        customerIndex = Random.Range(0, customers.Length);
        GameObject customer = Instantiate(customerPrefab, waitingRoom);// 손님 생성 및 위치 지정
        currentCustomer = customer;
        // 랜덤 손님 받아서 초기화 및 랜덤 버거도 초기화
        CustomerOrderInfo customerOrderInfo = customer.GetComponent<CustomerOrderInfo>();
        burgerId = Random.Range(0, burgers.Length);
        customerOrderInfo.Init(burgers[burgerId], customers[customerIndex]);
        customerOrderSystem.SetCustomerOrder(customerOrderInfo);
    }

    void OnDisable()
    {
        CustomerOrderSystem.OnOrderTimeout -= NewOrder;
        if (exitCoroutine != null)
        {
            StopCoroutine(exitCoroutine);
        }
    }
}