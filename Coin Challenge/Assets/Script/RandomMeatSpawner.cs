using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMeatSpawner : MonoBehaviour
{
    public GameObject[] meat;
    public int meatCount = 1;

    
    void Start()
    {
        
    }

    public void SpawnMeat(int meatCount)
    {
        for(int i = 0; i < meatCount; i++)
        {
            int randomIdex = Random.Range(0, meat.Length);
            Instantiate(meat[randomIdex], transform.position, Quaternion.identity);
        }
    }
}
