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


    public List<GameObject> enteredIngredients = new List<GameObject>(); // List to store entered ingredients


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
            if (!enteredIngredients.Contains(other.gameObject))
            {

                GameObject obj = other.gameObject;
                enteredIngredients.Add(obj);
                print(other.gameObject.name + " Entered the plate");
                Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
                IngredientSO ingredientSO = ingredient.GetIngredientSO();

                string ingredientName = ingredientSO.ingredientName;
                float amount = ingredientSO.ingredientAmount;
                string unit = ingredientSO.unit;

                if (displayedIngredients.ContainsKey(ingredientName))
                {
                    UpdateIngredientAmount(ingredientName, amount);
                }
                else
                {
                    DisplayNewIngredient(ingredientName, amount, unit);
                }
            }

          
            
        }
    }

    private void DisplayNewIngredient(string ingredientName, float amount, string unit)
    {

        Debug.Log("Entered display new ingredient", this);
        GameObject newText = Instantiate(textPrefab, Vector3.zero, Quaternion.identity, startingPoint);
        newText.transform.localRotation = Quaternion.identity; // Set rotation to zero

        TextMeshProUGUI nameText = newText.transform.Find("Name Text").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI amountText = newText.transform.Find("Amount Text").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI unitText = newText.transform.Find("Unit Text").GetComponent<TextMeshProUGUI>();

        nameText.text = ingredientName;
        amountText.text = amount.ToString();
        unitText.text = unit;
        displayedIngredients.Add(ingredientName, newText);
     
      
       
        RepositionUI();
    }

    private void UpdateIngredientAmount(string ingredientName, float amount)
    {
       
        GameObject textToUpdate = displayedIngredients[ingredientName];
        TextMeshProUGUI amountText = textToUpdate.transform.Find("Amount Text").GetComponent<TextMeshProUGUI>();
        string currentText = amountText.text;

        float currentAmount = float.Parse(currentText);
        float updatedAmount = currentAmount + amount;

        amountText.text = updatedAmount.ToString();
        RepositionUI();

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
            enteredIngredients.Remove(other.gameObject);

            Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
            IngredientSO ingredientSO = ingredient.GetIngredientSO();

            string ingredientName = ingredientSO.ingredientName;

            if (displayedIngredients.ContainsKey(ingredientName))
            {
                Destroy(displayedIngredients[ingredientName]);
                displayedIngredients.Remove(ingredientName);
                RepositionUI();
            }
        }
    }
}
