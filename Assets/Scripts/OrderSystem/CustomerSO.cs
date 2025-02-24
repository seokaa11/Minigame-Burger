using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "New Customer/Customer")]
public class CustomerSO : ScriptableObject
{
    [SerializeField] Sprite normalFace;
    [SerializeField] Sprite happyFace;
    [SerializeField] Sprite sadFace;
    [SerializeField] string customerName;
    [SerializeField]string originalOrderText;
    [SerializeField] string orderText;
    [SerializeField] string orderText2;
    



    public string OrderText
    {
        get
        {
            return orderText + orderText2;
        }
        set
        {
            orderText = originalOrderText;
            orderText += value;
        }
    }
    public Sprite GetCustomerNormalFace()
    {
        return normalFace;
    }
    public Sprite GetCustomerHappyFace()
    {
        return happyFace;
    }
    public Sprite GetCustomerSadFace()
    {
        return sadFace;
    }
    public string GetCustomerName()
    {
        return customerName;
    }
    
}
