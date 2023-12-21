using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBurntState : IngredientBaseState
{
    float timer = 0;
    public override void EnterState(IngredientStateManager ingredient)
    {
        ingredient.kitchenToolUI.ChangeIngredientName(ingredient.cookedSO.ingredientName, ingredient.burntSO.ingredientName);
        ingredient.ingredientObj.SetActive(false);
        ingredient.cookedObj.SetActive(false);
        ingredient.isCooking = false;
        ingredient.ingredient.ingredientSO = ingredient.burntSO;
        Debug.Log("burnt state");
        ingredient.cookSound.Stop();
        ingredient.burnSound.Play();
        ingredient.cookSmoke.SetActive(false);
        ingredient.burnSmoke.SetActive(true);
     
        ingredient.burntObj.SetActive(true);
        Object.Destroy(ingredient.progressBarPrefab);
        ingredient.kitchenToolUI.ChangeIngredientName(ingredient.cookedSO.ingredientName, ingredient.burntSO.ingredientName);

    }

    public override void OnCollisionEnter(IngredientStateManager ingredient, Collision collision)
    {
     
    }

    public override void OnCollisionExit(IngredientStateManager ingredient, Collision collision)
    {
      
    }

    public override void OnTriggerEnter(IngredientStateManager ingredient, Collider other)
    {
     
    }

    public override void OnTriggerExit(IngredientStateManager ingredient, Collider other)
    {
        
    }

    public override void UpdateState(IngredientStateManager ingredient)
    {
        timer += Time.deltaTime;

        if (timer > 5)
        {
            ingredient.burnSmoke.SetActive(false);
        }
    }

    
}
