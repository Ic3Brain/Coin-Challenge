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
    

    

    

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SoundPanelOn()
    {
        SoundPanel.SetActive(true);
    }

    public void SoundPanelOff()
    {
        SoundPanel.SetActive(false);
    }

    public void ResumeButton()
    {
        SettingsPanel.SetActive(false);
    }

    public void RestartButton()
    {
        GameOverPanel.SetActive(false);
    }

    


    public void SettingsPanelOn()
    {   
        Debug.Log("je suis appelé");
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsPanel.SetActive(true);
        }
    }   
}
