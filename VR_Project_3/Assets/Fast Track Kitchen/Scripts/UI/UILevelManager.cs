using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelManager : MonoBehaviour
{
    public GameObject[] levelContainers; // Array to hold your level containers
    private int currentLevelIndex = 0;

    void Start()
    {
        ShowCurrentLevel();
    }

    void ShowCurrentLevel()
    {
        // Disable all level containers
        for (int i = 0; i < levelContainers.Length; i++)
        {
            levelContainers[i].SetActive(false);
        }

        // Enable only the current level container
        levelContainers[currentLevelIndex].SetActive(true);
    }

    public void NextLevel()
    {
        if (currentLevelIndex < levelContainers.Length - 1)
        {
            currentLevelIndex++;
            ShowCurrentLevel();
        }
    }

    public void PreviousLevel()
    {
        if (currentLevelIndex > 0)
        {
            currentLevelIndex--;
            ShowCurrentLevel();
        }
    }
}
