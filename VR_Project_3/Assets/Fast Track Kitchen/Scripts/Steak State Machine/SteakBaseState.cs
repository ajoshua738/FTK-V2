using UnityEngine;

public abstract class SteakBaseState
{
    public abstract void EnterState(SteakStateManager steak);

    public abstract void UpdateState(SteakStateManager steak);

    public abstract void OnCollisionEnter(SteakStateManager steak, Collision collision);

    public abstract void OnCollisionExit(SteakStateManager steak, Collision collision);

    public abstract void OnTriggerEnter(SteakStateManager steak, Collider other);

    public abstract void OnTriggerExit(SteakStateManager steak, Collider other);

}
