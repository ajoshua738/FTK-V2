using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerHandsManager : MonoBehaviour
{
    public GameObject hand;
    public XRDirectInteractor directInteractor;
    public XRRayInteractor rayInteractor;
    public LayerMask layerMask;
   
    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.selectEntered.AddListener(HideHand);
        rayInteractor.selectExited.AddListener(ShowHand);
        directInteractor.selectEntered.AddListener(HideHand);
        directInteractor.selectExited.AddListener(ShowHand);
    }


    public void ShowHand(SelectExitEventArgs args)
    {

        if ((layerMask & (1 << args.interactableObject.transform.gameObject.layer)) != 0)
        {
            hand.SetActive(true);
        }

    }

    public void HideHand(SelectEnterEventArgs args)
    {

        if ((layerMask & (1 << args.interactableObject.transform.gameObject.layer)) != 0)
        {
            hand.SetActive(false);
        }

    }
}
