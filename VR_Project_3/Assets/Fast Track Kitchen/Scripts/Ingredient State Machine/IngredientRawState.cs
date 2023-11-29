using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IngredientRawState : IngredientBaseState
{
    bool hasCalledIsCookingEvent = false;
    public override void EnterState(IngredientStateManager ingredient)
    {
        
    }

    public override void OnCollisionEnter(IngredientStateManager ingredient, Collision collision)
    {
      
    }

    public override void OnCollisionExit(IngredientStateManager ingredient, Collision collision)
    {
        
    }

    public override void OnTriggerEnter(IngredientStateManager ingredient, Collider other)
    {
        if (other.gameObject.CompareTag("KitchenEquipment/Stove"))
        {
            ingredient.stove = other.GetComponent<Stove>();
            ingredient.isCooking = true;
        }
    }

    public override void OnTriggerExit(IngredientStateManager ingredient, Collider other)
    {
        if (other.gameObject.CompareTag("KitchenEquipment/Stove"))
        {
            ingredient.isCooking = false;
            IsNotCookingEvents(ingredient);
        }
    }

    public override void UpdateState(IngredientStateManager ingredient)
    {
        if(ingredient.stove == null)
        {
            return;
        }

        if(ingredient.isCooking && ingredient.stove.isOn && ingredient.stove.hasPan)
        {
            ingredient.timer += Time.deltaTime;
            if (!hasCalledIsCookingEvent)
            {
                hasCalledIsCookingEvent = true;
                IsCookingEvents(ingredient);
            }

            if(ingredient.timer > 1)
            {
                if (ingredient.stove.temp == 0)
                {
                    ingredient.progress += ingredient.lowTempProgress;

                }
                else if (ingredient.stove.temp == 1)
                {
                    ingredient.progress += ingredient.mediumTempProgress;

                }
                else
                {
                    ingredient.progress += ingredient.highTempProgress;
                }

               
                ingredient.img.fillAmount = ingredient.progress;

                if (ingredient.progress >= 1)
                {
                    ingredient.progress = 1;
                    ingredient.SwitchState(ingredient.cookedState);
                }
                ingredient.timer = 0;
            }


        }
        else
        {
            hasCalledIsCookingEvent = false;
            IsNotCookingEvents(ingredient);
        }
    }

    public void IsCookingEvents(IngredientStateManager ingredient)
    {

        ingredient.grillSound.Play();
        ingredient.cookSmoke.SetActive(true);
        ingredient.progressBarUI.SetActive(true);
    }

    public void IsNotCookingEvents(IngredientStateManager ingredient)
    {

        ingredient.grillSound.Stop();
        ingredient.cookSmoke.SetActive(false);
        ingredient.progressBarUI.SetActive(false);
    }
}
