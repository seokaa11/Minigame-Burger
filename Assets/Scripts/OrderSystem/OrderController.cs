using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    
    public Queue<GameObject> customerQueue= new Queue<GameObject>();

    [SerializeField] float nextOrderTime=7f;
    [SerializeField]CustomerSO[] customers;
    [SerializeField]Transform waitingRoom;
    [SerializeField] GameObject customerPrefab;
    float reducedTime=0.2f;
    float time=7f;
    // Start is called before the first frame update
    

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
        //������Ʈ Ǯ�� �ٲܱ� ���� ��
        int cutomerIndex=Random.Range(0, customers.Length);
        GameObject customer =Instantiate(customerPrefab, waitingRoom);
        customerQueue.Enqueue(customer);
        customer.GetComponent<SpriteRenderer>().sprite = customers[cutomerIndex].GetCustomerNormalFace();
        
    }
}
