using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaltShaker : MonoBehaviour
{
    public AudioSource shakeSound;
    public IngredientSO salt;
    public GameObject saltPrefab;
    public Transform origin;
    public LayerMask layerMask;
    private GenerateReticle generateReticle;
    public string parentTag;
    public string ingredientTag;
    // Start is called before the first frame update
    void Start()
    {
        generateReticle = GetComponent<GenerateReticle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaltShakerEvent()
    {
        shakeSound.Play();

        // Instantiate the ketchup GameObject at the hit point of the raycast
        if (Physics.Raycast(origin.position, Vector3.down, out RaycastHit hit, 2.0f, layerMask) && generateReticle.isPouring)
        {
            if (hit.collider.gameObject.CompareTag(ingredientTag))
            {
                Transform rawBurgerPatty = hit.collider.transform;

                // Check children of the RawBurgerPatty object for the Ingredient/Salt tag
                foreach (Transform child in rawBurgerPatty)
                {
                    if (child.CompareTag(parentTag))
                    {
                        GameObject saltObject = Instantiate(saltPrefab, child.transform.position, Quaternion.identity);
                        saltObject.transform.SetParent(child.transform); // Make the saltObject a child of 'child'
                        
                       
                    }
                }
            }
          
        }
    }
}
