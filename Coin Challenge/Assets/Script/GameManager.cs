using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{   
    [SerializeField]
    private HealthManager healthManager;
    
    [SerializeField]
    private IhmController ihmController;
    
    
    public void IsGameOver()
    {   
        if(healthManager.health <= 0)
        {   
            OnGameOver();
        }
    }

    public void OnGameOver()
    {   
        ihmController.GameOverPanel.SetActive(true);
    }

    

    
}
