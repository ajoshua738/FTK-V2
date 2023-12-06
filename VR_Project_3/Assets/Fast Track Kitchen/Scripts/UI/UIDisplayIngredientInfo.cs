using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UIDisplayIngredientInfo : MonoBehaviour
{
    public GameObject textPrefab;
    public GameObject UIContainer;
    private float amount;
    public bool canBeUpdated;
    public bool isSockted = false;
    int count = 0;

  
    // Start is called before the first frame update
    void Start()
    {
        UIContainer.SetActive(false);
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(x => ShowInfo());
        grabInteractable.selectExited.AddListener(x => HideInfo());
        grabInteractable.activated.AddListener(x => UpdateInfo());
    
       
        amount = GetComponent<Ingredient>().ingredientSO.ingredientAmount;
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInfo()
    {
     
        if (!isSockted)
        {
            UIContainer.SetActive(true);
        }
       
    }

    public void HideInfo()
    {
       
        UIContainer.SetActive(false);
        if (canBeUpdated)
        {
            ResetAmount();
        }
           
    }

    public void UpdateInfo()
    {

        
        if (canBeUpdated && count > 0) 
        {
            TextMeshProUGUI amountText = textPrefab.transform.Find("Amount Text").GetComponent<TextMeshProUGUI>();
            float updatedAmount = float.Parse(amountText.text) + amount;
            amountText.text = updatedAmount.ToString();
        }
        count++;

    }

    public void ResetAmount()
    {
        TextMeshProUGUI amountText = textPrefab.transform.Find("Amount Text").GetComponent<TextMeshProUGUI>();
        
        amountText.text = amount.ToString();
        count = 0;
    }
}


