using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRecipe : MonoBehaviour
{

    
    public List<IngredientSO> ingredients;
    public RecipeSO recipeSO;
    public Dictionary<string, float> ingredientAmounts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CheckFinalRecipe()
    {
        if (recipeSO != null)
        {
            List<IngredientSO> recipeIngredients = recipeSO.ingredientsSOList;

            foreach (var recipeIngredient in recipeIngredients)
            {
                // Find the corresponding ingredient in the ingredients list
                IngredientSO foundIngredient = ingredients.Find(x => x.indgredientName == recipeIngredient.indgredientName);

                if (foundIngredient != null)
                {
                    // Compare the amounts
                    if (foundIngredient.ingredientAmount < recipeIngredient.ingredientAmount)
                    {
                        Debug.Log(foundIngredient.indgredientName + " amount is lower than required!");
                    }
                    else if (foundIngredient.ingredientAmount > recipeIngredient.ingredientAmount)
                    {
                        Debug.Log(foundIngredient.indgredientName + " amount is higher than required!");
                    }
                    else
                    {
                        Debug.Log(foundIngredient.indgredientName + " amount matches the required amount.");
                    }
                }
                else
                {
                    Debug.Log("Ingredient not found: " + recipeIngredient.indgredientName);
                }
            }
        }
        else
        {
            Debug.LogError("RecipeSO is not assigned!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if(other.gameObject.layer == LayerMask.NameToLayer("Ingredient"))
        {
            Ingredient ingredient = other.GetComponent<Ingredient>();
            IngredientSO ingredientSO = ingredient.GetIngredientSO();
            ingredients.Add(ingredientSO);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ingredient"))
        {
            Ingredient ingredient = other.GetComponent<Ingredient>();
            IngredientSO ingredientSO = ingredient.GetIngredientSO();
            ingredients.Remove(ingredientSO);
        }
    }

    // Function to aggregate the amounts of the same ingredients
    public void AggregateIngredients()
    {
        ingredientAmounts = new Dictionary<string, float>();

        // Loop through the ingredients list
        for (int i = 0; i < ingredients.Count; i++)
        {
            string currentIngredientName = ingredients[i].indgredientName;
            float currentIngredientAmount = ingredients[i].ingredientAmount;

            // Check if the ingredient name exists in the dictionary
            if (ingredientAmounts.ContainsKey(currentIngredientName))
            {
                // Add the amount to the existing ingredient in the dictionary
                ingredientAmounts[currentIngredientName] += currentIngredientAmount;
            }
            else
            {
                // Add the ingredient to the dictionary
                ingredientAmounts.Add(currentIngredientName, currentIngredientAmount);
            }
        }

        // Clear the original ingredients list
        ingredients.Clear();

        // Rebuild the ingredients list with aggregated amounts
        foreach (var kvp in ingredientAmounts)
        {
            // Create a new instance of IngredientSO using CreateInstance
            IngredientSO aggregatedIngredient = ScriptableObject.CreateInstance<IngredientSO>();
            aggregatedIngredient.indgredientName = kvp.Key;
            aggregatedIngredient.ingredientAmount = kvp.Value;
            ingredients.Add(aggregatedIngredient);
        }
        CheckFinalRecipe();
    }
}
