using System.Collections;
using System.Collections.Generic;
using TMPro;
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





    public Ingredient ingredient;
    public IngredientSO rawSO;
    public IngredientSO cookedSO;
    public IngredientSO halfCookedSO;
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



    public GameObject progressBarPrefab;
    public float yOffset = 0.1f; // Adjust this value to set the height above the object
    public Transform objectToFollow;
    //public GameObject progressBarInstance;
    public Image progressBarImg;

    public UIDisplayCurrentIngredients kitchenToolUI;
    // Start is called before the first frame update
    void Start()
    {
      
        burntObj.SetActive(false);
        cookSmoke.SetActive(false);
        burnSmoke.SetActive(false);
       
        currentState = rawState;
        currentState.EnterState(this);
        ingredient = GetComponent<Ingredient>();

        //progressBarInstance = Instantiate(progressBarPrefab, transform.position + Vector3.up * yOffset, Quaternion.identity);
        objectToFollow = transform;

        //foreach (Transform child in progressBarInstance.transform)
        //{
        //    if (child.CompareTag("ProgressBar"))
        //    {
        //        progressBarImg = child.gameObject.GetComponent<Image>();
        //    }
        //}
        progressBarPrefab.SetActive(false);
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
        if (other.gameObject.CompareTag("KitchenToolUI"))
        {
            kitchenToolUI = other.GetComponent<UIDisplayCurrentIngredients>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this, other);
        if (other.gameObject.CompareTag("KitchenToolUI") && kitchenToolUI != null)
        {
            kitchenToolUI = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        //Debug.Log(currentState);
        if (progressBarPrefab != null && objectToFollow != null)
        {
            // Update the progress bar's position to follow the object
            progressBarPrefab.transform.position = objectToFollow.position + Vector3.up * yOffset;
        }

        if(cookSmoke != null && objectToFollow != null)
        {
            cookSmoke.transform.position = objectToFollow.position + Vector3.up * yOffset;
        }

        if (burnSmoke != null && objectToFollow != null)
        {
            burnSmoke.transform.position = objectToFollow.position + Vector3.up * yOffset;
        }

    }

    public void SwitchState(SteakBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
