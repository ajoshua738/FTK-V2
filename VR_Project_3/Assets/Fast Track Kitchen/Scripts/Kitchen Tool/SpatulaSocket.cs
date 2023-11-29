using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatulaSocket : MonoBehaviour
{
    public GameObject socket;
    bool isEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        socket.SetActive(isEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableSocket()
    {
        isEnabled = !isEnabled;
        socket.SetActive(isEnabled);
    }
}
