using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public enum AlertStage
{
	Peaceful,
	Alerted
}

public class PassiveMobAI : MonoBehaviour
{
	public NavMeshAgent agent;
	public Transform player;
	public LayerMask whatIsGround, whatIsPlayer;
	public Vector3 walkpoint;
	bool walkPointSet;
	public float walkPointRange;

	[SerializeField]
	Animator animator;

	[SerializeField] Transform anchor, anchor1;

	public float fov;
	public AlertStage alertStage;
	[Range(0, 1)] public float alertLevel; //Peaceful 0, Alerted 1
	[Range(0, 360)] public float fovAngle;

	private float currentSpeed
	{
		get
		{
			bool playerInFOV = PlayerInRange();

			if (playerInFOV)
			{
				playerLastTimeInRange = Time.timeSinceLevelLoad;
			}
			return elapseTimeSinceLastDetection > 7 ? 1.5f : 12f;
		}
	}

	float playerLastTimeInRange = -10000;
	float elapseTimeSinceLastDetection
	{
		get
		{
			return Time.timeSinceLevelLoad - playerLastTimeInRange;
		}
	}
	Vector2 quadranSize;
	Vector2 areaSize;

	private void Awake()
	{
		player = GameObject.Find("Player").transform;
		agent = GetComponent<NavMeshAgent>();
		alertStage = AlertStage.Peaceful;
		alertLevel = 0;

		InitQuadrans();

	}

	private void Start()
	{
		StartCoroutine(Behaviour());
	}

	Dictionary<Vector2Int, Quadran> _quadranDico;
	public List<Quadran> _quadrans = new List<Quadran>();

	void InitQuadrans()
	{
		_quadranDico = new Dictionary<Vector2Int, Quadran>();

		areaSize = new Vector2(anchor1.position.x - anchor.position.x, anchor1.position.z - anchor.position.z);
		quadranSize = new Vector2(areaSize.x / 4, areaSize.y / 4);

		for (int x = 0; x < 4; x++)
		{
			for (int z = 0; z < 4; z++)
			{
				Quadran quadran = new Quadran(new Vector2Int(x, z), quadranSize, anchor.position + new Vector3(quadranSize.x * x, 0, quadranSize.y * z));
				_quadrans.Add(quadran);
				_quadranDico.Add(quadran._coords, quadran);
			}
		}
	}


	IEnumerator Graze()
	{
		float timeToWait = Random.Range(3f, 10f);
		timeToWait = 0;
		float t = 0;

		while (t < timeToWait)
		{
			if (PlayerInRange()) yield break;
			t += Time.deltaTime;
			yield return null;
		}
	}

	IEnumerator Behaviour()
	{
		while (true)
		{
			yield return null;
			if (!walkPointSet) SearchWalkPoint();
			yield return StartCoroutine(GoToPosition(walkpoint));
			yield return StartCoroutine(Graze());
		}
	}

	private void SearchWalkPoint()
	{
		Vector2Int quadranOffset = Vector2Int.zero;

		if (PlayerInRange())
			quadranOffset = new Vector2Int(2, 4);
		else quadranOffset = new Vector2Int(0, 1);

		walkpoint = GetRandomQuadran(transform.position, quadranOffset).GetRandomPos();
		walkPointSet = true;
		return;
	}

	Quadran GetRandomQuadran(Vector3 position, Vector2Int quadranOffsetRange)
	{
		Quadran _currentQuadran = GetQuadranByPos(position);
		Vector2Int offset = Vector2Int.zero;

		offset.x = Random.Range(quadranOffsetRange.x, quadranOffsetRange.y);
		offset.x *= Random.Range(0, 2) == 1 ? 1 : -1;

		offset.y = Random.Range(quadranOffsetRange.x, quadranOffsetRange.y);
		offset.y *= Random.Range(0, 2) == 1 ? 1 : -1;

		CheckEdges(_currentQuadran._coords, offset, quadranOffsetRange);

		int newXCoord = Mathf.Clamp(_currentQuadran._coords.x + offset.x, 0, 3);
		int newYCoord = Mathf.Clamp(_currentQuadran._coords.y + offset.y, 0, 3);

		Quadran _quadran;
		_quadranDico.TryGetValue(new Vector2Int(newXCoord, newYCoord), out _quadran);
		return _quadran;
	}

	Vector2Int CheckEdges(Vector2Int _quadranCoord, Vector2Int offset, Vector2Int quadranOffsetRange)
	{
		if (quadranOffsetRange.x > 0)
		{
			if (_quadranCoord.x + offset.x <= 0) offset.x = Mathf.Abs(offset.x);
			else if (_quadranCoord.x + offset.x > 3) offset.x = -offset.x;

			if (_quadranCoord.y + offset.y <= 0) offset.y = Mathf.Abs(offset.y);
			else if (_quadranCoord.y + offset.y > 3) offset.y = -offset.y;
		}

		return offset;
	}

	Quadran GetQuadranByPos(Vector3 position)
	{
		position -= anchor.position;
		position.x = Mathf.Clamp(position.x, 0, areaSize.x);
		position.z = Mathf.Clamp(position.z, 0, areaSize.y);
		Vector2Int _coord = new Vector2Int((int)(position.x / quadranSize.x), (int)(position.z / quadranSize.y));

		Quadran _quadran;
		_quadranDico.TryGetValue(_coord, out _quadran);
		return _quadran;
	}

	IEnumerator GoToPosition(Vector3 position)
	{
		bool _isFleeing = false;

		agent.SetDestination(walkpoint);

		while (agent.pathPending)
			yield return null;

		if (agent.pathStatus != NavMeshPathStatus.PathComplete)
		{
			walkPointSet = false;
			yield break;
		}

		Vector3 distanceToWalkPoint;
		//animator.SetFloat("ForwardMove", 0.5f);

		do
		{
			if (PlayerInRange() && !_isFleeing)
			{
				SearchWalkPoint();
				_isFleeing = true;
				agent.SetDestination(walkpoint);
			}

			agent.speed = currentSpeed;

			distanceToWalkPoint = transform.position - walkpoint;
			yield return null;

		}
		while (distanceToWalkPoint.magnitude > 1f);

		walkPointSet = false;
		//animator.SetFloat("ForwardMove", 0f);
	}

	public bool PlayerInRange()
	{
		bool playerInFOV = false;
		Collider[] targetsInFOV = Physics.OverlapSphere(transform.position, fov, whatIsPlayer);

		foreach (Collider c in targetsInFOV)
		{
			if (c.CompareTag("Player"))
			{
				float signedAngle = Vector3.Angle(transform.forward, c.transform.position - transform.position);

				if (Mathf.Abs(signedAngle) < fovAngle / 2)
					playerInFOV = true;
				break;
			}
		}
		return playerInFOV;
	}

	[System.Serializable]
	public class Quadran
	{
		public Quadran(Vector2Int _coords, Vector2 size, Vector3 minAnchor)
		{
			this.minAnchor = minAnchor;
			_size = size;
			this._coords = _coords;

			//GameObject debugGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
			//debugGo.transform.position = minAnchor;
		}
		public Vector3 minAnchor;

		public Vector2Int _coords;

		public Vector2 _size;

		public Vector3 GetRandomPos()
		{
			float randomZ = Random.Range(0, _size.x);
			float randomX = Random.Range(0, _size.y);

			return this.minAnchor + new Vector3(randomX, 0, randomZ);
		}
	}
}
