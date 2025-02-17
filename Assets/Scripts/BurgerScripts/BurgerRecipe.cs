using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/BurgerRecipe", fileName = "BurgerRecipe")]
public class BurgerRecipe : ScriptableObject
{
    [SerializeField] int BurgerNum; //���� ��ȣ
    [SerializeField] string BurgerName;
    public List<GameObject> Ingredients; //���� ���

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