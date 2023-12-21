using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{

    public AudioSource eggSound;
    
    public GameObject eggPrefab;
    public Transform origin;
    public LayerMask layerMask;
    private GenerateReticle generateReticle;
    public GameObject eggObject;
    // Start is called before the first frame update
    void Start()
    {
        generateReticle = GetComponent<GenerateReticle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EggEvent()
    {
        eggSound.Play();

        // Instantiate the ketchup GameObject at the hit point of the raycast
        if (Physics.Raycast(origin.position, Vector3.down, out RaycastHit hit, 2.0f, layerMask) && generateReticle.isPouring)
        {
            Instantiate(eggPrefab, hit.point, Quaternion.identity);
            gameObject.SetActive(false);
            generateReticle.reticleInstance.SetActive(false);
            generateReticle.enabled = false;
            
            StartCoroutine(DestroyEgg());
        }

        
    }

    IEnumerator DestroyEgg()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
