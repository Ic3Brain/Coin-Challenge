using UnityEngine;


public class GameManager : MonoBehaviour
{   
    [SerializeField]
    private HealthManager healthManager;
    
    [SerializeField]
    IhmController ihmController;
    
    [SerializeField]
    private Player_Controller playerController;
     public static GameManager instance;

    [SerializeField]
    CollectingMeat collectingMeat;

    [SerializeField]
    Chronometer chronometer;

    [SerializeField]
    SunRotation sunRotation;

    private EnemyAi[] enemies;


    
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
        ihmController.freeLookCamera.Lock();
    }

    //Restart du jeu 
    public void Restart()
    {   
        healthManager.health = healthManager.maxHealth;
        PlayerHealthBar.Instance.UpdateBar(healthManager);
        playerController.Respawn();
        collectingMeat.meatCount = 0;
        chronometer.elapsedTime = 0;
        chronometer.OnApplicationPause(false);
        ihmController.StopChrono();
        ihmController.StartChrono(180);
        sunRotation.StartSunRotation();
        IhmController.scoreValue = 0;
        playerController.player.SetActive(true);
        ihmController.freeLookCamera.Unlock();

        enemies = FindObjectsOfType<EnemyAi>();

        foreach(EnemyAi enemy in enemies)
        {
            //StopAllCoroutines();
            StartCoroutine(enemy.PatrolingCorout());
        }
    }
}
