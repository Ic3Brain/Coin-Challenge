using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerHealthBar : MonoBehaviour
{

    public Image healthBarImage;
    public TMP_Text healthText;
    public static PlayerHealthBar Instance;
    
    
    void Awake()
    {
        Instance = this;
    }

    
    public void UpdateBar(HealthManager healthManager)
    {
        healthBarImage.fillAmount = healthManager.health / healthManager.maxHealth;
        healthText.text = healthManager.health + " / " + healthManager.maxHealth;
        healthManager.health = Mathf.Clamp(healthManager.health, 0f, healthManager.maxHealth);
    }
}
