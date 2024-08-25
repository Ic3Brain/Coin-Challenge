using System.Collections;
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
	
	public float walkPointRange;

	[SerializeField]
	Animator animator;

	

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
	

	private void Awake()
	{
		player = GameObject.Find("Player").transform;
		agent = GetComponent<NavMeshAgent>();
		alertStage = AlertStage.Peaceful;
		alertLevel = 0;
	}

	private void Start()
	{	
		transform.position = QuadranSystem.instance.SearchWalkPoint(true, transform.position);
		StartCoroutine(Behaviour());
	}

	

	

	//Broute l'herbe 
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
			walkpoint = QuadranSystem.instance.SearchWalkPoint(PlayerInRange(), transform.position);
			yield return StartCoroutine(GoToPosition(walkpoint));
			yield return StartCoroutine(Graze());
		}
	}

	
	//Va a la position
	IEnumerator GoToPosition(Vector3 position)
	{
		bool _isFleeing = false;
		agent.SetDestination(walkpoint);

		while (agent.pathPending)
			yield return null;

		if (agent.pathStatus != NavMeshPathStatus.PathComplete)
		{
			
			yield break;
		}


		do
		{
			if (PlayerInRange() && !_isFleeing)
			{
				QuadranSystem.instance.SearchWalkPoint(PlayerInRange(), transform.position);
				_isFleeing = true;
				agent.SetDestination(walkpoint);
			}

			agent.speed = currentSpeed;
			yield return null;

		}
		while (agent.isActiveAndEnabled &&  agent.remainingDistance > 0.1f);
		//while (agent.remainingDistance > 0.1f);
		
		
	}

	//Detecte le player si il est in range
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

	
}
