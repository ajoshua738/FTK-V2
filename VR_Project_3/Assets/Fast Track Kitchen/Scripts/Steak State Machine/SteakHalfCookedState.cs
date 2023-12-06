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
        steak.ingredient.ingredientSO = steak.halfCooekdSO;
        steak.img.fillAmount = 0;
        steak.progress = 0;
        steak.rawObj.SetActive(false);
        steak.cookedObj.SetActive(true);
        steak.hasCalledIsCookingEvent = true;
      
      
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
        // Logic for the Stove
        //if (steak.isOnStove && steak.stove != null)
        //{
        //    if (steak.stove.isOn && steak.stove.hasCorrectKitchenTool)
        //    {
             
        //        ProcessCooking(steak, steak.stove.temp, Color.red);
        //    }
        //    else
        //    {
        //        hasCalledIsCookingEvent = true;
        //        IsNotCookingEvents(steak);
        //    }
        //}
       


        //// Logic for the Oven
        //if (steak.isOnOven && steak.oven != null)
        //{
        //    if (steak.oven.isOn && steak.oven.hasCorrectKitchenTool)
        //    {
        //        if (!hasReset)
        //        {
        //            steak.progress = 0;
        //            steak.timer = 0;
        //            hasReset = true;
        //        }
        //        ProcessCooking(steak, steak.oven.temp, Color.green);
        //    }
        //    else
        //    {
        //        hasCalledIsCookingEvent = true;
        //        IsNotCookingEvents(steak);
        //    }
        //}

        if(steak.isOnStove)
        {
            if (steak.stove.isOn && steak.stove.hasCorrectKitchenTool)
            {
                steak.img.color = Color.red;
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


                    steak.img.fillAmount = steak.progress;

                    if (steak.progress >= 1)
                    {
                        steak.progress = 1;
                        steak.SwitchState(steak.burntState);
                    }
                    steak.timer = 0;
                }


            }
          
        }

        if(steak.isOnOven && steak.oven.activateCooking)
        {
            if(!reset)
            {
                steak.img.fillAmount = 0;
                steak.progress = 0;
                reset = true;
            }
            steak.img.color = Color.green;
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
                    steak.SwitchState(steak.cookedState);
                }
                steak.timer = 0;
            }


        }


        if(!steak.isOnStove && !steak.isOnOven)
        {
            steak.hasCalledIsCookingEvent = false;
            IsNotCookingEvents(steak);
        }
      



    }

    public void ProcessCooking(SteakStateManager steak, int temperature, Color indicatorColor)
    {
        steak.timer += Time.deltaTime;
        if (!steak.hasCalledIsCookingEvent)
        {
            steak.hasCalledIsCookingEvent = true;
            IsCookingEvents(steak);
        }

        if (steak.timer > 1)
        {
            steak.img.color = indicatorColor;

            
            switch (temperature)
            {
                case 0: // Low temperature
                    steak.progress += steak.lowTempProgress;
                    break;
                case 1: // Medium temperature
                    steak.progress += steak.mediumTempProgress;
                    break;
                case 2: // High temperature
                    steak.progress += steak.highTempProgress;
                    break;
                default:
                    break;
            }

          
            steak.img.fillAmount = steak.progress;

            if (steak.progress >= 1)
            {
                steak.progress = 1;
                steak.SwitchState(steak.cookedState);
            }
            steak.timer = 0;
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

    public void IsBurning(SteakStateManager steak)
    {

    }
}
