using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayHint : MonoBehaviour
{
    public GameObject prefab;  // GameObject to get materials from
    public AudioSource audioSource;   // Audio source for hint

    public Material[] defaultMaterials;  // Array to store materials
    public Material[] glowMaterials;
    public Material glowMat;
    public Material defMat;

    Renderer prefabRenderer;
    private void Start()
    {
        prefabRenderer = prefab.GetComponent<Renderer>(); // get the renderer
      
    
    }

    // Trigger hint effect
    public void TriggerHint()
    {
        // Enable the glow material for 3 seconds
        StartCoroutine(EnableGlowForDuration(10f));

        // Play hint audio
        audioSource.Play();
    }

    private IEnumerator EnableGlowForDuration(float duration)
    {

        prefabRenderer.materials = glowMaterials;


        yield return new WaitForSeconds(duration);

        // Disable the glow material after duration
        prefabRenderer.materials = defaultMaterials;
    }

    

    public void SetGlowMat()
    {
        //get the initial materials then add the glow mat
        glowMaterials = prefabRenderer.materials;
        glowMaterials[1] = glowMat;
    }
}
