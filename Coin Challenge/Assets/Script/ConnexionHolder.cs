using UnityEngine;

public class ConnexionHolder : MonoBehaviour
{

    public static ConnexionHolder Instance;

    public int score;

    public int killCount;

    public float time;

    void Start()
    {   
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
    void Update()
    {

    }

    public void GetData()
    {
        score = CollectingMeat.instance.meatCount;
        killCount = IhmController.scoreValue;
        time = Chronometer.instance.elapsedTime; 
    }
}
