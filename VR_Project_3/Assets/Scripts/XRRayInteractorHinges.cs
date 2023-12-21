using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRRayInteractorHinges : XRRayInteractor
{

    // Start is called before the first frame update
    void Start()
    {
        hoverEntered.AddListener(args => CheckInteractable(args.interactableObject.transform, args.interactorObject.transform));
    }

    

    private void CheckInteractable(Transform interactableTransform, Transform interactorTransform)
    {
        if(interactableTransform != null)
        {
            if(interactableTransform.CompareTag("Hinge"))
            {
               
                interactorTransform.GetComponent<XRRayInteractorHinges>().useForceGrab = false;
                interactorTransform.GetComponent<XRRayInteractorHinges>().allowAnchorControl = true;
            }
            else
            {
                interactorTransform.GetComponent<XRRayInteractorHinges>().useForceGrab = true;
                interactorTransform.GetComponent<XRRayInteractorHinges>().allowAnchorControl = false;
                
            }
        }
    }
}
