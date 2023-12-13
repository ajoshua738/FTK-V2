using UnityEngine;

public class PattyRawState : PattyBaseState
{

    bool hasCalledIsCookingEvent = false;

    public override void EnterState(PattyStateManager patty)
    {
        Debug.Log("Raw state");
    }

   

    public override void UpdateState(PattyStateManager patty)
    {
        if(patty.griddle == null)
        {
            return;
        }
        
        if (patty.griddle.isOn && patty.isCooking)
        {
            patty.timer += Time.deltaTime;
           
            if(!hasCalledIsCookingEvent)
            {
                hasCalledIsCookingEvent = true;
                IsCookingEvents(patty);
            }

            if(patty.timer >= 1)
            {
                if (patty.griddle.temp == 0)
                {
                    patty.progress += patty.lowTempProgress;
                  
                }
                else if (patty.griddle.temp == 1)
                {
                    patty.progress += patty.mediumTempProgress;
                
                }
                else
                {
                    patty.progress += patty.highTempProgress;
                }

                patty.pattyMat.SetFloat("_Blend", patty.progress);
                patty.progressBarImg.fillAmount = patty.progress;

                if (patty.progress >= 1)
                {
                    patty.progress = 1;
                    patty.SwitchState(patty.CookedState);
                }
                patty.timer = 0;
            }

        }
        else
        {
            hasCalledIsCookingEvent = false;
        }

    }

    public override void OnCollisionEnter(PattyStateManager patty, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("KitchenEquipment/Griddle"))
        {
            patty.griddle = other.GetComponent<Griddle>();
            patty.isCooking = true;
          
          
        }
    }

    public void IsCookingEvents(PattyStateManager patty)
    {
       
        patty.grillSound.Play();
        patty.cookSmoke.SetActive(true);
        patty.progressBarInstance.SetActive(true);
    }

    public void IsNotCookingEvents(PattyStateManager patty)
    {
        
        patty.grillSound.Stop();
        patty.cookSmoke.SetActive(false);
        patty.progressBarInstance.SetActive(false);
    }
    public override void OnCollisionExit(PattyStateManager patty, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("KitchenEquipment/Griddle"))
        {
            patty.isCooking = false;
            IsNotCookingEvents(patty);
        }
    }
}
