using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class ToggleRayInteractor : MonoBehaviour
{
    [Tooltip("Actions to check")]
    public InputAction action = null;

    private bool isInteractorEnabled = false;

    // When the button is pressed
    public UnityEvent OnPress = new UnityEvent();

    // When the button is pressed again
    public UnityEvent OnRelease = new UnityEvent();

    private void Awake()
    {
        action.started += ToggleInteractor;
    }

    private void OnDestroy()
    {
        action.started -= ToggleInteractor;
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    private void ToggleInteractor(InputAction.CallbackContext context)
    {
        isInteractorEnabled = !isInteractorEnabled;

        if (isInteractorEnabled)
        {
            // Enable the ray interactor here
          
            OnPress.Invoke();
        }
        else
        {
            // Disable the ray interactor here
           
            OnRelease.Invoke();
        }
    }
}
