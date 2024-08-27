using Cinemachine;
using System.Collections;
using UnityEngine;

public class PortalAttract : MonoBehaviour
{

	public float attractionForce = 10;
	public float maxDistance = 100;
	public float actualDistance;
	public bool _readDebugInput;

	[SerializeField]
	Rigidbody rb;

	[SerializeField]
	PortalAttract targetPortal;


	[SerializeField] LayerMask layerMask;

	PlayerAttractionFxCtrl _fxCtrl;

	CinemachineBrain brain;
	private void Start()
	{
		_fxCtrl = PlayerAttractionFxCtrl.instance;
		//Get the Live vCam
		brain = CinemachineCore.Instance.GetActiveBrain(0);
	}

	private void Update()
	{
		if (_readDebugInput && Input.GetKeyDown(KeyCode.Space)) AttrackToPortal();
	}

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
			_fxCtrl.transform.position = posTmp;
			_dist = Vector3.Distance(_fxCtrl.transform.position, transform.position);
			yield return null;
		}
		while (_dist > 0.5f);



		targetPortal.StartCoroutine(targetPortal.ExpulseCorrout());
	}

	//Redonne la taille normal au player
	IEnumerator ExpulseCorrout()
	{Debug.Log("expulse corout");
		_fxCtrl.OnEnterPortalReached();
		rb.transform.position = transform.position;

		GameObject _activeCam = brain.ActiveVirtualCamera.VirtualCameraGameObject;
		_activeCam.SetActive(false);
		yield return null;
		_activeCam.SetActive(true);

		float t = 0;
		float size;

		while (t < 1.1f)
		{
			t += Time.deltaTime;
			size = Mathf.Lerp(0, 1, t);
			rb.transform.localScale = new Vector3(size, size, size);
			yield return null;
		}

		yield return _fxCtrl.FadeOut();

		rb.velocity = Vector3.zero;
		rb.useGravity = true;
		rb.isKinematic = false;
	}
}
