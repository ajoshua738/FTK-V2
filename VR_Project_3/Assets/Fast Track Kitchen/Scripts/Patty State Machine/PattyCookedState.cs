using UnityEngine;

public class PattyCookedState : PattyBaseState
{

    bool isCooking = true;
    float increment = 0.1f;
    float t = 0;
    float timer = 0f;
    float cookTimer = 0f;
    float cookInterval = 1f;

    public override void EnterState(PattyStateManager patty)
    {
        Debug.Log("cooked state");
    }

    public override void OnCollisionEnter(PattyStateManager patty, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("KitchenEquipment/Griddle"))
        {
            isCooking = true;
            patty.cookSmoke.SetActive(true);
        }
    }

    public override void OnCollisionExit(PattyStateManager patty, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("KitchenEquipment/Griddle"))
        {
            isCooking = false;
            patty.cookSmoke.SetActive(false);
        }
    }

    public override void UpdateState(PattyStateManager patty)
    {
        if (isCooking)
        {
            Debug.Log("Is cooking");
            timer += Time.deltaTime;


            if (timer >= cookInterval)
            {
                t += increment;
              
                cookTimer += cookInterval;
                timer = 0f;
            }

            if (cookTimer >= 10f)
            {
                isCooking = false;
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
