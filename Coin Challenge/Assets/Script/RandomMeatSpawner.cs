using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMeatSpawner : MonoBehaviour
{
    public GameObject[] meat;

    
    void Start()
    {
        int randomIdex = Random.Range(0, meat.Length);
        Instantiate(meat[randomIdex], transform.position, Quaternion.identity);
    }
}
