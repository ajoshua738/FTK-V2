using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PattyStateManager : MonoBehaviour
{
    public PattyBaseState currentState;
    public PattyRawState RawState = new PattyRawState();
    public PattyCookedState CookedState = new PattyCookedState();
    public PattyBurntState BurntState = new PattyBurntState();


    //To change burger material from raw to cooked
    public GameObject pattyObj;
    public Material pattyMat;
    public AudioSource grillSound;
    public AudioSource burnSound;
    public GameObject cookSmoke;
    public GameObject burnSmoke;

    // Start is called before the first frame update
    void Start()
    {
        cookSmoke.SetActive(false);
        burnSmoke.SetActive(false);
        //burger material stuff

        Renderer rend = pattyObj.GetComponent<Renderer>();
        pattyMat = rend.material;
        //

        currentState = RawState;
        currentState.EnterState(this);
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
        Debug.Log(currentState);
    }

    public void SwitchState(PattyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
   
}
