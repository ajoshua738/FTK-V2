using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddWater : MonoBehaviour
{
    public GameObject water;
    public float volume = 0.0f;
    public bool isPouring = false;

    private float timePassed = 0.0f; // Variable to keep track of time
    private float yIncrement = 0.01f; // Y-axis increment per second
    public GameObject waterIngredient;
  

    void Start()
    {
        water.SetActive(false);
       
    }

    void Update()
    {
        if (isPouring && volume < 8)
        {
            timePassed += Time.deltaTime; // Increment timePassed by the time since the last frame
            if (timePassed >= 1.0f) // Check if one second has passed
            {
                volume += 1.0f; // Add 1 to the volume
                if(volume >= 4.0f)
                {
                    gameObject.tag = "KitchenTool/Pot";
                }
                timePassed = 0.0f;
                GameObject waterObj = Instantiate(waterIngredient,transform.position,Quaternion.identity);
                waterObj.transform.SetParent(transform);
               

                water.transform.position = new Vector3(water.transform.position.x, water.transform.position.y + yIncrement, water.transform.position.z);
            }
        }

        if (volume > 0.0f)
        {
            water.SetActive(true);
        }
    }
}
