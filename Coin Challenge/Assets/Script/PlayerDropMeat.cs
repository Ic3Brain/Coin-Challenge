using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropMeat : MonoBehaviour
{   
    [SerializeField]
    RandomMeatSpawner randomMeatSpawner;



    void Update()
    {
        
    }



    public void RmbMeatDrop(int meatCount)
    {   
        if(Input.GetMouseButtonDown(1))
        {   
            randomMeatSpawner.SpawnMeat(meatCount);  
        }
        
    }
}
