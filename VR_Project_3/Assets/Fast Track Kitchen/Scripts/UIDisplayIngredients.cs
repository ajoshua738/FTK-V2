using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIDisplayIngredient : MonoBehaviour
{
    public Transform displayArea; // Reference to the area where the UI will be displayed
    public GameObject textPrefab; // Reference to your TextMeshProUGUI Prefab
    public float spacing = 0.6f; // Adjust the spacing between UI elements

    private Dictionary<string, GameObject> displayedIngredients = new Dictionary<string, GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ingredient"))
        {
            Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
            IngredientSO ingredientSO = ingredient.GetIngredientSO();

            string ingredientName = ingredientSO.indgredientName;
            float amount = ingredientSO.ingredientAmount;

            if (displayedIngredients.ContainsKey(ingredientName))
            {
                UpdateIngredientAmount(ingredientName, amount);
            }
            else
            {
                DisplayNewIngredient(ingredientName, amount);
            }
        }
    }

    private void DisplayNewIngredient(string ingredientName, float amount)
    {
        GameObject newText = Instantiate(textPrefab, Vector3.zero, Quaternion.identity, displayArea);
        newText.transform.localRotation = Quaternion.identity; // Set rotation to zero
        newText.GetComponent<TextMeshProUGUI>().text = ingredientName + ": " + amount;
        displayedIngredients.Add(ingredientName, newText);

        RepositionUI();
    }

    private void UpdateIngredientAmount(string ingredientName, float amount)
    {
        GameObject textToUpdate = displayedIngredients[ingredientName];
        textToUpdate.GetComponent<TextMeshProUGUI>().text = ingredientName + ": " + amount;

        RepositionUI();
    }

    private void RepositionUI()
    {
        int index = 0;
        foreach (var item in displayedIngredients.Values)
        {
            item.transform.localPosition = new Vector3(0f, -index * spacing, 0f);
            index++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ingredient"))
        {
            Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
            IngredientSO ingredientSO = ingredient.GetIngredientSO();

            string ingredientName = ingredientSO.indgredientName;

            if (displayedIngredients.ContainsKey(ingredientName))
            {
                Destroy(displayedIngredients[ingredientName]);
                displayedIngredients.Remove(ingredientName);
                RepositionUI();
            }
        }
    }
}
