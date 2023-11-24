using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Content.Interaction;
using System.Drawing;

public class Griddle : MonoBehaviour
{
    public GameObject lowText;
    public GameObject mediumText;
    public GameObject highText;
    public bool isOn = false;
    public XRKnob dial;
    public int temp;
    // Start is called before the first frame update
    void Start()
    {
        dial.onValueChange.AddListener(TurnOn);
        lowText.SetActive(false);
        mediumText.SetActive(false);
        highText.SetActive(false);
    }

    public void TurnOn(float knobValue)
    {
        if (knobValue == 0)
        {
            lowText.SetActive(false);
            mediumText.SetActive(false);
            highText.SetActive(false);

            isOn = false;

        }
        else if (knobValue >= 0.1 && knobValue < 0.4)
        {
            isOn = true;
            SetLowTemp();
        }
        else if (knobValue >= 0.4 && knobValue < 0.7)
        {
            isOn = true;
            SetMediumTemp();
        }
        else if(knobValue > 0.7)
        {
            isOn = true;
            SetHighTemp();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
