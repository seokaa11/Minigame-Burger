using UnityEngine;

public class EvalueateBurger : MonoBehaviour
{
    public Scoredata[] scoredata;
    public BurgerRecipe[] burgerRecipes;
    public GameObject submitedBurger;
    public int requestBurgerNum;
    OrderController orderController;
    CustomerOrderSystem customerOrderSystem;
    [SerializeField] CustomerOrderInfo customerOrderInfo;
    void Start()
    {
        orderController = FindObjectOfType<OrderController>();
        customerOrderSystem = FindObjectOfType<CustomerOrderSystem>();
        submitedBurger = GameObject.Find("DropArea");
    }
    //������ ���� �巡�� ���� �� ��
    public void OnEvalue()
    {
        customerOrderSystem.IsMaking = false;
        requestBurgerNum = orderController.GetburgerId();
        BurgerScore(submitedBurger, requestBurgerNum);
        if (orderController != null)
        {
            orderController.NewOrder();
        }
    }
    public Scoredata GetScoredatas(int n)
    {
        return scoredata[n];
    }
    // ���� ���¿� ���� ������ �־����� ��ȭ�� ���
    public void BurgerScore(GameObject burger, int requestBurgerNum)
    {
        for (int scoredataIndex = 0; scoredataIndex <= scoredata.Length / 2; scoredataIndex++)
        {
            if (scoredata[scoredataIndex].Time >= GameManager.instance.takenTime)
            {
                if (IsPerferctBurger(burger, burgerRecipes[requestBurgerNum]))
                {
                    scoredataIndex += 3;
                }
                int customerIndex = orderController.GetCustomerIndex();
                GameManager.instance.score += scoredata[scoredataIndex].score[customerIndex];
                GameManager.instance.health += scoredata[scoredataIndex].health;
                GameManager.instance.Dialog = scoredata[scoredataIndex].dialog[customerIndex];
                GameManager.instance.takenTime = 0;
                SetCustomerFace_PlaySound(scoredataIndex);
                break;
            }
        }
    }

    void SetCustomerFace_PlaySound(int scoredataIndex)
    {
        customerOrderInfo = FindObjectOfType<CustomerOrderInfo>(); //�մԿ������� ���Ÿ� ����� OrderInfo�� ��ã�Ƽ� ����� �Ű���ϴ�.
        if (scoredataIndex >= 0 && scoredataIndex <= 3)
        {
            customerOrderInfo.SetCustomerSadFace();
            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_LOSTHEALTH);
        }
        else if (scoredataIndex >= 4 && scoredataIndex <= 5)
        {
            customerOrderInfo.SetCustomerHappyFace();
            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_SCORE);
        }
        else
        {
            customerOrderInfo.SetCustomerNormalFace();
            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_SCORE);

        }
    }

    //���Ű� �Ϻ����� �Ǵ�
    public bool IsPerferctBurger(GameObject items, BurgerRecipe recipe)
    {
        for (int i = 0; i < recipe.ingredients.Count; i++)
        {
            if (items.transform.childCount != recipe.ingredients.Count)
            {
                return false;
            }
            else if (recipe.ingredients[i].transform.name != items.transform.GetChild(i).name)
            {
                return false;
            }
        }
        return true;
    }
}
