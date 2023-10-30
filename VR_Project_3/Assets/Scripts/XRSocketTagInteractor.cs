using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketTagInteractor : XRSocketInteractor
{
    public List<string> targetTags = new List<string>();

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
        foreach (string tag in targetTags)
        {
            if (interactableTransform.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
}
