using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "New Customer/Customer")]
public class CustomerSO : ScriptableObject
{
    [SerializeField] Sprite normalFace;
    [SerializeField] Sprite happyFace;
    [SerializeField] Sprite sadFace;
    [SerializeField] string customerName;

    public Sprite GetCustomerNormalFace()
    {
        return normalFace;
    }
    public Sprite GetCustomerHappylFace()
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
