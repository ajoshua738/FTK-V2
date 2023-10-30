using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class PattyRawState : PattyBaseState
{
    float timer = 0f;
    float cookTimer = 0f;
    float cookInterval = 1f;
    bool isCooking = false;
    float increment = 0.1f;
    float t = 0;

   
    public override void EnterState(PattyStateManager patty)
    {
        Debug.Log("Raw state");
    }

   

    public override void UpdateState(PattyStateManager patty)
    {
        if(isCooking)
        {
            Debug.Log("Is cooking");
            timer += Time.deltaTime;
           

            if(timer >= cookInterval)
            {
                t += increment;
                patty.pattyMat.SetFloat("_Blend", t);
                cookTimer += cookInterval;
                timer = 0f;
            }

            if (cookTimer >= 10f)
            {
                isCooking = false;
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
            isCooking = true;
            patty.grillSound.Play();
            patty.cookSmoke.SetActive(true);
        }
    }

    public override void OnCollisionExit(PattyStateManager patty, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("KitchenEquipment/Griddle"))
        {
            isCooking = false;
            patty.grillSound.Stop();
            patty.cookSmoke.SetActive(false);
        }
    }
}
