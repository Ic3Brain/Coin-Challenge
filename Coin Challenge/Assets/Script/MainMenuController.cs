using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string SceneName;

    public GameObject SoundPanel;
    
    
    
    /*Lance le jeu sur la scène "game"*/
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    /*Change de scene*/
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    /*ferme le jeu*/
        public void Quit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif 
        #if UNITY_STANDALONE
        Application.Quit();
        #endif
    }

    public void SoundPanelOn()
    {
        SoundPanel.SetActive(true);
    }

    public void SoundPanelOff()
    {
        SoundPanel.SetActive(false);
    }
}
