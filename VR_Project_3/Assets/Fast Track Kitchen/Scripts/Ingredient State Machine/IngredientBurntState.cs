using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBurntState : IngredientBaseState
{
    float timer = 0;
    public override void EnterState(IngredientStateManager ingredient)
    {
        ingredient.isCooking = false;
        ingredient.ingredient.ingredientSO = ingredient.burntSO;
        Debug.Log("burnt state");
        ingredient.grillSound.Stop();
        ingredient.burnSound.Play();
        ingredient.cookSmoke.SetActive(false);
        ingredient.burnSmoke.SetActive(true);
        ingredient.progressBarUI.SetActive(false);
        ingredient.burntObj.SetActive(true);
        ingredient.ingredientObj.SetActive(false);

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
