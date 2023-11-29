using UnityEngine;

public abstract class IngredientBaseState 
{
    public abstract void EnterState(IngredientStateManager ingredient);

    public abstract void UpdateState(IngredientStateManager ingredient);

    public abstract void OnCollisionEnter(IngredientStateManager ingredient, Collision collision);

    public abstract void OnCollisionExit(IngredientStateManager ingredient, Collision collision);

    public abstract void OnTriggerEnter(IngredientStateManager ingredient, Collider other);

    public abstract void OnTriggerExit(IngredientStateManager ingredient, Collider other);

}
