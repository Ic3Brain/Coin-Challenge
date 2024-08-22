using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IhmController : MonoBehaviour
{   
    public GameObject SoundPanel;
    public GameObject SettingsPanel;
    public GameObject GameOverPanel;
    public static int scoreValue = 0;
    public static IhmController instance;
    public float time = 180f;
    private bool isPaused = false;
    public GameObject chronoImage;
    public Image imageChrono;
    private Coroutine chronoCoroutine;

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    PortalAttract portalAttract;

    [SerializeField]
    AudioClip huntZoneMusic;

    [SerializeField]
    AudioSource ambiantMusic;

    [SerializeField]
    TMP_Text score;

     [SerializeField]
    TMP_Text timerText;

    [SerializeField]
    Chronometer chronometer;
    
    [SerializeField]
    Player_Controller player_Controller;
    
    [SerializeField]
    public FreeLookCamera freeLookCamera;

    [SerializeField]
    DaggerController daggerController;

    public float timeRate
    {
        get
        {
            return time / 180f;
        }
    }

    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
       ambiantMusic.clip = huntZoneMusic;
       ambiantMusic.Play();
    }
    
    void Update()
    {
        SettingsPanelOn();
        KillCount();
    }

    //Affiche le panel de gestion du son 
    public void SoundPanelOn()
    {
        SoundPanel.SetActive(true);
    }

    //Enlève le panel de gestion du son 
    public void SoundPanelOff()
    {
        SoundPanel.SetActive(false);
    }

    //Boutton resume qui reprend la partie/enlève la pause
    public void ResumeButton()
    {
        SettingsPanel.SetActive(false);
        chronometer.OnApplicationPause(false);
        ChronoPause(false);
        freeLookCamera.Unlock();
        player_Controller.UnFreezeMovement();
        daggerController.EnableAttackComponents();
        player_Controller.EnableJumpComponents();
    }

    //Boutton qui permet le restart de la partie quand mort
    public void RestartButton()
    {   
        gameManager.Restart();
        GameOverPanel.SetActive(false); 
    }
    

    // Démarre le chronomètre
    public void StartChrono(float startTime)
    {
        time = startTime; 
        UpdateTimerText(); 
        isPaused = false; 

        if (chronoCoroutine != null)
        {
            StopCoroutine(chronoCoroutine);
        }
        chronoCoroutine = StartCoroutine(TimeChrono());
    }

    // Arrête le chronomètre
    public void StopChrono()
    {
        if (chronoCoroutine != null)
        {
            StopCoroutine(chronoCoroutine);
            chronoCoroutine = null;
        }
    }


    //Chrono
    public IEnumerator TimeChrono()
    {
        while(time > 0)
        {   
            if(!isPaused)
            {
                time--;
                UpdateTimerText();
                 yield return new WaitForSeconds(1);
            }
            else
            {
                yield return null;
            }

            
        }
        if(time <= 0)
        {   
            ambiantMusic.Stop();
            portalAttract.AttrackToPortal();
            chronoImage.SetActive(false);
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = string.Format("{0:0}:{1:00}", Mathf.Floor(time / 60), time % 60);
        FillTheTimer();
    }   

    public void ChronoPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
    }

    //Boutton echap qui permet d'afficher le Menu pause
    public void SettingsPanelOn()
    {   
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsPanel.SetActive(true);
            chronometer.OnApplicationPause(true);
            ChronoPause(true);
            player_Controller.FreezeMovement();
            freeLookCamera.Lock();
            daggerController.DisableAttackComponents();
            player_Controller.DisableJumpComponents();
        }
    }
    
    //compte le nombre de tigre tué
    public void KillCount()
    {
        score.text = "Kill: " + scoreValue.ToString();
    }   

    public void FillTheTimer()
    {
        imageChrono.fillAmount = timeRate;
    }
}
