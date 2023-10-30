using UnityEngine;

public abstract class PattyBaseState
{
    public abstract void EnterState(PattyStateManager patty);

    public abstract void UpdateState(PattyStateManager patty);

    public abstract void OnCollisionEnter(PattyStateManager patty, Collision collision);

    public abstract void OnCollisionExit(PattyStateManager patty, Collision collision);

}
   
    

