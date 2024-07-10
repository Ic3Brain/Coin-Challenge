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
    
    [SerializeField]Transform anchor, anchor1;
    
    public float fov;
    public AlertStage alertStage;
    [Range(0, 1)] public float alertLevel; //Peaceful 0, Alerted 1
    [Range(0, 360)]  public float fovAngle;

    private float currentSpeed
    {
        get 
        {
            bool playerInFOV = PlayerInRange();
            if(playerInFOV)
            {
                playerLastTimeInRange = Time.timeSinceLevelLoad;
            }
            return elapseTimeSinceLastDetection > 7? 12:1.5f;
        }
    }

    float playerLastTimeInRange = 0;
    float elapseTimeSinceLastDetection
    {
        get
        {
            return playerLastTimeInRange - Time.timeSinceLevelLoad  ;
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
        StartCoroutine(Behaviour());
    }

   

    IEnumerator UpdateAlertState(bool playerInFOV)
    {
        switch(alertStage)
        {
            case AlertStage.Peaceful:
            if(playerInFOV)
                alertLevel++;
            if(alertLevel == 1)
                alertStage = AlertStage.Alerted;
                yield return new WaitForSeconds(7);
                break;
            case AlertStage.Alerted:
            if(!playerInFOV)
                alertLevel--;
            if(alertLevel == 0)
                alertStage = AlertStage.Peaceful;
                break;
            
        }
    }

    //Avance ou il peut avancer
    IEnumerator Graze()
    {   
        float timeToWait = Random.Range(3f, 10f);
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
            agent.speed = currentSpeed;
            

            distanceToWalkPoint = transform.position - walkpoint;
            yield return null;

        }
        while(distanceToWalkPoint.magnitude > 1f);
        
        walkPointSet = false;
        //animator.SetFloat("ForwardMove", 0f);
    }

    public bool PlayerInRange()
    {
        bool playerInFOV = false;
        Collider[] targetsInFOV = Physics.OverlapSphere(transform.position, fov);
        foreach(Collider c in targetsInFOV)
        {
            if(c.CompareTag("Player"))
            {   
                float signedAngle = Vector3.Angle(transform.forward, c.transform.position - transform.position);
                
                if(Mathf.Abs(signedAngle) < fovAngle / 2)
                    playerInFOV = true;
                break;
            }
        }
        return playerInFOV;
    }
}
