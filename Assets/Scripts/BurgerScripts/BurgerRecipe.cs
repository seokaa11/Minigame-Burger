using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/BurgerRecipe", fileName = "BurgerRecipe")]
public class BurgerRecipe : ScriptableObject
{
    [SerializeField] int burgerNum; //���� ��ȣ
    [SerializeField] string orderText;
    [SerializeField] string burgerName;
    public List<GameObject> ingredients; //���� ���

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