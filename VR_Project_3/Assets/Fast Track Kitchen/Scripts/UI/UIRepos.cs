using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRepos : MonoBehaviour
{

    public Transform canvasTransform; // Reference to the canvas transform
    public Transform startPoint; // Start point transform
    public Transform endPoint; // End point transform

    public float startAngle = 0f; // Starting angle
    public float endAngle = 90f; // End angle

    public float transitionSpeed = 2.0f; // Speed of transition

    void Update()
    {
        // Calculate the normalized progress between start and end angles
        float normalizedProgress = Mathf.InverseLerp(startAngle, endAngle, transform.eulerAngles.x);

        // Calculate the target position based on the progress between start and end points
        Vector3 targetPosition = Vector3.Lerp(startPoint.position, endPoint.position, normalizedProgress);

        // Smoothly move the canvas to the target position
        canvasTransform.position = Vector3.Lerp(canvasTransform.position, targetPosition, Time.deltaTime * transitionSpeed);
    }
}
