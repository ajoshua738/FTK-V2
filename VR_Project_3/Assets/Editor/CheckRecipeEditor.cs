using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CheckRecipe))]
public class CheckRecipeEditor : Editor
{
    private SerializedProperty ingredientsProperty;

    private void OnEnable()
    {
        ingredientsProperty = serializedObject.FindProperty("ingredients");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(ingredientsProperty, true);

        if (GUILayout.Button("Aggregate Ingredients"))
        {
            if (target is CheckRecipe checkRecipe)
            {
                checkRecipe.AggregateIngredients();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
