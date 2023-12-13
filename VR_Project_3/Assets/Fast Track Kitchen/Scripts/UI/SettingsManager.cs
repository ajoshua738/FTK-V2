using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsManager : MonoBehaviour
{
    public InputActionProperty pauseButton;
    bool isPaused = false;
    public GameObject pauseScreen;

    private void Awake()
    {

        pauseButton.action.performed += OnPauseButtonPressed;
    }

    private void Start()
    {
        pauseScreen.SetActive(false);   
    }
    private void OnEnable()
    {
        // Ensure the input action is enabled
        pauseButton.action.Enable();
    }

    private void OnDisable()
    {
        // Disable the input action
        pauseButton.action.Disable();
    }


    private void Update()
    {
        
    }
   

    private void OnDestroy()
    {
        pauseButton.action.performed -= OnPauseButtonPressed;
    }

 
    private void OnPauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.0f)
        {
            if(!isPaused)
            {
                isPaused = true;
           
                pauseScreen.SetActive(true);
              
            }
            else
            {
                isPaused = false;
            
                pauseScreen.SetActive(false);
          
            }
          
        }
    }
}
