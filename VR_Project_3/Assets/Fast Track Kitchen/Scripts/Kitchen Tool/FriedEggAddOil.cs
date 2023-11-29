using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriedEggAddOil : MonoBehaviour
{
    public LayerMask layerMask; // Define the layers to include in the raycast

    private HashSet<GameObject> eggIngredients = new HashSet<GameObject>(); // List to store unique oil ingredients
    public List<GameObject> eggGameObjects = new List<GameObject>();
    public IngredientSO ingredientSO;
    public GameObject oilPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

 

    void Update()
    {
        // Cast a ray downward from the object's position
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, Vector3.up, out hit, Mathf.Infinity, layerMask))
        {
            //Find the ingredient i want 
            if (hit.collider.gameObject.CompareTag("Ingredient/FriedEgg"))
            {
                //If the ingredient is not in the list, add it
                if (!eggIngredients.Contains(hit.collider.gameObject))
                {
                    eggIngredients.Add(hit.collider.gameObject);
                    eggGameObjects.Add(hit.collider.gameObject);//Add in this list so i can see
                    
                    Transform ingredient = hit.collider.transform; //The mian ingredient/ parent ingredient

                    // Check children of the ingredient object for the parent tag
                    //For example, find egg -> oil to add oil
                    // find patty - > salt to add salt
                    foreach (Transform child in ingredient)
                    {
                        if (child.CompareTag("Ingredient/Oil"))
                        {
                            GameObject oilObject = Instantiate(oilPrefab, child.transform.position, Quaternion.identity);
                            oilObject.transform.SetParent(child.transform); // Make the saltObject a child of 'child'


                        }
                    }
                 
             
                    Debug.Log("Added: " + hit.collider.gameObject.name + " to oilIngredients list");
                }
            }
        }



    }

  
    
}
