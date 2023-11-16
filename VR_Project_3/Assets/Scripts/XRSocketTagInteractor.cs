using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketTagInteractor : XRSocketInteractor
{
    public List<string> targetTags = new List<string>();
    public Material noHoverMat;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && HasMatchingTag(interactable.transform);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && HasMatchingTag(interactable.transform);
    }

    private bool HasMatchingTag(Transform interactableTransform)
    {
        if(targetTags != null)
        {
            foreach (string tag in targetTags)
            {
                if (interactableTransform.CompareTag(tag))
                {
                    return true;
                }
              
            }
            return false;
        }
       
        return true;
    }


    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        base.OnSelectEntered(interactable);

        // Check if an object is socketed and disable the "Can't Hover" mesh.
        if (attachTransform != null)
        {
            interactableCantHoverMeshMaterial = null;
        }
    }

   

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        base.OnSelectExited(interactable);

       
        if (interactableCantHoverMeshMaterial == null)
        {
            interactableCantHoverMeshMaterial = noHoverMat;
        }
    }
}
