using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Oven : MonoBehaviour
{
    public List<GameObject> lights;
    public bool isOn = false;

    public GameObject lowText;
    public GameObject mediumText;
    public GameObject highText;
    public XRKnob tempDial;
    public XRKnob timeDial;
    public XRSimpleInteractable button;
    public bool hasCorrectKitchenTool = false;
    public string correctKitchenTool;
    public int temp;
    public float ovenTimer;
    public TMP_Text timeText;
    float time = 0;
    public GameObject doorHinge;
    public bool activateCooking = false;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<XRSimpleInteractable>().selectEntered.AddListener(x => TurnOn());
        button.selectEntered.AddListener(x => TurnOn());
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
            isOn = false;
        }

        tempDial.onValueChange.AddListener(SetTemperature);
        timeDial.onValueChange.AddListener(SetTime);
   
        lowText.SetActive(false);
        mediumText.SetActive(false);
        highText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn && hasCorrectKitchenTool && doorHinge.transform.eulerAngles.y <= 95)
        {
            activateCooking = true;
            time += Time.deltaTime;

            if(time >= 1)
            {
                ovenTimer -= 1;
                timeText.text = ovenTimer.ToString();
                time = 0;
            }

            if(ovenTimer <= 0)
            {
                ovenTimer = 0;
                timeText.text = ovenTimer.ToString();
                TurnOn();
                activateCooking = false;
            }
           
        }
       
    }

    public void SetTime(float knobValue)
    {
        if(knobValue < 0.1)
        {
            ovenTimer = 0;
            timeText.text = ovenTimer.ToString();
        }
        else if(knobValue >= 0.1 && knobValue < 0.4)
        {
            ovenTimer = 10;
            timeText.text = ovenTimer.ToString();
        }
        else if (knobValue >= 0.4 && knobValue < 0.7)
        {
            ovenTimer = 30;
            timeText.text = ovenTimer.ToString();
        }
        else if (knobValue > 0.7)
        {
            ovenTimer = 60;
            timeText.text = ovenTimer.ToString();
        }


    }
    public void SetTemperature(float knobValue)
    {
        if(knobValue <0.1)
        {
            lowText.SetActive(false);
            mediumText.SetActive(false);
            highText.SetActive(false);
        }
        else if (knobValue >= 0.1 && knobValue < 0.4)
        {
            SetLowTemp();
        }
        else if (knobValue >= 0.4 && knobValue < 0.7)
        {
            SetMediumTemp();
        }
        else if (knobValue > 0.7)
        { 
            SetHighTemp();
        }
    }

    public void TurnOn()
    {

        if(isOn)
        {
            foreach(GameObject light in lights)
            {
                light.SetActive(false);
                isOn = false;
            }
        }
        else
        {
            foreach(GameObject light in lights)
            {
                light.SetActive(true);
                isOn = true;
            }

        }


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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(correctKitchenTool))
        {
            hasCorrectKitchenTool = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(correctKitchenTool))
        {
            hasCorrectKitchenTool = false;
        }

    }
}
