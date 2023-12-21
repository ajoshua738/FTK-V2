using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateReticle : MonoBehaviour
{
    public GameObject reticle;
    public LayerMask layerMask;
    public float offset = 0.01f; // Adjust this offset value as needed
    public Transform origin;

    public GameObject reticleInstance;


    public int pourThreshold = 35;
    public bool isPouring = false;
    public float currentAngle;

    void Update()
    {
        // Create a ray pointing downwards from the current position
        Ray ray = new Ray(origin.position, Vector3.down);

        RaycastHit hit;
        bool pourCheck = CalculatePourAngle() < pourThreshold;

        if(isPouring != pourCheck)
        {
            isPouring = pourCheck;
        }
        // Perform the raycast
        if (Physics.Raycast(ray, out hit, 2.0f, layerMask) && isPouring)
        {
            // The ray hit an object in the specified layer mask
            Vector3 hitPoint = hit.point + Vector3.up * offset;

            // Check if the reticle is already instantiated
            if (reticleInstance == null)
            {
                // Instantiate the reticle with an offset
                reticleInstance = Instantiate(reticle, hitPoint, Quaternion.Euler(0, 0, 180));
            }
            else
            {
                // Update the reticle's position to the hit point with an offset
                reticleInstance.transform.position = hitPoint;
            }
        }
        else
        {
            // The raycast doesn't hit an object in the specified layer mask
            // Deactivate the reticle and destroy it if it exists
            if (reticleInstance != null)
            {
                reticleInstance.SetActive(false);
                Destroy(reticleInstance);
                reticleInstance = null;
            }
        }
    }

    private float CalculatePourAngle()
    {
        //print(transform.up.y * Mathf.Rad2Deg);
        currentAngle = transform.up.y * Mathf.Rad2Deg;
        return transform.up.y * Mathf.Rad2Deg;
    }
}
