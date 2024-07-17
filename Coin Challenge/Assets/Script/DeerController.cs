using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerController : MonoBehaviour, IDamageable
{
    [SerializeField]
    RandomMeatSpawner randomMeatSpawner;



    public void OnKill()
    {   
        randomMeatSpawner.SpawnMeat(1);
        Destroy(this.gameObject);
    }

    public void OnDamage(float damage)
    {
        
    }
}
