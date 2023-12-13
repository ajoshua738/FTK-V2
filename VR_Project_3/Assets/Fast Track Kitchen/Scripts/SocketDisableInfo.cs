using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketDisableInfo : MonoBehaviour
{
    public XRSocketTagInteractor socketInteractable;
    // Start is called before the first frame update
    void Start()
    {
        socketInteractable = GetComponent<XRSocketTagInteractor>();
        socketInteractable.selectEntered.AddListener(HideIndInfo);
        socketInteractable.selectExited.AddListener(DisableSocket);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideIndInfo(SelectEnterEventArgs args)
    {
        print("SOCKET DISABLE INFO");
        UIDisplayIngredientInfo a =  args.interactableObject.transform.GetComponent<UIDisplayIngredientInfo>();
        if(a != null )
        {
            a.ingredientInfoPrefab.SetActive(false);
            a.isSockted = true;
        }
  
    }

    public void DisableSocket(SelectExitEventArgs args)
    {
        UIDisplayIngredientInfo a = args.interactableObject.transform.GetComponent<UIDisplayIngredientInfo>();
        if(a != null)
        {
            a.isSockted = false;
        }
        
    }
}
