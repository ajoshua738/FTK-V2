using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class SliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public Material crossSectionMaterial;
    //public float cutForce = 2000;
    public LayerMask sliceableLayer;
    public VelocityEstimator velocityEstimator;
   
 
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);

        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            if (target.tag.Contains("Ingredient"))
            {
                Slice(target);
            }
          
        }
        

        
    }

    public void Slice(GameObject target)
    {

        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target,crossSectionMaterial);
            SetupSliceComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target,crossSectionMaterial);
            SetupSliceComponent(lowerHull);

            Destroy(target);
        }
    }

    public void SetupSliceComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        slicedObject.layer = LayerMask.NameToLayer("Sliceable");
        //rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);

    }
}
