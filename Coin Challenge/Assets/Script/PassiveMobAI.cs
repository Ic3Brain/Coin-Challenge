using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PassiveMobAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkpoint;
    bool walkPointSet;
    public float walkPointRange;
    public float sightRange;
    public bool playerInSightRange 
    {
        get
        {
            return Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        }
    }

    [SerializeField]
    Animator animator;
    public float detectionRadius = 10.0f;
    [SerializeField]Transform anchor, anchor1;
     

    
    

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(Behaviour());   
    }

    //Avance ou il peut avancer
    IEnumerator Graze()
    {   
        float timeToWait = Random.Range(1f, 3f);
        Debug.Log("appelé"+ timeToWait);
        yield return new WaitForSeconds(timeToWait);
    }

    IEnumerator Behaviour()
    {
        while(true)
        {
            yield return null;
            if(!walkPointSet) SearchWalkPoint();
            yield return StartCoroutine(GoToPosition(walkpoint));
            yield return StartCoroutine(Graze());
        }
    }

    //Vérifie si il peut avancer 
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        
        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        walkpoint.x = Mathf.Clamp(walkpoint.x, anchor.position.x, anchor1.position.x);
        walkpoint.z = Mathf.Clamp(walkpoint.z, anchor.position.z, anchor1.position.z);
        
        if(Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        walkPointSet = true;
    }

    void OnDrawGizmosSelected()
    {
        // Dessiner le rayon de détection pour la visualisation dans l'éditeur
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    
    IEnumerator GoToPosition(Vector3 position)
    {   

        agent.SetDestination(walkpoint);

        while(agent.pathPending)
        yield return null;

        if(agent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            walkPointSet = false;
            yield break;
        }

        Vector3 distanceToWalkPoint;
        //animator.SetFloat("ForwardMove", 0.5f);
        do
        {   
            if(playerInSightRange)
            {
                agent.speed = 7f;
            }
            else
            {
                agent.speed = 3.5f;
            }

            distanceToWalkPoint = transform.position - walkpoint;
            yield return null;
        }
        while(distanceToWalkPoint.magnitude > 1f);
        
        walkPointSet = false;
        //animator.SetFloat("ForwardMove", 0f);
    }
}
