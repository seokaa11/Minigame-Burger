using UnityEngine;

public class EvalueateBurger : MonoBehaviour
{
    public Scoredata[] scoredata;
    public BurgerRecipe[] burgerRecipes;
    public GameObject submitedBurger;
    public int requestBurgerNum;
    OrderController orderController;
    CustomerOrderSystem customerOrderSystem;
    void Start()
    {
        orderController = FindObjectOfType<OrderController>();
        customerOrderSystem=FindObjectOfType<CustomerOrderSystem>();
        submitedBurger = GameObject.Find("DropArea");
    }
    //고객에게 버거 드래그 제출 시 평가
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
    // 버거 상태에 따라 점수가 주어지고 대화문 출력
    public void BurgerScore(GameObject burger, int requestBurgerNum)
    {
        for (int i = 0; i <= scoredata.Length / 2; i++)
        {
            if (GameManager.instance.takenTime > 20) { i = 4; }
            if (scoredata[i].Time >= GameManager.instance.takenTime)
            {
                if (IsPerferctBurger(burger, burgerRecipes[requestBurgerNum]) && i != 4)
                {
                    i += 5;
                }
                GameManager.instance.score += scoredata[i].score[orderController.GetCustomerIndex()];
                if (scoredata[i].score[orderController.GetCustomerIndex()] > 0) { SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_SCORE); }
                GameManager.instance.health += scoredata[i].health;
                if (scoredata[i].health < 0) { SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_LOSTHEALTH); }
                GameManager.instance.dialog = scoredata[i].dialog;
                GameManager.instance.takenTime = 0;

                break;
            }
        }
    }

    //버거가 완벽한지 판단
    public bool IsPerferctBurger(GameObject items, BurgerRecipe recipe)
    {
        for (int i = 0; i < recipe.ingredients.Count; i++)
        {
            if (items.transform.childCount != recipe.ingredients.Count) { return false; }
            else if (recipe.ingredients[i].transform.name != items.transform.GetChild(i).name) { return false; }
        }
        return true;
    }
}
