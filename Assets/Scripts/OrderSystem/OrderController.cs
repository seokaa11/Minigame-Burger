using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ֹ� �ð� ����, �� �ð� ����
public class OrderController : MonoBehaviour
{
    [SerializeField] BurgerRecipe[] burgers;//���� ����
    [SerializeField] CustomerSO[] customers;//�մ� ����
    [SerializeField] GameObject customerPrefab;//�մ� ������
    [SerializeField] float timeUntilNextOrder = 7f; //���� �ֹ����� ���� �ð�
    [SerializeField] Transform waitingRoom; //�մ� ���� ��ġ
    [SerializeField] float reducedTime = 0.2f;   //�ֹ� �� ����
    [SerializeField]GameObject currentCustomer;
    CustomerOrderSystem customerOrderSystem;
    float time = 7f;    //���� ���۽� �ٷ� �ֹ� ������

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
    //�մ� ����
    void EnterCustomer()
    {
        int customerIndex = Random.Range(0, customers.Length);       

        // �մ� ���� �� ��ġ ����
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
        // ���� �մ� �޾Ƽ� �ʱ�ȭ �� ���� ���ŵ� �ʱ�ȭ
        CustomerOrderInfo customerOrderInfo = customer.GetComponent<CustomerOrderInfo>();
        customerOrderInfo.Init(burgers[Random.Range(0, burgers.Length)], customers[customerIndex]);  
        customerOrderSystem.SetCustomerOrder(customerOrderInfo);
    }    
}
