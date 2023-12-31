using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteakBurntState : SteakBaseState
{
    float timer = 0;
    public override void EnterState(SteakStateManager steak)
    {
        Debug.Log("Entereed burnt state");
        steak.isOnStove = false;
        steak.isOnOven = false;
        steak.ingredient.ingredientSO = steak.burntSO;
        steak.cookSound.Stop();
        steak.burnSound.Play();
      
        steak.burnSmoke.SetActive(true);
      
        steak.burntObj.SetActive(true);
        steak.cookedObj.SetActive(false);
        Object.Destroy(steak.progressBarPrefab);
        Object.Destroy(steak.cookSmoke);
        steak.kitchenToolUI.ChangeIngredientName(steak.halfCookedSO.ingredientName, steak.burntSO.ingredientName);
    }

    public override void OnCollisionEnter(SteakStateManager steak, Collision collision)
    {
      
    }

    public override void OnCollisionExit(SteakStateManager steak, Collision collision)
    {
   
    }

    public override void OnTriggerEnter(SteakStateManager steak, Collider other)
    {
      
    }

    public override void OnTriggerExit(SteakStateManager steak, Collider other)
    {
      
    }

    public override void UpdateState(SteakStateManager steak)
    {
       

        timer += Time.deltaTime;

        if (timer > 5)
        {
            Object.Destroy(steak.burnSmoke);
        }
    }
}
