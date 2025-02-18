using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/BurgerRecipe", fileName = "BurgerRecipe")]
public class BurgerRecipe : ScriptableObject
{
    [SerializeField] int burgerNum; //버거 번호
    [SerializeField] string orderText;
    [SerializeField] string burgerName;
    public List<GameObject> ingredients; //버거 재료

    public List<GameObject> GetRecipe()
    {
        return ingredients;
    }
    public string GetBurgerName()
    {
        return burgerName;
    }
    public int GetBurgerNum()
    {
        return burgerNum;
    }
    public string GetOrderText()
    {
        return orderText;
    }
}