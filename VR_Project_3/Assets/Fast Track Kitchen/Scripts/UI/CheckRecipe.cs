using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRecipe : MonoBehaviour
{

    
    public List<IngredientSO> ingredients;

  
  
  
  
    public RecipeSO originalRecipeSO;
    public RecipeSO newRecipeSO;
    public Dictionary<string, float> ingredientAmounts;
    public PerformanceTracker performanceTracker;
    // Start is called before the first frame update
    void Start()
    {
        newRecipeSO = Instantiate(originalRecipeSO);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CheckFinalRecipe()
    {
        if (newRecipeSO != null)
        {
            List<IngredientSO> recipeIngredients = newRecipeSO.ingredientsSOList;

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
                        performanceTracker.score -= 10;
                    }
                    else if (foundIngredient.ingredientAmount > recipeIngredient.ingredientAmount)
                    {
                        Debug.Log(foundIngredient.indgredientName + " amount is higher than required!");
                        performanceTracker.score -= 10;
                    }
                    else
                    {
                        Debug.Log(foundIngredient.indgredientName + " amount matches the required amount.");
                        performanceTracker.score += 10;
                    }
                }
                else
                {
                    Debug.Log("Ingredient not found: " + recipeIngredient.indgredientName);
                    performanceTracker.score -= 20;
                }
            }
        }
        else
        {
            Debug.LogError("RecipeSO is not assigned!");
        }

        performanceTracker.checkScore();

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
        // Loop through the ingredients list
        for (int i = 0; i < ingredients.Count; i++)
        {
            string currentIngredientName = ingredients[i].indgredientName;
            float currentIngredientAmount = ingredients[i].ingredientAmount;
            string currentIngredientUnit = ingredients[i].unit; // Retrieve the unit

            // Create a unique key for each ingredient including the unit
            string ingredientKey = currentIngredientName + "_" + currentIngredientUnit;

            // Check if the ingredient name exists in the dictionary
            if (ingredientAmounts.ContainsKey(ingredientKey))
            {
                // Add the amount to the existing ingredient in the dictionary
                ingredientAmounts[ingredientKey] += currentIngredientAmount;
            }
            else
            {
                // Add the ingredient to the dictionary
                ingredientAmounts.Add(ingredientKey, currentIngredientAmount);
            }
        }

        // Clear the original ingredients list
        ingredients.Clear();

        // Rebuild the ingredients list with aggregated amounts
        foreach (var kvp in ingredientAmounts)
        {
            string[] keyParts = kvp.Key.Split('_');
            string ingredientName = keyParts[0];
            string ingredientUnit = keyParts[1];

            // Create a new instance of IngredientSO using CreateInstance
            IngredientSO aggregatedIngredient = ScriptableObject.CreateInstance<IngredientSO>();
            aggregatedIngredient.name = ingredientName;
            aggregatedIngredient.indgredientName = ingredientName;
            aggregatedIngredient.ingredientAmount = kvp.Value;
            aggregatedIngredient.unit = ingredientUnit; // Set the unit

            ingredients.Add(aggregatedIngredient);
        }
        AggregateRecipeIngredients();
        CheckFinalRecipe();
    }


    public void AggregateRecipeIngredients()
    {
        ingredientAmounts = new Dictionary<string, float>();

        if (newRecipeSO != null)
        {
            List<IngredientSO> recipeIngredients = newRecipeSO.ingredientsSOList;

            // Loop through the ingredients in the recipeSO
            foreach (var recipeIngredient in recipeIngredients)
            {
                string currentIngredientName = recipeIngredient.indgredientName;
                float currentIngredientAmount = recipeIngredient.ingredientAmount;
                string currentIngredientUnit = recipeIngredient.unit; // Retrieve the unit

                // Create a unique key for each ingredient including the unit
                string ingredientKey = currentIngredientName + "_" + currentIngredientUnit;

                // Check if the ingredient name exists in the dictionary
                if (ingredientAmounts.ContainsKey(ingredientKey))
                {
                    // Add the amount to the existing ingredient in the dictionary
                    ingredientAmounts[ingredientKey] += currentIngredientAmount;
                }
                else
                {
                    // Add the ingredient to the dictionary
                    ingredientAmounts.Add(ingredientKey, currentIngredientAmount);
                }
            }
        }
        else
        {
            Debug.LogError("RecipeSO is not assigned!");
        }

        newRecipeSO.ingredientsSOList.Clear();
       
        foreach (var kvp in ingredientAmounts)
        {
      
            string[] keyParts = kvp.Key.Split('_');
            string ingredientName = keyParts[0];
            string ingredientUnit = keyParts[1];

            // Create a new instance of IngredientSO using CreateInstance
            IngredientSO aggregatedIngredient = ScriptableObject.CreateInstance<IngredientSO>();
            aggregatedIngredient.name = ingredientName;
            aggregatedIngredient.indgredientName = ingredientName;
            aggregatedIngredient.ingredientAmount = kvp.Value;
            aggregatedIngredient.unit = ingredientUnit; // Set the unit
            newRecipeSO.ingredientsSOList.Add(aggregatedIngredient);
        }
    }


}
