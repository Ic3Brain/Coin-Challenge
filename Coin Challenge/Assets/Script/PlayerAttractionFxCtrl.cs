using System.Collections;
using UnityEngine;



public class PlayerAttractionFxCtrl : MonoBehaviour
{
	[SerializeField]
	Material _mat;
	Material _matInst;

	Renderer[] _rends;

	[SerializeField]
	GameObject _sphere, playerMesh;

	[SerializeField] ParticleSystem _particles;



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
			t += Time.deltaTime;
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
    	float t = 0;

    while (t < 1.1f)
    {
        _matInst.SetFloat("_rate", Mathf.Lerp(1f, 0f, t));
        t += Time.deltaTime;
        yield return null;
    }

    	_sphere.transform.localScale = Vector3.one;
    	_sphere.transform.localPosition = Vector3.zero;
    	yield return StartCoroutine(SetSize(playerMesh, Vector3.one, 3f));
    	_particles.gameObject.SetActive(false);
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
}
