using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VegetableSocket : MonoBehaviour
{

    public XRSocketInteractor socketInteractor;
   
    // Start is called before the first frame update
    void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();

       
        socketInteractor.selectEntered.AddListener(OnSocketed);
        socketInteractor.selectExited.AddListener(OnDesocketed);


    }

    private void OnSocketed(SelectEnterEventArgs args)
    {
        // Get the collider of the interactable object
        Collider collider = args.interactableObject.transform.GetComponent<Collider>();

        // Set the collider as a trigger collider
        collider.isTrigger = true;
      
    }

    private void OnDesocketed(SelectExitEventArgs args)
    {
        // Get the collider of the interactable object
        Collider collider = args.interactableObject.transform.GetComponent<Collider>();
        // Set the collider as a trigger collider
        collider.isTrigger = false;
    }


   
}
