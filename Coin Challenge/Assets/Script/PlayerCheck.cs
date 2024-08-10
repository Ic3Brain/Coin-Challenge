using UnityEngine;

public class PlayerCheck : MonoBehaviour
{   
    //On détecte la col avec un "EnemyHeadCheck" puis on le détruit
    public void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<EnemyHeadCheck>())
        {  
            IhmController.scoreValue += 1;
            Destroy(transform.parent.gameObject);
        }
    }
}
