using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteakRawState : SteakBaseState
{
    bool hasCalledIsCookingEvent = false;
    public override void EnterState(SteakStateManager steak)
    {
       
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
            steak.stove = other.GetComponent<Stove>();
            steak.isOnStove = true;
        }
    }

    public override void OnTriggerExit(SteakStateManager steak, Collider other)
    {
        if (other.gameObject.CompareTag(steak.stoveTag))
        {
            steak.isOnStove = false;
        }
    }

    public override void UpdateState(SteakStateManager steak)
    {

        if (steak.stove == null)
        {
            return;
        }

        if (steak.isOnStove && steak.stove.isOn && steak.stove.hasCorrectKitchenTool)
        {
            steak.timer += Time.deltaTime;
            if (!hasCalledIsCookingEvent)
            {
                hasCalledIsCookingEvent = true;
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
                    steak.SwitchState(steak.halfCookedState);
                }
                steak.timer = 0;
            }


        }
        else
        {
            hasCalledIsCookingEvent = false;
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
