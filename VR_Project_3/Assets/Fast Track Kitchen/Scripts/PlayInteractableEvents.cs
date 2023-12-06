using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayInteractableEvents : MonoBehaviour
{

    public float transitionDuration = 0.25f;

    //Change colors when hovering
    public bool changeColorOnHover = false;
    public Renderer rend;
    Color defaultColor;
    public Color hoverColor = new Color(0, 0.5894128f, 0.9803922f, 1);
    float colorTransitionTime = 1f;
    Color currentColor;
    Color desiredColor;


    //change scale when hovering
    public bool changeScaleOnHover = false;
    public Transform obj;
    Vector3 hoverScale;
    Vector3 defaultScale;
    Vector3 currentScale;
    Vector3 desiredScale;
    public float scaleMultiplier = 1.1f;
    float scaleTransitionTime = 1f;

    //Change color when pressing activate
    public bool changeColorOnActivate = false;
    private bool isActivating = false;
    public Color activateColor = new Color(0.5660378f, 0.5660378f, 0.5660378f, 1);


    //Play audio when hovering
    public bool playAudioOnHover = false;
    public AudioSource hoverAudio;


    public bool playAudioOnActivate = false;
    public AudioSource activateAudio;


    void Start()
    {
        defaultColor = rend.material.color;
        defaultScale = obj.localScale;
        hoverScale = defaultScale * scaleMultiplier;

        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
       
        if(changeColorOnHover)
        {
            grabInteractable.hoverEntered.AddListener(args => SetMaterial(args.interactorObject.transform, hoverColor));
            grabInteractable.hoverExited.AddListener(args => SetMaterial(args.interactorObject.transform, defaultColor));
            grabInteractable.selectEntered.AddListener(args => SetMaterial(args.interactorObject.transform, defaultColor));
        }

        if(changeScaleOnHover)
        {
            grabInteractable.hoverEntered.AddListener(args => SetScale(args.interactorObject.transform, hoverScale));
            grabInteractable.hoverExited.AddListener(args => SetScale(args.interactorObject.transform, defaultScale));
        }


        if(changeColorOnActivate)
        {
            grabInteractable.activated.AddListener(args => ActivateMaterial(args.interactorObject.transform));
        }

        if(playAudioOnHover)
        {
            grabInteractable.hoverEntered.AddListener(args => PlayHoverAudio(args.interactorObject.transform));
        }

        if(playAudioOnActivate)
        {
            grabInteractable.activated.AddListener(args => PlayActivateAudio(args.interactorObject.transform));
        }
        
    }

    private void Update()
    {

        if (scaleTransitionTime < transitionDuration)
        {
            scaleTransitionTime += Time.deltaTime;

            float t = Mathf.Clamp01(scaleTransitionTime / transitionDuration);
            obj.localScale = Vector3.Lerp(currentScale, desiredScale, t);
        }


        if (colorTransitionTime < transitionDuration)
        {
            colorTransitionTime += Time.deltaTime;

            float t = Mathf.Clamp01(colorTransitionTime / transitionDuration);
            rend.material.color = Color.Lerp(currentColor, desiredColor, t);

            if (t >= 1.0f && isActivating)
            {
                isActivating = false;
                colorTransitionTime = 0f;
                currentColor = rend.material.color;
                desiredColor = defaultColor;
            }
        }
    }

    void SetMaterial(Transform interactorTransform, Color targetColor)
    {
        if (interactorTransform.tag == "LeftHand" || interactorTransform.tag == "RightHand")
        {
            colorTransitionTime = 0f;
            currentColor = rend.material.color;
            desiredColor = targetColor;
        }
    
    }

    void ActivateMaterial(Transform interactorTransform)
    {
        if (interactorTransform.tag == "LeftHand" || interactorTransform.tag == "RightHand")
        {
            isActivating = true;
            colorTransitionTime = 0f;
            currentColor = rend.material.color;
            desiredColor = activateColor;
        }
    }

    void SetScale(Transform interactorTransform, Vector3 targetScale)
    {
        if (interactorTransform.tag == "LeftHand" || interactorTransform.tag == "RightHand")
        {
            scaleTransitionTime = 0f;
            currentScale = obj.localScale;
            desiredScale = targetScale;
            
        }
    }

    void PlayHoverAudio(Transform interactorTransform)
    {
        if (interactorTransform.tag == "LeftHand" || interactorTransform.tag == "RightHand")
        {
            if(!hoverAudio.isPlaying)
            {
                hoverAudio.Play();
            }
          
        }
    }

    void PlayActivateAudio(Transform interactorTransform)
    {
        if (interactorTransform.tag == "LeftHand" || interactorTransform.tag == "RightHand")
        {
            activateAudio.Play();
        }
    }

 

}
