using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIDisplayCurrentIngredients : MonoBehaviour
{


    public List<IngredientSO> ingredients;
   
    public Dictionary<string, float> ingredientAmounts;


    public Transform startingPoint; // Reference to the starting point of first text prefab
    public GameObject textPrefab; // Reference to your TextMeshProUGUI Prefab
    public float textSpacing = 0.6f; // Adjust the spacing between UI text elements
    public float UISpacing = 0.1f; // Adjust the value to adjust the entire container


    public float maxDistance = 5f; // Maximum distance to show the UI
    public float maxViewAngle = 60f; // Maximum view angle to show the UI
 
    public GameObject UIContainer; // The kitchen tool that holds the UI
    public Transform playerTransform;
    public List<GameObject> UIObjects;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);



        if (ingredients.Count <= 0)
        {
            UIContainer.SetActive(false);
        }
        else
        {
            if (distanceToPlayer <= maxDistance)
            {
                // Check if the plate is within the view angle
                Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

                if (angleToPlayer <= maxViewAngle)
                {
                    UIContainer.SetActive(true);
                    return;
                }

            }
            else
            {
                UIContainer.SetActive(false);
            }
        }

    }



    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Ingredient"))
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
            // Create a unique key for each ingredient including the unit
            string ingredientKey = ingredientSO.indgredientName;

            // Check for the existence of the ingredient in the list based on the key
            IngredientSO ingredientToRemove = ingredients.Find(x => (x.indgredientName) == ingredientKey);

            // Remove the ingredient from the list
            if (ingredientToRemove != null)
            {
                ingredients.Remove(ingredientToRemove);
            }
          
            
        }
    }
    
    public void updateUI()
    {
        foreach(GameObject go in UIObjects)
        {
            Destroy(go);
        }
        UIObjects.Clear();
        int index = 0;
        foreach (var ing in ingredients)
        {
         
            GameObject newText = Instantiate(textPrefab, Vector3.zero, Quaternion.identity, startingPoint);
            newText.transform.localRotation = Quaternion.identity; // Set rotation to zero
            UIObjects.Add(newText);
            TextMeshProUGUI nameText = newText.transform.Find("Name Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI amountText = newText.transform.Find("Amount Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI unitText = newText.transform.Find("Unit Text").GetComponent<TextMeshProUGUI>();

            nameText.text = ing.indgredientName;
            amountText.text = ing.ingredientAmount.ToString();
            unitText.text = ing.unit;

            newText.transform.localPosition = new Vector3(0f, -index * textSpacing, 0f);
            UIContainer.transform.localPosition = new Vector3(0f, index * UISpacing, 0f);
            index++;
            
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

        updateUI();
      
    }
}
