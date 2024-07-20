using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField]
    private GameObject deer;


    public void DeerSpawnZone()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-328, -231), 2, Random.Range(37, 152));
        Instantiate(deer, randomSpawnPosition, Quaternion.identity);         
    }
}
