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
    HealthManager healthManager;
    

  

    private void AttackPlayer()
    {   
        Debug.Log("je suis appelé");
        if(!alreadyAttacked)
        {   
            Collider[] hitDeer = Physics.OverlapSphere(weapon.transform.position, deer);

        foreach (Collider deerCollider in hitDeer)
        {
            HealthManager deer = deerCollider.GetComponent<HealthManager>();
            if (deer != null)
            {
                healthManager.RemoveHealth(attackDamage);
                Debug.Log("Deer attacked ! Damage: " + attackDamage);
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
}
