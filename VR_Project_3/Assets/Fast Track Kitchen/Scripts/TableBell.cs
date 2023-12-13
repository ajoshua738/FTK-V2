using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TableBell : MonoBehaviour
{
    public UnityEvent RingBell;
    public AudioSource bell;
    public bool canBeCalled = false;
    public GameObject glowObj;

    private void Start()
    {
        glowObj.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canBeCalled)
        {
            if (other.gameObject.CompareTag("RightHand") || other.gameObject.CompareTag("LeftHand"))
            {
                RingBell.Invoke();
                bell.Play();
            }
        }
       
    }
}
