using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LoadPlayerData : MonoBehaviour
{

    public Transform cameraOffset;
    
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(DelayedLoad(5f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DelayedLoad(float delay)
    {
        yield return new WaitForSeconds(delay);
        Load();
    }


    public void Load()
    {
        string saveString = SaveSystem.Load("/PlayerData.json");
        if (saveString != null)
        {
            PlayerData data = JsonUtility.FromJson<PlayerData>(saveString);
            cameraOffset.localPosition = data.playerHeight;
            
            
           

        }
    }
}
