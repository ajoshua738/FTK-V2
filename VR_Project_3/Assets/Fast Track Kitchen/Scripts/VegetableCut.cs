using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableCut : MonoBehaviour
{
    public int ingredientAmount = 0;
    public int cut = 0;
    public int maxAmount = 0;
    public GameObject ingredientPrefab;
    public AudioSource cutSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Knife"))
        {
            cutSound.Play();
            cut++;
            if (cut >= maxAmount)
            {
                gameObject.SetActive(false);
                for (int i = 0; i < ingredientAmount; i++)
                {
                    Instantiate(ingredientPrefab, gameObject.transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }

        }
    }


}
