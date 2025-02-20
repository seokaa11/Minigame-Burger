using UnityEngine;

public class CustomerOrderInfo : MonoBehaviour
{
    [SerializeField] BurgerRecipe burgerRecipe;
    [SerializeField] CustomerSO customer;
    [SerializeField]SpriteRenderer customerSprite;
    string orderText;
    void Awake()
    {
        CustomerOrderSystem.OnOrderTimeout += SetCustomerSadFace;//주문 수락 실패 시
    }
    public void Init(BurgerRecipe recipe, CustomerSO customerSO)
    {
        this.burgerRecipe = recipe;
        this.customer = customerSO;
        orderText=recipe.GetOrderText();
        customerSprite = GetComponent<SpriteRenderer>().GetComponent<SpriteRenderer>();
        gameObject.GetComponent<Collider2D>().enabled=false;
        customerSprite.sprite = customer.GetCustomerNormalFace();
    }
    
    
    public void SetCustomerSadFace()
    {
        customerSprite.sprite = customer.GetCustomerSadFace();
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
    void OnDisable()
    {
        CustomerOrderSystem.OnOrderTimeout -= SetCustomerSadFace;//주문 수락 실패 시

    }
}
