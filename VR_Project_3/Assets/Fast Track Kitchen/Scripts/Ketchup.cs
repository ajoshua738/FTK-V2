using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ketchup : MonoBehaviour
{
    public AudioSource ketchupSound;
    public GameObject ketchup;
    public Transform origin;
    public LayerMask layerMask;
    private GenerateReticle generateReticle;
   
    // Start is called before the first frame update
    void Start()
    {
        generateReticle = GetComponent<GenerateReticle>();
    }


    public void KetchupEvent()
    {
        ketchupSound.Play();

        // Instantiate the ketchup GameObject at the hit point of the raycast
        if (Physics.Raycast(origin.position, Vector3.down, out RaycastHit hit, 2.0f, layerMask) && generateReticle.isPouring)
        {
            Instantiate(ketchup, hit.point, Quaternion.identity);
        }

    }
  
}
