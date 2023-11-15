using UnityEngine;

public class PattyBurntState : PattyBaseState
{

    float timer = 0;
    
    public override void EnterState(PattyStateManager patty)
    {
        patty.ingredient.ingredientSO = patty.burntPattySO;
        Debug.Log("burnt state");
        patty.grillSound.Stop();
        patty.burnSound.Play();
        patty.cookSmoke.SetActive(false);
        patty.burnSmoke.SetActive(true);
        patty.progressUI.SetActive(false);
        patty.pattyObj.GetComponent<Renderer>().material = patty.burnMat;
    }

    public override void OnCollisionEnter(PattyStateManager patty, Collision collision)
    {
       
    }

    public override void OnCollisionExit(PattyStateManager patty, Collision collision)
    {
       
    }

    public override void UpdateState(PattyStateManager patty)
    {
        timer += Time.deltaTime;

        if(timer >5)
        {
            patty.burnSmoke.SetActive(false);
        }
    }
}
