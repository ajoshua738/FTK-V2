using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UIDisplayIngredientInfo : MonoBehaviour
{

    private float amount;
    public bool canBeUpdated;
    public bool isSockted = false;
    int count = 0;

    public GameObject ingredientInfoPrefab;
    public float yOffset = 0.1f; // Adjust this value to set the height above the object
    Transform objectToFollow;
  
    public Transform textPrefab;
    // Start is called before the first frame update
    void Start()
    {
        ingredientInfoPrefab.SetActive(false);
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(x => ShowInfo());
        grabInteractable.selectExited.AddListener(x => HideInfo());
        //grabInteractable.activated.AddListener(x => UpdateInfo());
    
       
        amount = GetComponent<Ingredient>().ingredientSO.ingredientAmount;

        
        objectToFollow = transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (ingredientInfoPrefab != null && objectToFollow != null)
        {
            // Update the progress bar's position to follow the object
            ingredientInfoPrefab.transform.position = objectToFollow.position + Vector3.up * yOffset;
        }
    }

    public void ShowInfo()
    {
     
        if (!isSockted)
        {
            ingredientInfoPrefab.SetActive(true);
        }
       
    }

    public void HideInfo()
    {

        ingredientInfoPrefab.SetActive(false);
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


