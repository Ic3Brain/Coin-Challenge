using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyAi : MonoBehaviour
{   
    public NavMeshAgent agent;
    public Transform player;
    public Transform meat;
    public LayerMask whatIsGround, whatIsPlayer, whatIsMeat;
    public Vector3 walkpoint;
    bool walkPointSet;
    public float walkPointRange;
    public float timeBetweenAttacks;
    HealthManager playerHealthManager;
    public float sightRange, attackRange;
    public bool meatInSightRange;
    public int attackDamage = 25;
    public Transform weapon;
    
    [SerializeField]
    Animator animator;
    
    [SerializeField]
    HealthManager healthManager;
    
    List<GameObject> availableMeat = new List<GameObject>();

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
        StartCoroutine(PatrolingCorout());
    }


    private void Update()
    {
        
    }


    //Avance ou il peut avancer
    private IEnumerator PatrolingCorout()
    {   
        
        do
        {   
            Debug.Log("je m'execute");
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkpoint);

        

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

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        if(Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        walkPointSet = true;
    }

    //Va a la position du player
    private IEnumerator ChasePlayerCorout()
    {   
        
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
            Debug.Log(hitPlayers.Count());
            foreach (Collider playerCollider in hitPlayers)
            {
                Debug.Log(playerCollider.gameObject.name);
                HealthManager player = playerCollider.transform.root.GetComponent<HealthManager>();
                if (player != null)
                {
                    healthManager.RemoveHealth(attackDamage);
                    if(!healthManager.IsAlive)
                    yield break;
                    Debug.Log("Player attacked! Damage: " + attackDamage);
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

    
    public void LookForMeat()
    {
        Collider[] hitColliders = new Collider[10];
        int numbCollider = Physics.OverlapSphereNonAlloc(transform.position, sightRange, hitColliders, whatIsMeat, QueryTriggerInteraction.UseGlobal);
        Debug.Log(numbCollider);
        if(numbCollider > 0)
        {
            meatInSightRange = true;
            agent.SetDestination(hitColliders[0].transform.position);
        }
    }
}
