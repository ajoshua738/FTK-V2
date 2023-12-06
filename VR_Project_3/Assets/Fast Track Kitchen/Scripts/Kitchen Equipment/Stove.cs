using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Stove : MonoBehaviour
{  
    
    public GameObject flame;

    public float minScale = 0.01f; // Minimum scale when knobValue is 0
    public float maxScale = 0.02f; // Maximum scale when knobValue is 1
    public List<GameObject> flames;
    public bool isOn = false;
    public int temp;
    public GameObject lowText;
    public GameObject mediumText;
    public GameObject highText;
    public XRKnob dial;
    public bool hasCorrectKitchenTool = false;
    public string correctKitchenTool;
    public XRSocketTagInteractor socket;
    
    // Start is called before the first frame update
    void Start()
    { 
        dial.onValueChange.AddListener(TurnOn);
        socket.selectEntered.AddListener(CheckItem);
        socket.selectExited.AddListener(RemoveItem);
        
        
        flame.SetActive(false);
        lowText.SetActive(false);
        mediumText.SetActive(false);
        highText.SetActive(false);
        AddFlames();

    }
    private void Update()
    {
       
    }

    public void CheckItem(SelectEnterEventArgs args)
    {
        //IXRSelectInteractable obj = socket.GetOldestInteractableSelected();
        //if(obj == null)
        //{
        //    return;
        //}
        //else if(obj.transform.CompareTag("KitchenTool/Pan"))
        //{
        //    hasCorrectKitchenTool = true;
        //}
        hasCorrectKitchenTool = true;
      
    }

    public void RemoveItem(SelectExitEventArgs args)
    {
        //IXRSelectInteractable obj = socket.GetOldestInteractableSelected();
        //if (obj == null)
        //{
        //    return;
        //}
        //else if (obj.transform.CompareTag("KitchenTool/Pan"))
        //{
        //    hasCorrectKitchenTool = false;
        //}
        hasCorrectKitchenTool = false;
    }
 

    public void AddFlames()
    {

        for(int i = 0; i < 25;i++)
        {
            Transform child = flame.transform.GetChild(i); // Get the child at index 'i'
            flames.Add(child.gameObject); // Add the child object to the list
        }
    }
    
    public void TurnOn(float knobValue)
    {
   

      
        if (knobValue < 0.1)
        {
            flame.SetActive(false);
            lowText.SetActive(false);
            mediumText.SetActive(false);
            highText.SetActive(false);

            isOn = false;

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
            isOn = true;
        }

      
        if (knobValue >= 0.1 && knobValue < 0.4)
        {
            isOn = true;
            SetLowTemp();
        }
        else if (knobValue >= 0.4 && knobValue < 0.7)
        {
            isOn = true;
            SetMediumTemp();
        }
        else if (knobValue > 0.7)
        {
            isOn = true;
            SetHighTemp();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.CompareTag(correctKitchenTool))
        //{
        //    hasCorrectKitchenTool = true;
        //}
       
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag(correctKitchenTool))
        //{
        //    hasCorrectKitchenTool = false;
        //}

    }

    public void SetLowTemp()
    {
        temp = 1;
        lowText.SetActive(true);
        mediumText.SetActive(false);
        highText.SetActive(false);
    }

    public void SetMediumTemp()
    {
        temp = 2;
        lowText.SetActive(false);
        mediumText.SetActive(true);
        highText.SetActive(false);
    }

    public void SetHighTemp()
    {
        temp = 3;
        lowText.SetActive(false);
        mediumText.SetActive(false);
        highText.SetActive(true);
    }




}
