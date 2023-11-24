using UnityEngine;

public class PattyCookedState : PattyBaseState
{

    bool hasCalledIsCookingEvent = false;

    public override void EnterState(PattyStateManager patty)
    {
        patty.ingredient.ingredientSO = patty.cookedPattySO;
        Debug.Log("cooked state");

        patty.img.fillAmount = 0;
        patty.img.color = Color.red;
        //patty.cookTimer = 0;
        
        //patty.increment = 0;
        patty.progress = 0;
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

    public override void OnCollisionExit(PattyStateManager patty, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("KitchenEquipment/Griddle"))
        {
            patty.isCooking = false;
            IsNotCookingEvents(patty);
        }
    }

    public void IsCookingEvents(PattyStateManager patty)
    {

        patty.grillSound.Play();
        patty.cookSmoke.SetActive(true);
        patty.progressBarUI.SetActive(true);
    }

    public void IsNotCookingEvents(PattyStateManager patty)
    {

        patty.grillSound.Stop();
        patty.cookSmoke.SetActive(false);
        patty.progressBarUI.SetActive(false);
    }


    public override void UpdateState(PattyStateManager patty)
    {


        if (patty.griddle.isOn && patty.isCooking)
        {
            patty.timer += Time.deltaTime;

            if (!hasCalledIsCookingEvent)
            {
                hasCalledIsCookingEvent = true;
                IsCookingEvents(patty);
            }

            if (patty.timer >= 1)
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

              
                patty.img.fillAmount = patty.progress;

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
}
