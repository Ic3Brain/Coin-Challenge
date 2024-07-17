using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerController : MonoBehaviour, IDamageable
{   [SerializeField]
    HealthManager healthManager;



    public void OnKill()
    {
        Destroy(this.gameObject);
    }

    public void OnDamage(float damage)
    {
        healthManager.RemoveHealth(damage);
    }

}
