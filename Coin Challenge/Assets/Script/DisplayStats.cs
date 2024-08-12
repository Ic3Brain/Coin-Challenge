using UnityEngine;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text killCountText;
    public TMP_Text timeText;

    void Start()
    {
        // Vérifier que ConnexionHolder existe
        if (ConnexionHolder.Instance != null)
        {
            // Mettre à jour les TextMeshPro avec les données de ConnexionHolder
            scoreText.text = "Score: " + ConnexionHolder.Instance.score;
            killCountText.text = "Kills: " + ConnexionHolder.Instance.killCount;
            timeText.text = "Time: " + ConnexionHolder.Instance.time.ToString("F2") + "s";
        }
        else
        {
            Debug.LogError("ConnexionHolder instance not found");
        }
    }
}
