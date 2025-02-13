using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    [SerializeField]CustomerOrderSystem customerOrderSystem;
    Queue<GameObject> customerQueue= new Queue<GameObject>();

    [SerializeField] float nextOrderTime=7f;
    [SerializeField]CustomerSO[] customers;
    [SerializeField]Transform waitingRoom;
    [SerializeField] GameObject customerPrefab;
    float reducedTime=0.2f;
    float time=7f;

    void Awake()
    {
        customerOrderSystem=FindObjectOfType<CustomerOrderSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;
        if (time > nextOrderTime) {
            EnterCustomer();
            time = 0f;
            nextOrderTime -= reducedTime;
        }
        //print(customerQueue.Count);
    }
    void EnterCustomer()
    {
        //오브젝트 풀로 바꿀까 생각 중
        int cutomerIndex=Random.Range(0, customers.Length);
        GameObject customer =Instantiate(customerPrefab, waitingRoom);
        customerQueue.Enqueue(customer);
        customer.GetComponent<SpriteRenderer>().sprite = customers[cutomerIndex].GetCustomerNormalFace();
        customerOrderSystem.SetCustomerOrder(customers[cutomerIndex]);
    }
}
