using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public float incrementAmount = 0.1f; // The amount by which to increase the y value
    public Transform cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseHeight()
    {
        cameraOffset.localPosition += new Vector3(0f, incrementAmount, 0f);

        PlayerData data = new PlayerData
        {
            playerHeight = cameraOffset.localPosition
        };


        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        SaveSystem.Save(json, "/PlayerData.json");

    }
}
