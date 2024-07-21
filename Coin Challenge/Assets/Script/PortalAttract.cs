using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalAttract : MonoBehaviour
{
    public GameObject portal;
    public float attractionForce = 10;
    public float maxDistance = 100;
    public float actualDistance;
    Rigidbody rb;
    


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void AttrackToPortal()
    {   
        this.rb.useGravity = false;
        actualDistance = Vector3.Distance(portal.transform.position, transform.position);
        if(actualDistance <= maxDistance)
        {
            rb.AddForce((portal.transform.position - transform.position) * attractionForce * Time.smoothDeltaTime);
        }
    }
}
