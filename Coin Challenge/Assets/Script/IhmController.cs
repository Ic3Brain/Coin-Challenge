using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class IhmController : MonoBehaviour
{
    [SerializeField]
    TMP_Text timerText;
    public GameObject SoundPanel;
    public GameObject SettingsPanel;
    public GameObject GameOverPanel;
    public float time = 10f;
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    PortalAttract portalAttract;

    [SerializeField]
    AudioClip huntZoneMusic;

    [SerializeField]
    AudioSource ambiantMusic;
    



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
            time--;
            yield return new WaitForSeconds(1);
            timerText.text = string.Format("{0:0}:{1:00}", Mathf.Floor(time / 60), time % 60);
        }
        if(time == 0)
        {   
            ambiantMusic.Stop();
            portalAttract.AttrackToPortal();
        }
    }

    //Boutton echap qui permet d'afficher le Menu pause
    public void SettingsPanelOn()
    {   
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsPanel.SetActive(true);
        }
    }   
}
