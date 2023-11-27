using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class IngredientSO : ScriptableObject
{
    public string ingredientName;
    public float ingredientAmount;
    public string unit;
}
