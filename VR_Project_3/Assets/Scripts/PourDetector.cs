﻿using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 35;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private WaterStream currentStream = null;
    public float currentAngle = 0;

    private void Update()
    {
        bool pourCheck = CalculatePourAngle() < pourThreshold;

        
        if(isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if(isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }
    }

    private void StartPour()
    {
        print("Start");
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        print("End");
        currentStream.End();
        currentStream = null;
    }


    private float CalculatePourAngle()
    {
        currentAngle = (transform.up.y * Mathf.Rad2Deg);
        return transform.up.y * Mathf.Rad2Deg;
    }

    private WaterStream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<WaterStream>();
    }
}