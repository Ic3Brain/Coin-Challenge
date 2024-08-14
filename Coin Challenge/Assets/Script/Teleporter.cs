using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject player;
    public GameObject tp;

    

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("teleporter"))
        {   
            //player.transform.position = tp.transform.position;
            LoadEndGameLoader.LoadScene();
        }
    }
}
