using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Customer",menuName ="New Customer/Customer")]
public class CustomerSO : ScriptableObject
{
    [SerializeField] Sprite normalFace;
    [SerializeField] Sprite happyFace;
    [SerializeField] Sprite sadFace;
    [SerializeField] string customerName;
    [SerializeField] BurgerRecipe burger;

    public Sprite GetCustomerNormalFace()
    {
        return normalFace;
    }
    public string GetCustomerName()
    {
        return customerName;
    }
    public BurgerRecipe GetBurgerRecipe()
    {
        return burger;
    }

}
