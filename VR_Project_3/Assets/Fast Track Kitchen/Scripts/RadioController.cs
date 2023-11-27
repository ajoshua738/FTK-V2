using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] songs;
    private int currentSongIndex = 0;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (songs.Length > 0 && audioSource != null)
        {
            audioSource.clip = songs[currentSongIndex];
            audioSource.Play();
        }
    }

    public void Play()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
       
    }

    private void Update()
    {
        
        if (!audioSource.isPlaying)
        {
            timer += Time.deltaTime;
            if(timer > 5)
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
    }

    public void Next()
    {
        if (songs.Length == 0 || audioSource == null)
            return;

        currentSongIndex = (currentSongIndex + 1) % songs.Length;
        audioSource.Stop();
        audioSource.clip = songs[currentSongIndex];
        audioSource.Play();
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
    }

}
