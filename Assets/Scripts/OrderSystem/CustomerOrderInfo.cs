using UnityEngine;

public class CustomerOrderInfo : MonoBehaviour
{
    [SerializeField] BurgerRecipe burgerRecipe;
    [SerializeField] CustomerSO customer;
    public void Init(BurgerRecipe recipe, CustomerSO customerSO)
    {
        this.burgerRecipe = recipe;
        this.customer = customerSO;
        GetComponent<SpriteRenderer>().sprite = customer.GetCustomerNormalFace();
    }
    public int GetBurgerNum()
    {
        return burgerRecipe.GetBurgerNum();
    }

    public string GetBurgerName()
    {
        return burgerRecipe.GetBurgerName(); ;
    }
    public string GetCustomerName()
    {
        return customer.GetCustomerName();
    }
}
