using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class DaggerController : MonoBehaviour
{   
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public int attackDamage = 10;
    public Transform weapon;
    public LayerMask deer;

    [SerializeField]
    float attackRange = 1.5f;
    
    [SerializeField] 
    LayerMask layerMask;
    
    [SerializeField]
    AudioClip attack;

    [SerializeField]
    AudioSource SFXAudioSource;
    

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AttackPlayer();
        }
    }
  
    //Attaque du joueur
    private void AttackPlayer()
    {   
        if(!alreadyAttacked)
        {   
            Collider[] hitDeer = Physics.OverlapSphere(weapon.transform.position, attackRange, layerMask);

        foreach (Collider deerCollider in hitDeer)
        {   
            Debug.Log(deerCollider.transform.root.name);
            HealthManager healthManager = deerCollider.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.RemoveHealth(attackDamage);
                Debug.Log(healthManager.gameObject.name + " " + attackDamage);
            }
        }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            SFXAudioSource.clip = attack;
            SFXAudioSource.Play();
        }
    }

    //reset l'attaque
    private void ResetAttack()
    {
        alreadyAttacked = false;
    } 
}
