using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDisplayCurrentIngredients : MonoBehaviour
{
    public List<IngredientSO> ingredientsOnPlate = new List<IngredientSO>();



    public GameObject UIContainer; 
    public List<GameObject> UIObjects = new List<GameObject>();
    public Transform startingPoint; // Reference to the starting point of first text prefab
    public GameObject textPrefab; // Reference to your TextMeshProUGUI Prefab
    float textSpacing = 0.8f; // Adjust the spacing between UI text elements
    float UISpacing = 0.1f; // Adjust the value to adjust the entire container
    float containerYPos;


    public float yOffset = 0.1f;
    public Transform objectToFollow;

    private void Start()
    {
        objectToFollow = transform;
        containerYPos = UIContainer.transform.localPosition.y;
     
        UpdateUI();
    }

    private void Update()
    {
        if (UIContainer != null && objectToFollow != null)
        {
            // Update the progress bar's position to follow the object
            UIContainer.transform.position = objectToFollow.position + Vector3.up * yOffset;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();

        if (ingredient != null && other.gameObject.layer == LayerMask.NameToLayer("Ingredient"))
        {
            IngredientSO currentIngredientSO = ingredient.GetIngredientSO();

            // Check if the ingredient is already on the plate
            IngredientSO existingIngredient = ingredientsOnPlate.Find(x => x.ingredientName == currentIngredientSO.ingredientName);

            if (existingIngredient != null)
            {
                // Update the amount of the existing ingredient
                existingIngredient.ingredientAmount += currentIngredientSO.ingredientAmount;
            }
            else
            {
                // Create a new instance of the IngredientSO and add it to the list
                IngredientSO newIngredient = ScriptableObject.CreateInstance<IngredientSO>();
                newIngredient.ingredientName = currentIngredientSO.ingredientName;
                newIngredient.ingredientAmount = currentIngredientSO.ingredientAmount;
                newIngredient.unit = currentIngredientSO.unit;

                ingredientsOnPlate.Add(newIngredient);
            }


            UpdateUI();

        
        }
    }



    private void OnTriggerExit(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();

        if (ingredient != null && other.gameObject.layer == LayerMask.NameToLayer("Ingredient"))
        {
            IngredientSO currentIngredientSO = ingredient.GetIngredientSO();

            // Check if the ingredient is on the plate
            IngredientSO existingIngredient = ingredientsOnPlate.Find(x => x.ingredientName == currentIngredientSO.ingredientName);

            if (existingIngredient != null)
            {
                // Reduce the amount or remove completely if the amount is zero
                existingIngredient.ingredientAmount -= currentIngredientSO.ingredientAmount;

                if (existingIngredient.ingredientAmount <= 0f)
                {
                    ingredientsOnPlate.Remove(existingIngredient);
                }
            }
            UpdateUI();
        }
    }


    public void UpdateUI()
    {

        foreach (GameObject go in UIObjects)
        {
            Destroy(go);
        }
        UIObjects.Clear();
        int index = 0;
        foreach (var ing in ingredientsOnPlate)
        {

            GameObject newText = Instantiate(textPrefab, Vector3.zero, Quaternion.identity, startingPoint);
            newText.transform.localRotation = Quaternion.identity; // Set rotation to zero
            UIObjects.Add(newText);
            TextMeshProUGUI nameText = newText.transform.Find("Name Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI amountText = newText.transform.Find("Amount Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI unitText = newText.transform.Find("Unit Text").GetComponent<TextMeshProUGUI>();

            nameText.text = ing.ingredientName;
            amountText.text = ing.ingredientAmount.ToString();
            unitText.text = ing.unit;

            newText.transform.localPosition = new Vector3(0f, -index * textSpacing, 0f);
            UIContainer.transform.localPosition = new Vector3(0f, containerYPos + (index * UISpacing), 0f);
            index++;

        }
        if(UIObjects.Count <= 0)
        {
            UIContainer.SetActive(false);
        }
        else
        {
            UIContainer.SetActive(true);
        }
    }

    public void ChangeIngredientName(string oldName, string newName)
    {
        IngredientSO ingredientToChange = ingredientsOnPlate.Find(x => x.ingredientName == oldName);

        if (ingredientToChange != null)
        {
            ingredientToChange.ingredientName = newName;
            UpdateUI(); // Update the UI after changing the name
        }
        else
        {
            Debug.LogWarning("Ingredient '" + oldName + "' not found.");
        }
    }

}
