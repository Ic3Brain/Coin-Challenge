using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class RandomMeatSpawner : MonoBehaviour
{
    public GameObject[] meat;
    


    public void SpawnMeat(int meatCount, Vector3 spawnPosition)
    {
        for(int i = 0; i < meatCount; i++)
        {
            int randomIdex = Random.Range(0, meat.Length);
            Instantiate(meat[randomIdex], spawnPosition, Quaternion.identity);
        }
    }

    public void SpawnMeat(int meatId, bool isCollectable, Vector3 spawnPos)
    {
        GameObject meatInstance = Instantiate(meat[meatId], spawnPos, Quaternion.identity);
        MeatCollectable meatCtrl = meatInstance.GetComponent<MeatCollectable>();
        meatCtrl._isCollectable = isCollectable;
    }
}
