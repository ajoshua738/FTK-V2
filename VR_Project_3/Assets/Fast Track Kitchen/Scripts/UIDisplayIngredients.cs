using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIDisplayIngredient : MonoBehaviour
{
    public Transform startingPoint; // Reference to the starting point of first text prefab
    public GameObject textPrefab; // Reference to your TextMeshProUGUI Prefab
    public float textSpacing = 0.6f; // Adjust the spacing between UI text elements
    public float UISpacing = 0.1f; // Adjust the value to adjust the entire container

    private Dictionary<string, GameObject> displayedIngredients = new Dictionary<string, GameObject>();


    public float maxDistance = 5f; // Maximum distance to show the UI
    public float maxViewAngle = 60f; // Maximum view angle to show the UI
    public Transform playerTransform;
    public GameObject UIContainer; // The kitchen tool that holds the UI

   




    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

      

        if(displayedIngredients.Count <= 0)
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Ingredient"))
        {
            print(other.gameObject.name +" Entered the plate");
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
        GameObject newText = Instantiate(textPrefab, Vector3.zero, Quaternion.identity, startingPoint);
        newText.transform.localRotation = Quaternion.identity; // Set rotation to zero
        newText.GetComponentInChildren<TextMeshProUGUI>().text = ingredientName + ": " + amount;
        displayedIngredients.Add(ingredientName, newText);

        RepositionUI();
    }

    private void UpdateIngredientAmount(string ingredientName, float amount)
    {
        if (displayedIngredients.ContainsKey(ingredientName))
        {
            GameObject textToUpdate = displayedIngredients[ingredientName];
            TextMeshProUGUI textMeshPro = textToUpdate.GetComponentInChildren<TextMeshProUGUI>();

            // Get the current text and extract the amount value
            string currentText = textMeshPro.text;
            string[] splitText = currentText.Split(':');
            float currentAmount = float.Parse(splitText[1]);

            // Add the new amount to the existing amount
            float updatedAmount = currentAmount + amount;

            // Update the text with the new amount
            textMeshPro.text = ingredientName + ": " + updatedAmount;

            RepositionUI();
        }
    }

    private void RepositionUI()
    {
        int index = 0;
        
        foreach (var item in displayedIngredients.Values)
        {
            item.transform.localPosition = new Vector3(0f, -index * textSpacing, 0f);
            UIContainer.transform.localPosition = new Vector3(0f, index * UISpacing, 0f);
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
