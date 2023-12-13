using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOil : MonoBehaviour
{
    public LayerMask layerMask; // Define the layers to include in the raycast

    private HashSet<GameObject> ingredients = new HashSet<GameObject>(); // List to store unique oil ingredients
    public List<GameObject> gameObjects = new List<GameObject>();
    public IngredientSO ingredientSO;
    public GameObject oilPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

 

    void Update()
    {
        
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, Vector3.up, out hit, Mathf.Infinity, layerMask))
        {
            //Find the ingredient i want 
            if (hit.collider.gameObject.tag.Contains("Ingredient"))
            {
                //If the ingredient is not in the list, add it
                if (!ingredients.Contains(hit.collider.gameObject))
                {
                    ingredients.Add(hit.collider.gameObject);
                    gameObjects.Add(hit.collider.gameObject);//Add in this list so i can see
                    
                    Transform ingredient = hit.collider.transform; //The mian ingredient/ parent ingredient

                    // Check children of the ingredient object for the parent tag
                    //For example, find egg -> oil to add oil
                    // find patty - > salt to add salt
                    foreach (Transform child in ingredient)
                    {
                        if (child.CompareTag("Ingredient/Oil"))
                        {
                            GameObject oilObject = Instantiate(oilPrefab, child.transform.position, Quaternion.identity);
                            oilObject.transform.SetParent(child.transform);


                        }
                    }
                 
             
                    
                }
            }
        }



    }

  
    
}
