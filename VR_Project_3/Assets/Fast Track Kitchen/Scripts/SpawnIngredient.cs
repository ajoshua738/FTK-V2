using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIngredient : MonoBehaviour
{

    [SerializeField] private LayerMask collisionLayerMask;
    [SerializeField] private float raycastDistance = 1.0f;
    private RaycastHit hitInfo;

    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnPoint;

    private float timer = 0;
    // Update is called once per frame
    void Update()
    {


        if (!Physics.Raycast(transform.position, transform.up, out hitInfo, raycastDistance, collisionLayerMask))
        {
          
            timer += Time.deltaTime;
            if (timer > 2.0f)
            {
                timer = 0.0f;
                Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            }

        }

        // Draw the raycast for visualization
        //Debug.DrawRay(transform.position, transform.up * raycastDistance, Color.red);
    }

}
