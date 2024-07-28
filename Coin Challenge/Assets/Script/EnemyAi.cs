using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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
    bool alreadyAttacked;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, meatInSightRange;
    public int attackDamage = 25;
    public Transform weapon;
    
    [SerializeField]
    Animator animator;
    
    [SerializeField]
    HealthManager healthManager;
    
    
    

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        //meat = GameObject.Find("Meat").transform;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        meatInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsMeat);

        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInAttackRange && playerInSightRange) AttackPlayer();
        if(meatInSightRange) DistractedByMeat();
    }

    //Avance ou il peut avancer
    private void Patroling()
    {
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkpoint);

        Vector3 distanceToWalkPoint = transform.position - walkpoint;

        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
            animator.SetFloat("ForwardMove", 0.5f);
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
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetFloat("ForwardMove", 1f);
    }

    //Attaque le joueur
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        
        
        if(!alreadyAttacked)
        {   
            Collider[] hitPlayers = Physics.OverlapSphere(weapon.transform.position, attackRange, whatIsPlayer);

        foreach (Collider playerCollider in hitPlayers)
        {
            HealthManager player = playerCollider.GetComponent<HealthManager>();
            if (player != null)
            {
                healthManager.RemoveHealth(attackDamage);
                Debug.Log("Player attacked! Damage: " + attackDamage);
            }
        }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    //reset l'attaque
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    public void DistractedByMeat()
    {   
       Debug.Log("je cours sur la meat");
        agent.SetDestination(meat.position);
    }
}
