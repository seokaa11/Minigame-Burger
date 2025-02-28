using System.Collections;
using UnityEngine;
//�ֹ� �ð� ����, �� �ð� ����
public class OrderController : MonoBehaviour
{
    [SerializeField] BurgerRecipe[] burgers;//���� ����
    [SerializeField] CustomerSO[] customers;//�մ� ����
    [SerializeField] GameObject customerPrefab;//�մ� ������
    [SerializeField] Transform waitingRoom; //�մ� ���� ��ġ
    [SerializeField] GameObject dialog;
    [SerializeField] int burgerId; //���� ��ȣ
    [SerializeField] int customerIndex;  //�մ� id
    CustomerOrderSystem customerOrderSystem;
    [SerializeField] GameObject currentCustomer;
    [SerializeField] GameObject prevCustomer;

    private Coroutine exitCoroutine = null; // ���� �ڷ�ƾ

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
        // ���� exitCoroutine �ߴ��ϰ� ���� ����
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

        // ���� �մ��� ������ ���� ����
        if (prevCustomer != null)
        {
            Destroy(prevCustomer);
            prevCustomer = null;
        }

        // ���� �մ��� ���� �մ����� �̵�
        if (currentCustomer != null)
        {
            prevCustomer = currentCustomer;
            currentCustomer = null;
            prevCustomer.transform.position += new Vector3(4.5f, 0, 0);
        }

        yield return new WaitForSeconds(2);

        // ���� �մ� �ı�
        if (prevCustomer != null)
        {
            Destroy(prevCustomer);
            prevCustomer = null;
        }

        dialog.SetActive(false);
        exitCoroutine = null;
    }

    //�մ� ����
    void EnterCustomer()
    {
        customerIndex = Random.Range(0, customers.Length);
        GameObject customer = Instantiate(customerPrefab, waitingRoom);// �մ� ���� �� ��ġ ����
        currentCustomer = customer;
        // ���� �մ� �޾Ƽ� �ʱ�ȭ �� ���� ���ŵ� �ʱ�ȭ
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