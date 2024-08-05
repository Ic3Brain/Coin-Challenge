using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    
    private IDamageable damageable;
    
    
   void Awake()
   {
        damageable = GetComponent<IDamageable>();
        if(damageable == null)
        {
            Debug.LogError("l'interface idamageable n'a pas été trouvée");
        }
   }

   
    public bool IsAlive
    {
        get
        {
            return damageable.IsAlive;
        }
    }

    //retire la vie
    public void RemoveHealth(float lostHealth)
    {
        health -= lostHealth;
        damageable.OnDamage(lostHealth);
        if(health <= 0)
        {
            damageable.OnKill();
        }
    }
    
    
}


