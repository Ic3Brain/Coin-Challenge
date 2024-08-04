using System.Collections;
using UnityEngine;

public class PortalAttract : MonoBehaviour
{
    
    public float attractionForce = 10;
    public float maxDistance = 100;
    public float actualDistance;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    float startShrinkingDist = 10f;

    [SerializeField]
    PortalAttract targetPortal;
    


    public void AttrackToPortal()
    {   
        StartCoroutine(AttrackPortalCorrout());
    }

    IEnumerator AttrackPortalCorrout()
    {
        this.rb.useGravity = false;
        float size = 1;
        do 
        {   
            actualDistance = Vector3.Distance(transform.position, rb.transform.position);
            if(actualDistance <= maxDistance)
            {   
                rb.AddForce((transform.position - rb.transform.position) * attractionForce * Time.smoothDeltaTime);
            }
            size = Mathf.InverseLerp(0, startShrinkingDist, actualDistance);
            rb.transform.localScale = new Vector3(size, size, size);
            yield return null;
        }
        while (actualDistance > 1);

        rb.transform.position = targetPortal.transform.position;
        targetPortal.StartCoroutine(targetPortal.ExpulseCorrout());
    }

    IEnumerator ExpulseCorrout()
    {   
        Debug.Log("expulse corrout");
        float t = 0;
        float size = 0;
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
        while(t < 1.1f)
        {   
            t += Time.deltaTime;
            size = Mathf.Lerp(0, 1, t);
            rb.transform.localScale = new Vector3(size, size, size);
            yield return null;
        }
    }
}
