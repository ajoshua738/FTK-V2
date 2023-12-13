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



    public AudioSource cookSound;
    public AudioSource burnSound;


 


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
   
    public GameObject ingredientObj;
    public GameObject cookedObj;
    public GameObject burntObj;
    public string kitchenEquipmentTag;

    public GameObject progressBarPrefab;
    public float yOffset = 0.1f; // Adjust this value to set the height above the object
    public Transform objectToFollow;
   //public GameObject progressBarInstance;
    public Image progressBarImg;
    // Start is called before the first frame update
    void Start()
    {
        cookedObj.SetActive(false);
        burntObj.SetActive(false);
        cookSmoke.SetActive(false);
        burnSmoke.SetActive(false);
   
        currentState = rawState;
        currentState.EnterState(this);
        ingredient = GetComponent<Ingredient>();

       // progressBarInstance = Instantiate(progressBarPrefab, transform.position + Vector3.up * yOffset, Quaternion.identity);
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
        if (progressBarPrefab != null && objectToFollow != null)
        {
            // Update the progress bar's position to follow the object
            progressBarPrefab.transform.position = objectToFollow.position + Vector3.up * yOffset;
        }

    }

    public void SwitchState(IngredientBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
