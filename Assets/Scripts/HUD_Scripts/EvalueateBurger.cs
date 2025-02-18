using UnityEngine;

public class EvalueateBurger : MonoBehaviour
{
    public Scoredata[] scoredata;
    public BurgerRecipe[] burgerRecipes;
    public GameObject submitedBurger;
    public int requestBurgerNum;
    OrderController orderController;
    void Start()
    {
        orderController=FindObjectOfType<OrderController>();
        submitedBurger = GameObject.Find("DropArea");
    }
    //버튼이 눌리면 평가
    public void OnEvalue()
    {
        Debug.Log("평가시작");
        BurgerScore(submitedBurger, requestBurgerNum);
        if(orderController != null)
        {
            orderController.NewOrder();
        }
    }

    // 버거 상태에 따라 점수가 주어지고 대화문 출력
    public void BurgerScore(GameObject burger, int requestBurgerNum)
    {
        for (int i = 0; i < scoredata.Length / 2; i++)
        {
            if (GameManager.instance.takenTime > 20) { i = 4; }
            if (scoredata[i].Time >= GameManager.instance.takenTime)
            {
                if (IsPerferctBurger(burger, burgerRecipes[requestBurgerNum]) && i != 4) { i += 5; }
                //GameManager.instance.score += scoredata[i].score[customer.id];
                GameManager.instance.health += scoredata[i].health;
                GameManager.instance.dialog = scoredata[i].dialog;
                GameManager.instance.takenTime = 0;

                break;
            }
        }
    }

    //버거가 완벽한지 판단
    public bool IsPerferctBurger(GameObject items, BurgerRecipe recipe)
    {
        for (int i = 0; i < recipe.Ingredients.Count; i++)
        {
            if (items.transform.childCount != recipe.Ingredients.Count) { return false; }
            else if (recipe.Ingredients[i].transform.name != items.transform.GetChild(i).name) { return false; }
        }
        return true;
    }
}
