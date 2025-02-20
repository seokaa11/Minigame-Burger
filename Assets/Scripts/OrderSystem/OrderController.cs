using System.Collections;
using UnityEngine;

//�ֹ� �ð� ����, �� �ð� ����
public class OrderController : MonoBehaviour
{
    [SerializeField] BurgerRecipe[] burgers;//���� ����
    [SerializeField] CustomerSO[] customers;//�մ� ����
    [SerializeField] GameObject customerPrefab;//�մ� ������
    [SerializeField] float timeUntilNextOrder = 7f; //���� �ֹ����� ���� �ð�
    [SerializeField] Transform waitingRoom; //�մ� ���� ��ġ
    [SerializeField] GameObject dialog;
    [SerializeField] int burgerId; //���� ��ȣ
    [SerializeField] int customerIndex;  //�մ� id

    CustomerOrderSystem customerOrderSystem;
    GameObject currentCustomer;
    float reducedTime = 0.2f;   //�ֹ� �� ����
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
        yield return new WaitForSeconds(timeUntilNextOrder);
        EnterCustomer();
        timeUntilNextOrder -= reducedTime;
        timeUntilNextOrder = Mathf.Max(timeUntilNextOrder, 0);
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
    //�մ� ����
    void EnterCustomer()
    {
        customerIndex = Random.Range(0, customers.Length);

        // �մ� ���� �� ��ġ ����
        GameObject customer = Instantiate(customerPrefab, waitingRoom);
        if (currentCustomer != null)
        {
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
    void OnDisable()
    {
        CustomerOrderSystem.OnOrderTimeout -= NewOrder;
    }
}
