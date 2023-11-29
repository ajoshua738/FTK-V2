using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RadioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] songs;
    public int currentSongIndex = 0;
    float timer = 0;
    public TMP_Text songName;
    public GameObject songContainer;
    public bool isOn = false;
    public GameObject onText;
    public GameObject offText;
    // Start is called before the first frame update
    void Start()
    {
        songContainer.SetActive(isOn);
        onText.SetActive(!isOn);
        offText.SetActive(isOn);
    }

    public void Play()
    {
        if(songs.Length > 0 && audioSource != null)
        {
            audioSource.clip = songs[currentSongIndex];
            audioSource.Play();
        }
        SetSongName();
    }

    public void TurnOn()
    {
        isOn = !isOn; 
        songContainer.SetActive(isOn);
        onText.SetActive(!isOn);
        offText.SetActive(isOn);
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            Play();
        }
    }

    private void Update()
    {

        if (!audioSource.isPlaying && isOn)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                Next();
                timer = 0;
            }

        }

    }

    public void Pause()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else if(audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void Next()
    {
        if (songs.Length == 0 || audioSource == null)
            return;

        currentSongIndex = (currentSongIndex + 1) % songs.Length;
        audioSource.Stop();
        audioSource.clip = songs[currentSongIndex];
        audioSource.Play();
        SetSongName();
    }

    public void Previous()
    {
        if (songs.Length == 0 || audioSource == null)
            return;

        currentSongIndex--;
        if (currentSongIndex < 0)
            currentSongIndex = songs.Length - 1;

        audioSource.Stop();
        audioSource.clip = songs[currentSongIndex];
        audioSource.Play();
        SetSongName();
    }

    void SetSongName()
    {
        songName.text = audioSource.clip.name;
    }

}
