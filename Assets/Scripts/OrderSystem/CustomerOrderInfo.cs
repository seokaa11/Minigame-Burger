using UnityEngine;

public class CustomerOrderInfo : MonoBehaviour
{
    [SerializeField] BurgerRecipe burgerRecipe;
    [SerializeField] CustomerSO customer;
    [SerializeField] SpriteRenderer customerSprite;
    string orderText;
    void Awake()
    {

        CustomerOrderSystem.OnOrderTimeout += SetCustomerSadFace;//�ֹ� ���� ���� ��
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
        print("������");
        customerSprite.sprite = customer.GetCustomerSadFace();
    }
    public void SetCustomerHappyFace()
    {
        print("�����");
        customerSprite.sprite = customer.GetCustomerHappyFace();
    }
    public void SetCustomerNormalFace()
    {
        print("������");
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

        CustomerOrderSystem.OnOrderTimeout -= SetCustomerSadFace;//�ֹ� ���� ���� ��

    }
}
