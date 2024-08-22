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

    //Actualise la barre de vie
    public void UpdateBar(HealthManager healthManager)
    {
        healthBarImage.fillAmount = healthManager.healthRate;
        healthText.text = healthManager.health + " / " + healthManager.maxHealth;
        healthManager.health = Mathf.Clamp(healthManager.health, 0f, healthManager.maxHealth);
    }
}
