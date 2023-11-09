using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class PattyRawState : PattyBaseState
{
   


    public override void EnterState(PattyStateManager patty)
    {
        Debug.Log("Raw state");
    }

   

    public override void UpdateState(PattyStateManager patty)
    {
        if(patty.isCooking)
        {
            Debug.Log("Is cooking");
            patty.timer += Time.deltaTime;
           

            //Update Cook progress every 1 second
            if(patty.timer >= patty.cookInterval)
            {
                patty.increment += 0.1f;
                patty.pattyMat.SetFloat("_Blend", patty.increment);
                patty.cookTimer += patty.cookInterval;
                patty.timer = 0f;
                patty.img.fillAmount = patty.increment;
            }

            //Cooked at 10 seconds
            if (patty.cookTimer >= patty.cookTime)
            {
                patty.isCooking = false;
                Debug.Log("Is cooked");
                patty.SwitchState(patty.CookedState);
            }
        }
        else
        {
            Debug.Log("Not cooking");
        }

    }

    public override void OnCollisionEnter(PattyStateManager patty, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("KitchenEquipment/Griddle"))
        {
            patty.isCooking = true;
            patty.grillSound.Play();
            patty.cookSmoke.SetActive(true);
            patty.progressUI.SetActive(true);
        }
    }

    public override void OnCollisionExit(PattyStateManager patty, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("KitchenEquipment/Griddle"))
        {
            patty.isCooking = false;
            patty.grillSound.Stop();
            patty.cookSmoke.SetActive(false);
            patty.progressUI.SetActive(false);
        }
    }
}
