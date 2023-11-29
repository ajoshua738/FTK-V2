using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class IngredientStateManager : MonoBehaviour
{

    public IngredientBaseState currentState;
    public IngredientRawState rawState = new IngredientRawState();
    public IngredientCookedState cookedState = new IngredientCookedState();
    public IngredientBurntState burntState = new IngredientBurntState();



    public AudioSource grillSound;
    public AudioSource burnSound;


    public GameObject progressBarUI;
    public Image img;


    public Ingredient ingredient;
    public IngredientSO cookedSO;
    public IngredientSO burntSO;




    public float progress;
    public float lowTempProgress;
    public float mediumTempProgress;
    public float highTempProgress;

    public Stove stove = null;

    public bool isCooking = false;
    public float timer = 0;

    public GameObject cookSmoke;
    public GameObject burnSmoke;
    public GameObject burntObj;
    public GameObject ingredientObj;
    // Start is called before the first frame update
    void Start()
    {
        burntObj.SetActive(false);
        cookSmoke.SetActive(false);
        burnSmoke.SetActive(false);
        progressBarUI.SetActive(false);
        currentState = rawState;
        currentState.EnterState(this);
        ingredient = GetComponent<Ingredient>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        currentState.OnCollisionExit(this, collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this, other);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        //Debug.Log(currentState);

    }

    public void SwitchState(IngredientBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
