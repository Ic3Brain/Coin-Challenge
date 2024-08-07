using UnityEngine;
using UnityEngine.SceneManagement;

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
        //Je met les infos la ? 
        if(Input.GetKeyDown(KeyCode.Y))
        {
            
        }
    }

    public void GetData()
    {
        score = CollectingMeat.instance.meatCount;
        //mettre ici les infos 
    }
}
