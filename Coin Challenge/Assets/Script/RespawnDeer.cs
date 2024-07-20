using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnDeer : MonoBehaviour
{
    private const int TotalDeerCount = 8;

    [SerializeField]
    SpawnZone spawnZone;

    private List<GameObject> deerList = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < TotalDeerCount; i++)
        {
            spawnZone.DeerSpawnZone();
        }
    }

    void Update()
    {
        // Logique pour respawner les cerfs morts
        for (int i = 0; i < deerList.Count; i++)
        {
            if (deerList[i] == null)
            {
                spawnZone.DeerSpawnZone();
            }
        }
    }
}
