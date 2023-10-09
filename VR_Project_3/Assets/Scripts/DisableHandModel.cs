using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableHandModel : MonoBehaviour
{

    public GameObject leftHandModel;
    public GameObject rightHandModel;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HideHand);
        grabInteractable.selectExited.AddListener(ShowHand);
    }


    public void HideHand(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.tag == "LeftHand")
        {
            leftHandModel.SetActive(false);
        }
        else if(args.interactorObject.transform.tag == "RightHand")
        {
            rightHandModel.SetActive(false);
        }
    }

    public void ShowHand(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.tag == "LeftHand")
        {
            leftHandModel.SetActive(true);
        }
        else if (args.interactorObject.transform.tag == "RightHand")
        {
            rightHandModel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
