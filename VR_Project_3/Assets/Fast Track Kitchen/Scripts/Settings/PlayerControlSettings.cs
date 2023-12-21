using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class PlayerControlSettings : MonoBehaviour
{
    public Button addButton;
    public Button minusButton;
    public string fileName;
    public TMP_Text heightText;
    public Transform playerCam;
    public float incrementAmount = 0.1f; // The amount by which to increase the y value
  
    
    // Start is called before the first frame update
    private void Awake()
    {
        SaveSystem.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedLoad(5f));
       
        addButton.onClick.AddListener(AddHeight);
        minusButton.onClick.AddListener(MinusHeight);
        
    }
   


    
    public void AddHeight()
    {

        if (playerCam.localPosition.y <= 2)
        {
            playerCam.localPosition += new Vector3(0f, incrementAmount, 0f);
            heightText.text = playerCam.localPosition.y.ToString();
        }



       

        string saveString = SaveSystem.Load("/" + fileName);
        if (saveString != null)
        {
            PlayerControlSettingsData data = JsonUtility.FromJson<PlayerControlSettingsData>(saveString);


            data.playerHeight = playerCam.localPosition;

            // Serialize the updated data back to JSON
            string updatedJson = JsonUtility.ToJson(data);

            // Save the updated JSON data back to the file
            SaveSystem.Save(updatedJson, "/" + fileName);
        }
    }

    public void MinusHeight()
    {
        if (playerCam.localPosition.y >= 0)
        {
            playerCam.localPosition -= new Vector3(0f, incrementAmount, 0f);
            heightText.text = playerCam.localPosition.y.ToString();
        }

        
        string saveString = SaveSystem.Load("/" + fileName);
        if (saveString != null)
        {
            PlayerControlSettingsData data = JsonUtility.FromJson<PlayerControlSettingsData>(saveString);


            data.playerHeight = playerCam.localPosition;

            // Serialize the updated data back to JSON
            string updatedJson = JsonUtility.ToJson(data);

            // Save the updated JSON data back to the file
            SaveSystem.Save(updatedJson, "/" + fileName);
        }
    }

    IEnumerator DelayedLoad(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadSettings();
    }
    public void LoadSettings()
    {
        string saveString = SaveSystem.Load("/" + fileName);
      
        if (saveString != null)
        {
            PlayerControlSettingsData data = JsonUtility.FromJson<PlayerControlSettingsData>(saveString);

            if(data != null)
            {
                playerCam.localPosition = data.playerHeight;

              
            }
            


        }
        else
        {
            PlayerControlSettingsData data = new PlayerControlSettingsData
            {
                playerHeight = playerCam.localPosition

            };
         

            string json = JsonUtility.ToJson(data);
            Debug.Log(json);

            SaveSystem.Save(json, "/" + fileName);

        }

        heightText.text = playerCam.localPosition.y.ToString();

       
    }

}
