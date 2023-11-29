using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour
{
    public AudioSource pourSound;
    public IngredientSO oil;
    public GameObject oilPrefab;
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


    public void OilEvent()
    {
        pourSound.Play();

        // Instantiate the ketchup GameObject at the hit point of the raycast
        if (Physics.Raycast(origin.position, Vector3.down, out RaycastHit hit, 2.0f, layerMask) && generateReticle.isPouring)
        {
            if (hit.collider.gameObject.CompareTag(ingredientTag))
            {
                Transform parentObj = hit.collider.transform;

                // Check children of the RawBurgerPatty object for the Ingredient/Salt tag
                foreach (Transform child in parentObj)
                {
                    if (child.CompareTag(parentTag))
                    {
                        GameObject oilObject = Instantiate(oilPrefab, child.transform.position, Quaternion.Euler(-180,-90,0));
                        oilObject.transform.SetParent(child.transform); // Make the saltObject a child of 'child'


                    }
                }
            }

        }
    }
}
