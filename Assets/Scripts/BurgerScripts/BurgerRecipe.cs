using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/BurgerRecipe", fileName = "BurgerRecipe")]
public class BurgerRecipe : ScriptableObject
{
    public int BurgerNum; //���� ��ȣ
    public List<GameObject> Ingredients; //���� ���

    public List<GameObject> GetRecipe()
    {
        return Ingredients;
}
}

