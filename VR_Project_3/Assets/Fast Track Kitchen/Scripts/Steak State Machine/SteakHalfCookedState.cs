using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteakHalfCookedState : SteakBaseState
{
    //bool hasCalledIsCookingEvent = false;
    bool reset = false;
    
    public override void EnterState(SteakStateManager steak)
    {
        Debug.Log("Entereed half cooked state");
        steak.ingredient.ingredientSO = steak.halfCookedSO;
        steak.progressBarImg.fillAmount = 0;
        steak.progress = 0;
        steak.rawObj.SetActive(false);
        steak.cookedObj.SetActive(true);
        steak.hasCalledIsCookingEvent = false;
        steak.kitchenToolUI.ChangeIngredientName(steak.rawSO.ingredientName, steak.halfCookedSO.ingredientName);
      
      
    }

    public override void OnCollisionEnter(SteakStateManager steak, Collision collision)
    {
       
    }

    public override void OnCollisionExit(SteakStateManager steak, Collision collision)
    {
       
    }

    public override void OnTriggerEnter(SteakStateManager steak, Collider other)
    {
        if (other.gameObject.CompareTag(steak.stoveTag))
        {
            steak.isOnStove = true;
        }
        if(other.gameObject.CompareTag(steak.ovenTag))
        {
            steak.oven = other.GetComponent<Oven>();
            steak.isOnOven = true;
        }
    }

    public override void OnTriggerExit(SteakStateManager steak, Collider other)
    {
        if (other.gameObject.CompareTag(steak.stoveTag))
        {
            steak.isOnStove = false;
        }
        if (other.gameObject.CompareTag(steak.ovenTag))
        {
            steak.isOnOven = false;
        }
    }

    public override void UpdateState(SteakStateManager steak)
    {

        //if (steak.progressBarInstance != null && steak.objectToFollow != null)
        //{
        //    // Update the progress bar's position to follow the object
        //    steak.progressBarInstance.transform.position = steak.objectToFollow.position + Vector3.up * steak.yOffset;
        //}

        if (steak.isOnStove)
        {
            if (steak.stove.isOn && steak.stove.hasCorrectKitchenTool)
            {
                steak.progressBarImg.color = Color.red;
                steak.timer += Time.deltaTime;
                if (!steak.hasCalledIsCookingEvent)
                {
                    steak.hasCalledIsCookingEvent = true;
                    IsCookingEvents(steak);
                }

                if (steak.timer > 1)
                {
                    if (steak.stove.temp == 0)
                    {
                        steak.progress += steak.lowTempProgress;

                    }
                    else if (steak.stove.temp == 1)
                    {
                        steak.progress += steak.mediumTempProgress;

                    }
                    else
                    {
                        steak.progress += steak.highTempProgress;
                    }


                    steak.progressBarImg.fillAmount = steak.progress;

                    if (steak.progress >= 1)
                    {
                        steak.progress = 1;
                        steak.SwitchState(steak.burntState);
                    }
                    steak.timer = 0;
                }


            }
            


        }

        if(steak.isOnOven)
        {
            if (steak.oven != null && steak.oven.activateCooking)
            {
                if (!reset)
                {
                    steak.progressBarImg.fillAmount = 0;
                    steak.progress = 0;
                    reset = true;
                }
                steak.progressBarImg.color = Color.green;
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


                    steak.progressBarImg.fillAmount = steak.progress;

                    if (steak.progress >= 1)
                    {
                        steak.progress = 1;
                        steak.SwitchState(steak.cookedState);
                    }
                    steak.timer = 0;
                }


            }
           

        }

        if (steak.isOnStove)
        {
            if (!steak.stove.isOn || !steak.stove.hasCorrectKitchenTool)
            {
                steak.hasCalledIsCookingEvent = false;
                IsNotCookingEvents(steak);
            }
        }
        else if (steak.isOnOven && steak.oven != null)
        {
            if(!steak.oven.activateCooking)
            {
                steak.hasCalledIsCookingEvent = false;
                IsNotCookingEvents(steak);
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
        steak.progressBarPrefab.SetActive(true);
    }

    public void IsNotCookingEvents(SteakStateManager steak)
    {

        steak.cookSound.Stop();
        steak.cookSmoke.SetActive(false);
        steak.progressBarPrefab.SetActive(false);
    }

    public void IsBurning(SteakStateManager steak)
    {

    }
}
