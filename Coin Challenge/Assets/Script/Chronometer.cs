using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Chronometer : MonoBehaviour
{
    public TMP_Text chronoText; // A UI Text to display the chronometer
    public float elapsedTime = 0f;
    private bool isPaused = false;
    public static Chronometer instance;


    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        // Initialize the chronometer
        chronoText.text = "Time: 0.00s";
    }

    void Update()
    {
        // If the game is not paused, increment the chronometer
        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            chronoText.text = "Time: " + elapsedTime.ToString("F2") + "s";
        }
    }

    // Function to pause the chronometer
    public void PauseChronometer()
    {
        isPaused = true;
    }

    // Function to resume the chronometer
    public void ResumeChronometer()
    {
        isPaused = false;
    }

    // Detect when the application is paused (e.g., game pause, minimized, etc.)
    public void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
    }
}


