using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    public bool hasCorrectKitchenTool = false;
    public GameObject onText;
    public GameObject offText;
    public GameObject sinkObject;
    public bool isOn = false;
    public AddWater addWater = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(addWater != null)
        {
            if(hasCorrectKitchenTool && isOn)
            {
                addWater.isPouring = true;
            }
            else
            {
                addWater.isPouring = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Kitchen Tool"))
        {
            if (other.gameObject.CompareTag("KitchenTool/Pot"))
            {
                hasCorrectKitchenTool = true;
                addWater = other.gameObject.GetComponent<AddWater>();
            }
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Kitchen Tool"))
        {
            if (other.gameObject.CompareTag("KitchenTool/Pot"))
            {
                hasCorrectKitchenTool = false;
              
            }
        }

    }
    public void TurnOn()
    {
       
        onText.SetActive(isOn);
        offText.SetActive(!isOn);
        if(!isOn)
        {
            sinkObject.transform.rotation = Quaternion.Euler(0f, 90f, -90f);
        }
        else
        {
            sinkObject.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        isOn = !isOn;

    }
 
}
