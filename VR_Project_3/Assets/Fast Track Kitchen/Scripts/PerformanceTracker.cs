using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class PerformanceTracker : MonoBehaviour
{
    public float time;
    public int score = 0;
    public int maxScore = 0;
    public string grade;
    public string fileName;

    public TMP_Text timeText;
    public TMP_Text scoreText;
    public TMP_Text gradeText;

    //burger level = 110


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

     

        if (score >= (maxScore / 1.2))
        {
            grade = "A";
        }
        else if (score >= (maxScore / 1.5) && score < (maxScore / 1.2))
        {
            grade = "B";
        }
        else if (score >= (maxScore / 2) && score < (maxScore / 1.5))
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

        SaveSystem.Save(json, "/" + fileName);

        timeText.text = data.time.ToString();
        scoreText.text = data.score.ToString();
        gradeText.text = data.grade.ToString();


    }

   
    
}
