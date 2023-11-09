using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    private Transform objectTransform;
    private Vector3 previousPosition;
    private Vector3 currentVelocity;

    public float minVelocityThreshold = 5.0f; // Adjust this threshold as needed

    private void Start()
    {
        objectTransform = transform;
        previousPosition = objectTransform.position;
    }

    private void Update()
    {
        Vector3 currentPosition = objectTransform.position;
        currentVelocity = (currentPosition - previousPosition) / Time.deltaTime;
        print(currentVelocity.magnitude);
        // Check if the magnitude of the current velocity is above the threshold
        if (currentVelocity.magnitude > minVelocityThreshold)
        {
            print("TRUE");
        }

        previousPosition = currentPosition;
    }
}
