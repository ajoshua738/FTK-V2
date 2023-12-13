using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public string fileName;
 
    private void Awake()
    {
        SaveSystem.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume)*20);

        string saveString = SaveSystem.Load("/" + fileName);
        if (saveString != null)
        {
            VolumeSettingsData data = JsonUtility.FromJson<VolumeSettingsData>(saveString);

            // Update the music volume
            data.musicVolume = volume;

            // Serialize the updated data back to JSON
            string updatedJson = JsonUtility.ToJson(data);

            // Save the updated JSON data back to the file
            SaveSystem.Save(updatedJson, "/" + fileName);
        }
       

    }

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);

        string saveString = SaveSystem.Load("/" + fileName);


        if (saveString != null)
        {
            VolumeSettingsData data = JsonUtility.FromJson<VolumeSettingsData>(saveString);

            // Update the SFX volume
            data.sfxVolume = volume;

            // Serialize the updated data back to JSON
            string updatedJson = JsonUtility.ToJson(data);

            // Save the updated JSON data back to the file
            SaveSystem.Save(updatedJson, "/" + fileName);
        }
      
    }

    public void LoadSettings()
    {
        string saveString = SaveSystem.Load("/" + fileName);
        if (saveString != null)
        {
            VolumeSettingsData data = JsonUtility.FromJson<VolumeSettingsData>(saveString);
            musicSlider.value = data.musicVolume;
            SetMusicVolume();
            sfxSlider.value = data.sfxVolume;
            SetSfxVolume();

        }
        else
        {
            VolumeSettingsData data = new VolumeSettingsData
            {
                musicVolume = musicSlider.value,
                sfxVolume = sfxSlider.value,
            };

            string json = JsonUtility.ToJson(data);
            Debug.Log(json);

            SaveSystem.Save(json, "/" + fileName);

        }
    }

}
