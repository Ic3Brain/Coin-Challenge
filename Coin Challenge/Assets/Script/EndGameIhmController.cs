using System.Collections;
using TMPro;
using UnityEngine;

public class EndGameIhmController : MonoBehaviour
{   
    public GameObject SoundPanel;
    public GameObject SettingsPanel;

    void Awake()
    {
        
    }

    void Start()
    {
       
    }
    
    void Update()
    {
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

    //Boutton echap qui permet d'afficher le Menu pause
    public void SettingsPanelOn()
    {   
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsPanel.SetActive(true);
        }
    }
}