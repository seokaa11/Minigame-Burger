using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/BurgerRecipe", fileName = "BurgerRecipe")]
public class BurgerRecipe : ScriptableObject
{
    public int BurgerNum; //버거 번호
    public List<GameObject> Ingredients; //버거 재료

    public List<GameObject> GetRecipe()
    {
        return Ingredients;
}
}

