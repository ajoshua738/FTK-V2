using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeStepManager : MonoBehaviour
{
    public TMP_Text recipeStepText;
    private int currentStep = 0;
    public RecipeStep[] recipeStepHints;

    void Start()
    {
        // Display the initial recipe step
        UpdateRecipeStepUI();
    }

    private void UpdateRecipeStepUI()
    {
        if (currentStep >= 0 && currentStep < recipeStepHints.Length)
        {
            recipeStepText.text = recipeStepHints[currentStep].stepDescription;
        }
        else
        {
            Debug.LogWarning("Invalid step index or recipe step not assigned!");
        }
    }

    public void ShowNextStep()
    {
        if (currentStep < recipeStepHints.Length - 1)
        {
            currentStep++;
            UpdateRecipeStepUI();
        }
    }

    public void ShowPreviousStep()
    {
        if (currentStep > 0)
        {
            currentStep--;
            UpdateRecipeStepUI();
        }
    }

    public void TriggerHintsForStep()
    {
        if (currentStep >= 0 && currentStep < recipeStepHints.Length)
        {
            RecipeStep currentRecipeStep = recipeStepHints[currentStep];
            foreach (PlayHint hintObject in currentRecipeStep.hintGameObjects)
            {
                hintObject.TriggerHint();
            }
        }
        else
        {
            Debug.LogWarning("Invalid step index or recipe step not assigned!");
        }
    }
}
