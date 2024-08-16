using UnityEngine;

public class MeatSpawner : MonoBehaviour
{   
    public GameObject[] meat;

    [SerializeField]
    RandomMeatSpawner randomMeatSpawner;

     void Start()
    {
        randomMeatSpawner.SpawnMeat(1, transform.position);
    }
}
