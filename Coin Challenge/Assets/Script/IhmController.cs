using System.Collections;
using TMPro;
using UnityEngine;

public class IhmController : MonoBehaviour
{   
    public GameObject SoundPanel;
    public GameObject SettingsPanel;
    public GameObject GameOverPanel;
    public static int scoreValue = 0;
    public static IhmController instance;
    public float time = 10f;
    private bool isPaused = false;


    public GameObject chronoImage;

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
    FreeLookCamera freeLookCamera;

    

    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
       StartCoroutine(TimeChrono());
       ambiantMusic.clip = huntZoneMusic;
       ambiantMusic.Play();
    }
    
    void Update()
    {
        TimeChrono();
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
    }

    //Boutton qui permet le restart de la partie quand mort
    public void RestartButton()
    {   
        gameManager.Restart();
        GameOverPanel.SetActive(false);
        
    }

    
    //Chrono
    public IEnumerator TimeChrono()
    {
        while(time > 0)
        {   
            if(!isPaused)
            {
                time--;
                timerText.text = string.Format("{0:0}:{1:00}", Mathf.Floor(time / 60), time % 60);
            }
            
            yield return new WaitForSeconds(1);
            
        }
        if(time == 0)
        {   
            ambiantMusic.Stop();
            portalAttract.AttrackToPortal();
            chronoImage.SetActive(false);
        }
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
        }
    }

    

    
    //compte le nombre de tigre tué
    public void KillCount()
    {
        score.text = "Kill: " + scoreValue.ToString();
    }   
}
