using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerController : MonoBehaviour, IDamageable
{
    [SerializeField]
    RandomMeatSpawner randomMeatSpawner;

    public bool IsAlive => throw new System.NotImplementedException();

    public void OnKill()
    {   
        randomMeatSpawner.SpawnMeat(1, transform.position);
        Destroy(this.gameObject);
    }

    public void OnDamage(float damage)
    {
        
    }

    
}
