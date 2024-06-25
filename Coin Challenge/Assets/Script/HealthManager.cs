using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public Image healthBarImage;
    public TMP_Text healthText;
    public GameManager gameManager;
    
   
    
    void Update()
    {
        healthBarImage.fillAmount = health / maxHealth;
        health = Mathf.Clamp(health, 0f, maxHealth);
        healthText.text = health + " / " + maxHealth;
    }

    public void AttackTest(int damageAmount)
    {
        health -= damageAmount;
        gameManager.IsGameOver();
    }

    
}
