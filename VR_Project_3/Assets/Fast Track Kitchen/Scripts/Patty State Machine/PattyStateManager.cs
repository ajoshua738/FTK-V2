using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PattyStateManager : MonoBehaviour
{
    public PattyBaseState currentState;
    public PattyRawState RawState = new PattyRawState();
    public PattyCookedState CookedState = new PattyCookedState();
    public PattyBurntState BurntState = new PattyBurntState();


    //To change burger material from raw to cooked
    public GameObject pattyObj;
    public Material pattyMat;
    public Material burnMat;
    public AudioSource grillSound;
    public AudioSource burnSound;
    public GameObject cookSmoke;
    public GameObject burnSmoke;


    public GameObject progressUI;
    public Image img;








    public float cookTime = 10.0f; //Time it takes to cook the food
    public float cookTimer = 0f; //Keeps track of total time in seconds that has passed
    public float timer = 0f; //Timer to keep track of how many seconds passsed, gets reset after passing the interval
    
    public float cookInterval = 0.0f; // How many seconds should it take to update the progress
    public bool isCooking = false;
  
    public float increment = 0;// the value to add to change the material


    public Ingredient ingredient;
    public IngredientSO cookedPattySO;
    public IngredientSO burntPattySO;


    // Start is called before the first frame update
    void Start()
    {
        cookInterval = cookTime / 10.0f;
        progressUI.SetActive(false);
        cookSmoke.SetActive(false);
        burnSmoke.SetActive(false);
        //burger material stuff

        Renderer rend = pattyObj.GetComponent<Renderer>();
        pattyMat = rend.material;
        //

        currentState = RawState;
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

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        //Debug.Log(currentState);
    }

    public void SwitchState(PattyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
   
}
