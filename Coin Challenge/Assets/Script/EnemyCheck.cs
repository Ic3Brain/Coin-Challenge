using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{   
    //On détecte la col avec 'EnemyAi' et on détruit l'objet
    private void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<EnemyAi>())
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
