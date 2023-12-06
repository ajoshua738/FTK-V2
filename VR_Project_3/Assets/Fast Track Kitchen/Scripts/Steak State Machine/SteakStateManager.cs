using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteakStateManager : MonoBehaviour
{
    public SteakBaseState currentState;
    public SteakRawState rawState = new SteakRawState();
    public SteakHalfCookedState halfCookedState = new SteakHalfCookedState();
    public SteakCookedState cookedState = new SteakCookedState();
    public SteakBurntState burntState = new SteakBurntState();


    public AudioSource cookSound;
    public AudioSource burnSound;


    public GameObject progressBarUI;
    public Image img;


    public Ingredient ingredient;
    public IngredientSO cookedSO;
    public IngredientSO halfCooekdSO;
    public IngredientSO burntSO;

    public float progress;
    public float lowTempProgress;
    public float mediumTempProgress;
    public float highTempProgress;

    public Stove stove = null;
    public Oven oven = null;
    

    public bool isOnStove = false;
    public bool isOnOven = false;
    public float timer = 0;

    public GameObject cookSmoke;
    public GameObject burnSmoke;
    public GameObject rawObj;
    public GameObject cookedObj;
    public GameObject burntObj;
    public string stoveTag;
    public string ovenTag;

    public bool hasCalledIsCookingEvent = false;
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

    public void SwitchState(SteakBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
