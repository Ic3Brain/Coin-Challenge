using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCheck : MonoBehaviour
{
   [SerializeField]
   private Rigidbody playerRb;


    //si je saute sur le tigre je fais un petit bond
   private void OnTriggerEnter(Collider col)
   {
    if(col.GetComponent<PlayerCheck>())
    {
        playerRb.velocity = new Vector3(playerRb.velocity.x, 0f);
        playerRb.AddForce(Vector3.up * 300f);
    }
   }
}
