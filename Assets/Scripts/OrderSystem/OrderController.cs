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
    GameObject currentCustomer;

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
        Invoke("EnterCustomer", 1f);
    }

    public void NewOrder()
    {
        StartCoroutine(WaitingtimeUntilNextOrder());
        StartCoroutine(ExitCustomer());
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
        currentCustomer.transform.position += new Vector3(4.5f, 0, 0);
        yield return new WaitForSeconds(3);
        Destroy(currentCustomer);
        dialog.SetActive(false);
    }
    //손님 입장
    void EnterCustomer()
    {
        customerIndex = Random.Range(0, customers.Length);        
        GameObject customer = Instantiate(customerPrefab, waitingRoom);// 손님 생성 및 위치 지정

        if (currentCustomer != null) currentCustomer = customer;
        else currentCustomer = customer;

        // 랜덤 손님 받아서 초기화 및 랜덤 버거도 초기화
        CustomerOrderInfo customerOrderInfo = customer.GetComponent<CustomerOrderInfo>();
        burgerId = Random.Range(0, burgers.Length);
        //Debug.Log(burgerId);
        customerOrderInfo.Init(burgers[burgerId], customers[customerIndex]);
        customerOrderSystem.SetCustomerOrder(customerOrderInfo);
    }
    void OnDisable()
    {
        CustomerOrderSystem.OnOrderTimeout -= NewOrder;
    }
}
