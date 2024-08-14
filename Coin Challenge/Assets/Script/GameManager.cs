using UnityEngine;


public class GameManager : MonoBehaviour
{   
    [SerializeField]
    private HealthManager healthManager;
    
    [SerializeField]
    private IhmController ihmController;
    
    [SerializeField]
    private Player_Controller playerController;
     public static GameManager instance;

    [SerializeField]
    CollectingMeat collectingMeat;

    [SerializeField]
    Chronometer chronometer;
    
    void Awake()
    {
        instance = this;
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
        chronometer.OnApplicationPause(true);
        ihmController.ChronoPause(true);
    }

    //Restart du jeu 
    public void Restart()
    {   
        playerController.Respawn();
        collectingMeat.meatCount = 0;
        healthManager.health = healthManager.maxHealth;
        chronometer.elapsedTime = 0;
        chronometer.OnApplicationPause(false);
    }
}
