using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField]
    private GameObject deer;
    
    [SerializeField] 
    private Transform anchor, anchor1;

    
    

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {   
            
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-328, -231), 2, Random.Range(37, 152));
            Instantiate(deer, randomSpawnPosition, Quaternion.identity);         
        }
    }  
}
