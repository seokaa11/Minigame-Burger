using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] GameObject currentCustomer;
    CustomerOrderSystem customerOrderSystem;

    public int burgerId; //���� ��ȣ

    public int customerIndex;  //�մ� id
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
    //�մ� ����
    void EnterCustomer()
    {
        customerIndex = Random.Range(0, customers.Length);

        // �մ� ���� �� ��ġ ����
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
        // ���� �մ� �޾Ƽ� �ʱ�ȭ �� ���� ���ŵ� �ʱ�ȭ
        CustomerOrderInfo customerOrderInfo = customer.GetComponent<CustomerOrderInfo>();
        burgerId = Random.Range(0, burgers.Length);
        Debug.Log(burgerId);
        customerOrderInfo.Init(burgers[burgerId], customers[customerIndex]);
        customerOrderSystem.SetCustomerOrder(customerOrderInfo);
    }
}
