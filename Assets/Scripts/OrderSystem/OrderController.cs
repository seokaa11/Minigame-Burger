using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    [SerializeField] float nextOrderTime=7f;
    [SerializeField]CustomerSO[] customers;
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
    }
    void EnterCustomer()
    {
        int cutomerIndex=Random.Range(0, customers.Length);
        print(customers[cutomerIndex]);
    }
}
