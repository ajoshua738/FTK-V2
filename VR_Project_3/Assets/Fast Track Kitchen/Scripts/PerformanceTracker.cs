using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PerformanceTracker : MonoBehaviour
{
    public float time;
    public int score = 0;
    public int maxScore = 0;
    public string grade;

   

  

    private void Awake()
    {
        SaveSystem.Init();
      
   
    }

    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public void checkScore()
    {

     

        if (score >= maxScore / 1.2)
        {
            grade = "A";
        }
        else if (score >= maxScore / 1.5 && score < maxScore / 1.5)
        {
            grade = "B";
        }
        else if (score >= maxScore / 2 && score < maxScore / 1.5)
        {
            grade = "C";
        }
        else
        {
            grade = "F";
        }
        Debug.Log("Time taken : " + time);
        Debug.Log("Grade : " + grade);

        PerformanceData data = new PerformanceData
        {
            time = time,
            score = score,
            grade = grade
        };

        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        SaveSystem.Save(json, "/BurgerLevel.json");
      ;

    }

   
    
}
