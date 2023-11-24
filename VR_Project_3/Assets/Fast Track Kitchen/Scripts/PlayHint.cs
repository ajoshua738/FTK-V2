using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering;

public class PlayHint : MonoBehaviour
{

    public GameObject model;
  
    public AudioSource hintSound;
 
    private void Start()
    {
        model.SetActive(false);
    }

    // Trigger hint effect
    public void TriggerHint()
    {
        // Enable the glow material for 3 seconds
        StartCoroutine(EnableGlowForDuration(10f));

        // Play hint audio
        hintSound.Play();
    }

    private IEnumerator EnableGlowForDuration(float duration)
    {

        model.SetActive(true);


        yield return new WaitForSeconds(duration);

        model.SetActive(false);
    }

}
