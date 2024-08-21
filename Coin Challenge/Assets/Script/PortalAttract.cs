using System.Collections;
using UnityEngine;

public class PortalAttract : MonoBehaviour
{

	[SerializeField]
	Rigidbody rb;

	[SerializeField]
	PortalAttract targetPortal;

	[SerializeField] LayerMask layerMask;

	[SerializeField] PlayerAttractionFxCtrl _fxCtrl;

	public void AttrackToPortal()
	{

		StartCoroutine(AttrackPortalCorrout());
	}

	//Attire le joueur vers le portail
	/*
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
        while (actualDistance > 2);

        rb.transform.position = targetPortal.transform.position;
        targetPortal.StartCoroutine(targetPortal.ExpulseCorrout());
    }*/

	IEnumerator AttrackPortalCorrout()
	{
		_fxCtrl.transform.LookAt(transform.position);
		yield return _fxCtrl.FadeIn();
		rb.isKinematic = true;
		float _speed = 10;
		float _dist;

		do
		{
			Vector3 posTmp = Vector3.MoveTowards(_fxCtrl.transform.position, transform.position, Time.deltaTime * _speed);
			_speed += Time.deltaTime * 3f;
			/*if (Physics.CheckSphere(posTmp, 3, layerMask))
				posTmp.y += Time.deltaTime * 5f;*/
			_fxCtrl.transform.position = posTmp;
			_dist = Vector3.Distance(_fxCtrl.transform.position, transform.position);
			yield return null;
		}
		while (_dist > 0.5f);

        rb.transform.position = targetPortal.transform.position;
        targetPortal.StartCoroutine(targetPortal.ExpulseCorrout());
	}

	//Redonne la taille normal au player
	IEnumerator ExpulseCorrout()
	{
		Debug.Log("expulse corrout");
		float t = 0;
		float size = 0;
		rb.velocity = Vector3.zero;
		rb.useGravity = true;
		while (t < 1.1f)
		{
			t += Time.deltaTime;
			size = Mathf.Lerp(0, 1, t);
			rb.transform.localScale = new Vector3(size, size, size);
			yield return null;
		}
	}
}
