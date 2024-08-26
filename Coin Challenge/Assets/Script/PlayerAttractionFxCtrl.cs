using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;


public class PlayerAttractionFxCtrl : MonoBehaviour
{
	public static PlayerAttractionFxCtrl instance;

	[SerializeField]
	Material _mat;
	Material _matInst;

	Renderer[] _rends;

	[SerializeField]
	GameObject _sphere, playerMesh;

	[SerializeField] ParticleSystem _particles;

	private void Awake()
	{
		instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		_matInst = new Material(_mat);
		_sphere.GetComponent<Renderer>().material = _matInst;
		GetAllRenderers();
		AddMaterialToAllRend();
		_particles.gameObject.SetActive(false);

	}

	public Coroutine FadeIn()
	{
		return StartCoroutine(FadeInCorout());
	}

	IEnumerator FadeInCorout()
	{
		float t = 0;

		while (t < 1.1f)
		{
			_matInst.SetFloat("_rate", Mathf.Lerp(0f, 1f, t));
			t += Time.deltaTime * 0.5f;
			yield return null;
		}

		_sphere.transform.position = transform.position + new Vector3(0, 0.5f, 0);
		_sphere.transform.localScale = Vector3.one;
		yield return StartCoroutine(SetSize(_sphere, new Vector3(1.2f, 4.2f, 1.2f), 4f));
		StartCoroutine(SetSize(playerMesh, Vector3.zero, 3f));
		StartCoroutine(SetSize(_sphere, new Vector3(0.3f, 0.3f, 0.3f), 3f));

		_sphere.transform.SetParent(this.transform);

		_particles.gameObject.SetActive(true);
	}

	public Coroutine FadeOut()
	{
		return StartCoroutine(FadeOutCorout());
	}
	 IEnumerator FadeOutCorout()
	{

		_particles.gameObject.SetActive(false);

		_sphere.transform.position = transform.position + new Vector3(0, 0.5f, 0);
		_sphere.transform.localScale = Vector3.one;
		yield return StartCoroutine(SetSize(_sphere, new Vector3(1.2f, 4.2f, 1.2f), 4f));
		playerMesh.transform.localScale = Vector3.one;
		StartCoroutine(SetSize(_sphere, new Vector3(0.3f, 0.3f, 0.3f), 3f));

		_sphere.transform.SetParent(this.transform);

		_particles.gameObject.SetActive(true);

		float t = 0;

		while (t < 1f)
		{
			_matInst.SetFloat("_rate", Mathf.Lerp(1f, 0f, t));
			t += Time.deltaTime * 0.5f;
			yield return null;
		}
		_sphere.SetActive(false);
		RemoveMaterialFromAllRend();
	}

	IEnumerator SetSize(GameObject _go, Vector3 _targetScale, float speed)
	{
		float t = 0;
		Vector3 _startsize = _go.transform.localScale;

		while (t < 1.1f)
		{
			_go.transform.localScale = Vector3.Lerp(_startsize, _targetScale, t);
			t += Time.deltaTime * speed;
			yield return null;
		}

	}

	// Update is called once per frame
	void Update()
	{

	}

	void GetAllRenderers()
	{
		_rends = GetComponentsInChildren<Renderer>();

	}

	void AddMaterialToAllRend()
	{
		Material[] mats = new Material[2];
		mats[1] = _matInst;

		foreach (var rend in _rends)
		{
			mats[0] = rend.material;

			rend.materials = mats;
		}
	}

	void RemoveMaterialFromAllRend()
	{
		Material[] mats = new Material[1];

		foreach (var rend in _rends)
		{
			mats[0] = rend.materials[0];
			rend.materials = mats;
		}
	}

	public void OnEnterPortalReached()
	{
		_particles.gameObject.SetActive(false);
	}
}
