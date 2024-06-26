using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class IhmController : MonoBehaviour
{
    [SerializeField]
    TMP_Text timerText;
    float elapsedTime;
    public GameObject SoundPanel;
    public GameObject SettingsPanel;
    public GameObject GameOverPanel;
    
    [SerializeField]
    GameManager gameManager;
    

    

    

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

    //Chrono qui permet de voir le temps
    public void TimeChrono()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //Boutton echap qui permet d'afficher le Menu pause
    public void SettingsPanelOn()
    {   
        Debug.Log("je suis appelé");
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsPanel.SetActive(true);
        }
    }   
}
