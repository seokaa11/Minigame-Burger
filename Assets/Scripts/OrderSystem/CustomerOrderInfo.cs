using UnityEngine;

public class CustomerOrderInfo : MonoBehaviour
{
    [SerializeField] BurgerRecipe burgerRecipe;
    [SerializeField] CustomerSO customer;
    string orderText;
    public void Init(BurgerRecipe recipe, CustomerSO customerSO)
    {
        this.burgerRecipe = recipe;
        this.customer = customerSO;
        orderText=recipe.GetOrderText();
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
    public string GetCustomerOrderText()
    {
        return orderText;
    }
}
