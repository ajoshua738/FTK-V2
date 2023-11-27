using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckRecipe : MonoBehaviour
{

    
    public List<IngredientSO> ingredients;


   

    public TMP_Text remarksText;

    public GameObject recipeContainer;
    public GameObject performanceContainer;

    public RecipeSO originalRecipeSO;
    public RecipeSO newRecipeSO;
    public Dictionary<string, float> ingredientAmounts;
    public PerformanceTracker performanceTracker;
    // Start is called before the first frame update
    void Start()
    {
        recipeContainer.SetActive(true);
        performanceContainer.SetActive(false);
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
                IngredientSO foundIngredient = ingredients.Find(x => x.ingredientName == recipeIngredient.ingredientName);

                if (foundIngredient != null)
                {
                    // Compare the amounts
                    if (foundIngredient.ingredientAmount < recipeIngredient.ingredientAmount)
                    {
                        Debug.Log(foundIngredient.ingredientName + " amount is lower than required!");
                        remarksText.text += foundIngredient.ingredientName.ToString() + " amount is lower than required!\n\n";
                        performanceTracker.score -= 10;
                    }
                    else if (foundIngredient.ingredientAmount > recipeIngredient.ingredientAmount)
                    {
                        Debug.Log(foundIngredient.ingredientName + " amount is higher than required!");
                        remarksText.text += foundIngredient.ingredientName.ToString() + " amount is higher than required!\n\n";
                        performanceTracker.score -= 10;
                    }
                    else
                    {
                        Debug.Log(foundIngredient.ingredientName + " amount matches the required amount.");
                        remarksText.text += foundIngredient.ingredientName.ToString() + " amount matches the required amount.\n\n";
                        performanceTracker.score += 10;
                    }

                }
                else
                {
                    Debug.Log("Ingredient not found: " + recipeIngredient.ingredientName);
                    performanceTracker.score -= 20;
                }
            }
        }
        else
        {
            Debug.LogError("RecipeSO is not assigned!");
        }

        performanceTracker.checkScore();
        EnableDisplayPerformance();

    }

    public void EnableDisplayPerformance()
    {
        recipeContainer.SetActive(false);
        performanceContainer.SetActive(true);
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
            string currentIngredientName = ingredients[i].ingredientName;
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
            aggregatedIngredient.ingredientName = ingredientName;
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
                string currentIngredientName = recipeIngredient.ingredientName;
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
            aggregatedIngredient.ingredientName = ingredientName;
            aggregatedIngredient.ingredientAmount = kvp.Value;
            aggregatedIngredient.unit = ingredientUnit; // Set the unit
            newRecipeSO.ingredientsSOList.Add(aggregatedIngredient);
        }
    }


}
