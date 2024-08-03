using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnexionHolder : MonoBehaviour
{

    public static ConnexionHolder Instance;
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
            SceneManager.LoadScene(2);
        }
    }
}
