using UnityEngine;

public class PattyCookedState : PattyBaseState
{

    

    public override void EnterState(PattyStateManager patty)
    {
        Debug.Log("cooked state");

        patty.img.fillAmount = 0;
        patty.img.color = Color.red;
        patty.cookTimer = 0;
        patty.isCooking = true;
        patty.increment = 0;
    }

    public override void OnCollisionEnter(PattyStateManager patty, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("KitchenEquipment/Griddle"))
        {
            patty.isCooking = true;
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
            patty.cookSmoke.SetActive(false);
            patty.progressUI.SetActive(false);
        }
    }

    public override void UpdateState(PattyStateManager patty)
    {
        if (patty.isCooking)
        {
            Debug.Log("Is cooking");
            patty.timer += Time.deltaTime;


            if (patty.timer >= patty.cookInterval)
            {

                patty.increment += 0.1f;
                patty.cookTimer += patty.cookInterval;
                patty.timer = 0f;
                patty.img.fillAmount = patty.increment;
            }

            if (patty.cookTimer >= patty.cookTime)
            {
                patty.isCooking = false;
                Debug.Log("Is burnt");
                patty.SwitchState(patty.BurntState);
            }
        }
        else
        {
            Debug.Log("Not cooking");
        }
    }
}
