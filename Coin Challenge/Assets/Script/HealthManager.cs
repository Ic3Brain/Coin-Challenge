using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public float healthRate
    {
        get
        {
            return health / maxHealth;
        }
    }
    
    private IDamageable damageable;
    
    [SerializeField]
    Renderer rend;

    [SerializeField]
    Material damageMat;
   
   
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
        if(rend != null)
        {
            StartCoroutine(DamageFeedBack());
        }
        damageable.OnDamage(lostHealth);
        if(health <= 0)
        {
            damageable.OnKill();
        }
    }
    
    IEnumerator DamageFeedBack()
    {
        Material originalMat = rend.material;

        rend.material = damageMat;
        yield return new WaitForSeconds(1);
        rend.material = originalMat; 
    }
    
}


