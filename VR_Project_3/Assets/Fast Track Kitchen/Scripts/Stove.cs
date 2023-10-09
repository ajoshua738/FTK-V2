using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class Stove : MonoBehaviour
{  
    
    public GameObject flame;

    public float minScale = 0.01f; // Minimum scale when knobValue is 0
    public float maxScale = 0.02f; // Maximum scale when knobValue is 1
    public List<GameObject> flames;
    // Start is called before the first frame update
    void Start()
    { 
        GetComponent<XRKnob>().onValueChange.AddListener(TurnOn);
        flame.SetActive(false);
        
    }

    
    public void TurnOn(float knobValue)
    {
        Debug.Log("knob value : "+knobValue);

      
        if (knobValue == 0)
        {
            flame.SetActive(false);

        }
        else if(knobValue >= 0.1)
        {
            flame.SetActive(true);
            // Calculate the new scale based on knobValue
            float newScaleValue = Mathf.Lerp(minScale, maxScale, knobValue);
            // Loop through the objects in the list and set their scales
            foreach (GameObject obj in flames)
            {
                obj.transform.localScale = new Vector3(newScaleValue, newScaleValue, newScaleValue);
            }
        }


    }
}
