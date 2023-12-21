using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFridgeLights : MonoBehaviour
{
    public GameObject[] lights;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var light in lights)
        {
            light.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameObject.transform.eulerAngles.y >= 275)
        {
            foreach (var light in lights)
            {
                light.SetActive(true);
            }
        }
        else
        {
            foreach (var light in lights)
            {
                light.SetActive(false);
            }
        }
    }
}
