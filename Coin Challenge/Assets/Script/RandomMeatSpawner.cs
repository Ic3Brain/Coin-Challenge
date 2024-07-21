using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class RandomMeatSpawner : MonoBehaviour
{
    public GameObject[] meat;
    
    

    public void SpawnMeat(int meatCount)
    {
        for(int i = 0; i < meatCount; i++)
        {
            int randomIdex = Random.Range(0, meat.Length);
            Instantiate(meat[randomIdex], transform.position, Quaternion.identity);
        }
    }
}
