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
    //��ư�� ������ ��
    public void OnEvalue()
    {
        Debug.Log("�򰡽���");
        BurgerScore(submitedBurger, requestBurgerNum);
        if(orderController != null)
        {
            orderController.NewOrder();
        }
    }

    // ���� ���¿� ���� ������ �־����� ��ȭ�� ���
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

    //���Ű� �Ϻ����� �Ǵ�
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
