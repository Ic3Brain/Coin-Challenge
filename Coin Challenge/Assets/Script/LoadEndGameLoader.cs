using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadEndGameLoader : MonoBehaviour
{
    public UnityEvent onBeforeLoad;
    
    public static LoadEndGameLoader instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene(2);
        }
    }


   public static void LoadScene()
   {    
        instance.onBeforeLoad.Invoke();
        SceneManager.LoadScene(2);
   }
}
