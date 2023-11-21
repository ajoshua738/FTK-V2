using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Oven : MonoBehaviour
{
    public List<GameObject> lights;
    bool isLightOn = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(x => TurnOn());
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
            isLightOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOn()
    {

        if(isLightOn)
        {
            foreach(GameObject light in lights)
            {
                light.SetActive(false);
                isLightOn = false;
            }
        }
        else
        {
            foreach(GameObject light in lights)
            {
                light.SetActive(true);
                isLightOn = true;
            }

        }


    }
}
