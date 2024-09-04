using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAi : MonoBehaviour
{   
    public NavMeshAgent agent;
    public Transform player;
    
    public LayerMask whatIsGround, whatIsPlayer, whatIsMeat;
    public Vector3 walkpoint;
    bool walkPointSet;
    public float walkPointRange;
    public float timeBetweenAttacks;
    HealthManager playerHealthManager;
    public float sightRange, attackRange;
    public bool meatInSightRange;
    //public int attackDamage = 25;
    //public float chaseSpeed = 10f;
    //public float normalSpeed = 1f;
    public Transform weapon;
    
    [SerializeField]
    Animator animator;
    
    [SerializeField]
    HealthManager healthManager;
    
    bool isEatingMeat;

    [SerializeField]
    AudioClip attack;

    [SerializeField]
    AudioSource SFXAudioSource;
    
    [SerializeField]
    TigerSettings tigerSettings;
    
    

    bool playerInSightRange
    {
        get
        {   
            if(!playerHealthManager.IsAlive)
            return false;
            return
            Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        }
    }

    bool playerInAttackRange
    {
        get
        {
            return
            Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        }
    }
    
    

    private void Awake()
    {   
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void Start()
    {   
        playerHealthManager = player.gameObject.GetComponent<HealthManager>();
    }


    private void Update()
    {
        LookForMeat();
    }
    

    //Avance ou il peut avancer
    public IEnumerator PatrolingCorout()
    {   
        agent.speed = tigerSettings.normalSpeed;

        do
        {   

        if(!walkPointSet) SearchWalkPoint();

        

        if(walkPointSet)
            agent.SetDestination(walkpoint);

        while (agent.pathPending)
			yield return null;

		if (agent.pathStatus != NavMeshPathStatus.PathComplete)
		{
			Debug.Log(agent.pathStatus);
            walkPointSet = false;
			yield break;
		}
        

            Vector3 distanceToWalkPoint;
        do
        {   
            
            distanceToWalkPoint = transform.position - walkpoint;
            
            if(playerInSightRange)
            {
                StartCoroutine(ChasePlayerCorout());
                yield break;
            }

            animator.SetFloat("ForwardMove", 0.5f);
            yield return null;
        }

        while(distanceToWalkPoint.magnitude > 1.5f);        
        
        walkPointSet = false;
        }


    while(true);
    }

    //Vérifie si il peut avancer 
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        /*walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        if(Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        walkPointSet = true;*/

    Vector3 randomPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    
    NavMeshHit hit;
    
        if (NavMesh.SamplePosition(randomPoint, out hit, walkPointRange, NavMesh.AllAreas))
        {
            walkpoint = hit.position;
            walkPointSet = true;
        }
        else
        {
            walkPointSet = false;
        }
    }

    //Va a la position du player
    private IEnumerator ChasePlayerCorout()
    {   
        agent.speed = tigerSettings.chaseSpeed;
        
        Vector3 distanceToWalkPoint;
        do 
        {   
            agent.SetDestination(player.position);
            distanceToWalkPoint = transform.position - player.position;
            
            animator.SetFloat("ForwardMove", 1f);
            yield return null;
        }
        
        while(distanceToWalkPoint.magnitude > 1.5f && playerInSightRange);
        
        if(playerInAttackRange)
        {  
            StartCoroutine(AttackPlayerCorout());
        }
        else 
        
        if(!playerInSightRange)
        {   
            StartCoroutine(PatrolingCorout());
        }
    }

    //Attaque le joueur
    private IEnumerator AttackPlayerCorout()
    {   
        Collider[] hitPlayers;
        
        do
       {    
            transform.LookAt(player);

            hitPlayers = Physics.OverlapSphere(weapon.transform.position, attackRange, whatIsPlayer);
            
            foreach (Collider playerCollider in hitPlayers)
            {   
                SFXAudioSource.clip = attack;
                SFXAudioSource.Play();
                
                HealthManager player = playerCollider.transform.root.GetComponent<HealthManager>();
                if (player != null)
                {
                    healthManager.RemoveHealth(tigerSettings.attackDamage);
                    if(!healthManager.IsAlive)
                    yield break;
                }

                yield return new WaitForSeconds(timeBetweenAttacks);
                break;
            }
        }    

       while(hitPlayers.Length > 0);

       if(playerInSightRange)
       {
            StartCoroutine(ChasePlayerCorout());
       }
       else 
       {
            StartCoroutine(PatrolingCorout());
       }
    }

    //Regarde si il y a de la meat dans la zone
    public void LookForMeat()
    {   
        if(isEatingMeat)
        return;

        Collider[] hitColliders = new Collider[10];
        int numbCollider = Physics.OverlapSphereNonAlloc(transform.position, sightRange, hitColliders, whatIsMeat, QueryTriggerInteraction.UseGlobal);
        
        if(numbCollider > 0)
        {
            meatInSightRange = true;
            
            StopAllCoroutines();
            MeatCollectable meat = hitColliders[0].transform.root.GetComponent<MeatCollectable>();
            StartCoroutine(EatMeat(meat));
            
        }
    }

    //va manger la meat 
    public IEnumerator EatMeat(MeatCollectable meat)
    {   
        isEatingMeat = true;
        meat.SetTriggerActive(false);
        agent.SetDestination(meat.transform.position);
        while(Vector3.Distance(transform.position, meat.transform.position) > 2.5f)
        yield return null;
        animator.SetFloat("ForwardMove", 0f);
        yield return new WaitForSeconds(meat.eatDuration);
        Destroy(meat.gameObject);
        isEatingMeat = false;
        StartCoroutine(PatrolingCorout());
    }
}
