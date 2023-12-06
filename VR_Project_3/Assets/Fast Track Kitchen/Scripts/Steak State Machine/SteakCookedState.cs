using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteakCookedState : SteakBaseState
{
    public override void EnterState(SteakStateManager steak)
    {
        Debug.Log("Entereed cooked state");
        steak.ingredient.ingredientSO = steak.cookedSO;
        steak.img.fillAmount = 0;
        steak.progress = 0;
        steak.img.color = Color.red;

    }

    public override void OnCollisionEnter(SteakStateManager steak, Collision collision)
    {
      
    }

    public override void OnCollisionExit(SteakStateManager steak, Collision collision)
    {
       
    }

    public override void OnTriggerEnter(SteakStateManager steak, Collider other)
    {
        if (other.gameObject.CompareTag(steak.ovenTag))
        {
         
            steak.isOnOven = true;
        }
    }

    public override void OnTriggerExit(SteakStateManager steak, Collider other)
    {
        if (other.gameObject.CompareTag(steak.ovenTag))
        {
            steak.isOnOven = false;
        }
    }

    public override void UpdateState(SteakStateManager steak)
    {
        if (steak.isOnOven && steak.oven.activateCooking)
        {
            steak.timer += Time.deltaTime;
            if (!steak.hasCalledIsCookingEvent)
            {
                steak.hasCalledIsCookingEvent = true;
                IsCookingEvents(steak);
            }

            if (steak.timer > 1)
            {
                if (steak.oven.temp == 0)
                {
                    steak.progress += steak.lowTempProgress;

                }
                else if (steak.oven.temp == 1)
                {
                    steak.progress += steak.mediumTempProgress;

                }
                else
                {
                    steak.progress += steak.highTempProgress;
                }


                steak.img.fillAmount = steak.progress;

                if (steak.progress >= 1)
                {
                    steak.progress = 1;
                    steak.SwitchState(steak.burntState);
                }
                steak.timer = 0;
            }
        }
        else
        {
            steak.hasCalledIsCookingEvent = false;
            IsNotCookingEvents(steak);
        }
    }


    public void IsCookingEvents(SteakStateManager steak)
    {

        steak.cookSound.Play();
        steak.cookSmoke.SetActive(true);
        steak.progressBarUI.SetActive(true);
    }

    public void IsNotCookingEvents(SteakStateManager steak)
    {

        steak.cookSound.Stop();
        steak.cookSmoke.SetActive(false);
        steak.progressBarUI.SetActive(false);
    }

}
