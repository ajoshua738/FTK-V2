using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterStream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;
    private ParticleSystem splashParticle = null;

    private Coroutine pourRoutine = null;

    public AddWater addwater;


    private Vector3 targetPosition = Vector3.zero;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splashParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        MoveToPoistion(0, transform.position);
        MoveToPoistion(1, transform.position);
    }

    

    public void Begin()
    {
        StartCoroutine(UpdateParticle());
        pourRoutine = StartCoroutine(BeginPour());
    }

    private IEnumerator BeginPour()
    {
        while(gameObject.activeSelf)
        {
            targetPosition = FindEndPoint();

            MoveToPoistion(0, transform.position);
            AnimateToPosition(1, targetPosition);


            yield return null;
        }
        
        
    }

    public void End()
    {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(EndPour());
    }

    private IEnumerator EndPour()
    {
        while(!HasReachedPosition(0, targetPosition))
        {
            AnimateToPosition(0, targetPosition);
            AnimateToPosition(1, targetPosition);

            yield return null;
        }
            
        Destroy(gameObject);
    }

    private Vector3 FindEndPoint()
    {
        RaycastHit hit;

        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, 2.0f);
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);

        if(hit.collider.gameObject.tag == "Base")
        {
            addwater = hit.collider.gameObject.GetComponent<AddWater>();
            addwater.isPouring = true;
        }
        else
        {
            if(addwater != null)
            {
                addwater.isPouring = false;
            }
       
        }
       
       
    

        return endPoint;
    }

    private void MoveToPoistion(int index, Vector3 targetPosition)
    {
        lineRenderer.SetPosition(index, targetPosition);
             
    }

    private void AnimateToPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * 1.75f);
        lineRenderer.SetPosition(index, newPosition);

    }

    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPosition = lineRenderer.GetPosition(index);

        return currentPosition == targetPosition;
    }

    private IEnumerator UpdateParticle()
    {
        while (gameObject.activeSelf)
        {
            splashParticle.gameObject.transform.position = targetPosition;

            bool isHitting = HasReachedPosition(1, targetPosition);
            splashParticle.gameObject.SetActive(isHitting);

            yield return null;
        }
       
    }
}
