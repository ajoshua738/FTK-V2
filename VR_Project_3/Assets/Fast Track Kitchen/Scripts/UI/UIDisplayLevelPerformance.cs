using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDisplayLevelPerformance : MonoBehaviour
{

    public TMP_Text time;
    public TMP_Text score;
    public TMP_Text grade;
    public string fileName;
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    
    public void Load()
    {
        string saveString = SaveSystem.Load("/" + fileName);
        if (saveString != null)
        {
            PerformanceData data = JsonUtility.FromJson<PerformanceData>(saveString);
            time.text = data.time.ToString("F2");
            score.text = data.score.ToString();
            grade.text = data.grade;

        }
    }
}
