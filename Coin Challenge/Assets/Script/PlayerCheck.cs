using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{   
    //On détecte la col avec un "EnemyHeadCheck" puis on le détruit
    private void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<EnemyHeadCheck>())
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
