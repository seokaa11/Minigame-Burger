using UnityEngine;

public class CustomerOrderInfo : MonoBehaviour
{
    [SerializeField] BurgerRecipe burgerRecipe;
    [SerializeField] CustomerSO customer;
    [SerializeField] SpriteRenderer customerSprite;
    string orderText;
    void Awake()
    {

        CustomerOrderSystem.OnOrderTimeout += SetCustomerSadFace;//주문 수락 실패 시
    }
    public void Init(BurgerRecipe recipe, CustomerSO customerSO)
    {
        this.burgerRecipe = recipe;
        this.customer = customerSO;
        customer.OrderText = recipe.GetOrderText();
        orderText = customer.OrderText;
        customerSprite = GetComponent<SpriteRenderer>().GetComponent<SpriteRenderer>();
        gameObject.GetComponent<Collider2D>().enabled = false;
        customerSprite.sprite = customer.GetCustomerNormalFace();
    }

    public void SetCustomerSadFace()
    {
        print("슬픔이");
        customerSprite.sprite = customer.GetCustomerSadFace();
    }
    public void SetCustomerHappyFace()
    {
        print("기쁨이");
        customerSprite.sprite = customer.GetCustomerHappyFace();
    }
    public void SetCustomerNormalFace()
    {
        print("보통이");
        customerSprite.sprite = customer.GetCustomerNormalFace();
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
