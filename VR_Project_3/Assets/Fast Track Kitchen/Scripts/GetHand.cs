using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHand : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DisableHandModel>())
        {
            other.gameObject.GetComponent<DisableHandModel>().leftHandModel = leftHand;
            other.gameObject.GetComponent<DisableHandModel>().rightHandModel = rightHand;
        }
    }
}
