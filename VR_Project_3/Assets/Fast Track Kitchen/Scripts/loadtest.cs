using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class loadtest : MonoBehaviour
{
    public TMP_Text time;
    public TMP_Text score;
    public TMP_Text grade;

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        string saveString = SaveSystem.Load("/BurgerLevel.json");
        if(saveString != null)
        {
            PerformanceData data = JsonUtility.FromJson<PerformanceData>(saveString);
            time.text = data.time.ToString("F2");
            score.text = data.score.ToString();
            grade.text = data.grade;
           
        }
    }
}
