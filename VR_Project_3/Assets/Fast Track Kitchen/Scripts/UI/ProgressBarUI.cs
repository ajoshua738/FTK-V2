using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    public GameObject progressBar;
    public Image img;
    public float cookTime = 10;
    public float cookInterval = 1.0f;
    float timer = 0;
    float increment = 0.1f;
    public float cookTimer = 0;



    // Start is called before the first frame update
    void Start()
    {
        progressBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (progressBar.activeInHierarchy && cookTimer < cookTime)
        {
            timer += Time.deltaTime;

            if(timer > cookInterval)
            {
                timer = 0;
                img.fillAmount += increment;
                cookTime++;
            }
        }
        else
        {
            progressBar.SetActive(false);
        }

        
    }

    
}
