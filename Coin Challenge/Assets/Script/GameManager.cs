using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements.Experimental;

public class GameManager : MonoBehaviour
{   
    [SerializeField]
    private HealthManager healthManager;
    
    [SerializeField]
    private IhmController ihmController;
    [SerializeField]
    private Player_Controller player_Controller;
     public static GameManager INSTANCE;

    [SerializeField]
    CollectingMeat collectingMeat;
    
    void Awake()
    {
        INSTANCE = this;
    }
    void Start()
    {
        StartNewGame();
    }

    public void StartNewGame()
    {
        Restart();
    }

    //Si vie <= 0 = gameOver
    public void IsGameOver()
    {   
        if(healthManager.health <= 0)
        {   
            OnGameOver();
        }
    }

    //Affiche le gameOver panel
    public void OnGameOver()
    { 
        ihmController.GameOverPanel.SetActive(true);
    }

    //Restart du jeu 
    public void Restart()
    {   
        player_Controller.Respawn();
        collectingMeat.meatCount = 0;
        healthManager.health = healthManager.maxHealth;
        ihmController.TimeChrono();
    }
}
