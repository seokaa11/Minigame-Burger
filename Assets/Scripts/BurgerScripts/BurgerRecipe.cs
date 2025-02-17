using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/BurgerRecipe", fileName = "BurgerRecipe")]
public class BurgerRecipe : ScriptableObject
{
    [SerializeField] int BurgerNum; //버거 번호
    [SerializeField] string BurgerName;
    public List<GameObject> Ingredients; //버거 재료

    public List<GameObject> GetRecipe()
    {
        return Ingredients;
    }
    public string GetBurgerName()
    {
        return BurgerName;
    }
    public int GetBurgerNum()
    {
        return BurgerNum;
    }
    
}