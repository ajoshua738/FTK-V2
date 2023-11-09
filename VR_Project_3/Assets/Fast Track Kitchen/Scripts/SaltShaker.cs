using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltShaker : MonoBehaviour
{
    public AudioSource shakeSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaltShakerEvent()
    {
        shakeSound.Play();
    }
}
