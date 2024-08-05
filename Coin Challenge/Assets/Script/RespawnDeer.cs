using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnDeer : MonoBehaviour
{
    private const int TotalDeerCount = 8;

    [SerializeField]
    GameObject deerPrefab;



    public List<GameObject> deerList = new List<GameObject>();

    void Start()
    {   
        
        for (int i = 0; i < TotalDeerCount; i++)
        {
            SpawnDeer();
        }
    }

    void Update()
    {
        // Logique pour respawner les cerfs morts
        for (int i = 0; i < deerList.Count; i++)
        {
            if (deerList[i] == null)
            {
                
            }
        }
    }

    //instancie un cerf 
    void SpawnDeer()
    {
        DeerController deer = Instantiate(deerPrefab).GetComponent<DeerController>();
    }
}
