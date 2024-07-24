using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropMeat : MonoBehaviour
{   
    [SerializeField]
    RandomMeatSpawner randomMeatSpawner;

    public float meatSpawnCooldown = 2f; 
    public bool canSpawnMeat = true;


    void Update()
    {
        RmbMeatDrop();
    }
    
    public void RmbMeatDrop()
    {   
        if(Input.GetMouseButtonDown(1))
        {   
            randomMeatSpawner.SpawnMeat(1, transform.position);
            StartCoroutine(StartCooldown());
        }   
    }


    public IEnumerator StartCooldown()
    {
        canSpawnMeat = false;
        yield return new WaitForSeconds(meatSpawnCooldown);
        canSpawnMeat = true;
    }
}
