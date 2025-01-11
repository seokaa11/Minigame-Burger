using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ScoreData", fileName = "ScoreData")]
public class Scoredata : ScriptableObject
{
    public float Time;
    public bool isPerfectBurger;
    public int score;
    public float health;
    public string dialog;
}
