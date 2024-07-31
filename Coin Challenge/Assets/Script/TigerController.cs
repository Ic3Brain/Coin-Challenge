using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerController : MonoBehaviour, IDamageable
{   [SerializeField]
    HealthManager healthManager;

    public bool IsAlive => throw new System.NotImplementedException();

    public void OnKill()
    {
        Destroy(this.gameObject);
    }

    public void OnDamage(float damage)
    {
        healthManager.RemoveHealth(damage);
    }

}
