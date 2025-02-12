using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Customer",menuName ="New Customer/Customer")]
public class CustomerSO : ScriptableObject
{
    [SerializeField] Sprite normalFace;
    [SerializeField] Sprite happyFace;
    [SerializeField] Sprite sadFace;

}
