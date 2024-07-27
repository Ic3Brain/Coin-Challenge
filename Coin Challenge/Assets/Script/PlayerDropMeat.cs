using System.Collections;
using UnityEngine;

public class PlayerDropMeat : MonoBehaviour
{   
    [SerializeField]
    RandomMeatSpawner randomMeatSpawner;

    [SerializeField]
    Toolbar toolbar;

    public float meatSpawnCooldown = 2f; 
    public bool canSpawnMeat = true;
    

    void Update()
    {
        RmbMeatDrop();
    }
    
    public void RmbMeatDrop()
    {   

        int currentSlotIndex = toolbar.GetCurrentSlotIndex();

        if(Input.GetMouseButtonDown(1))
        {   
            randomMeatSpawner.SpawnMeat(currentSlotIndex, false, transform.position);
            StartCoroutine(StartCooldown());
            Debug.Log("je suis appelé" + StartCoroutine(StartCooldown()));
        }   
    }


    public IEnumerator StartCooldown()
    {
        canSpawnMeat = false;
        yield return new WaitForSeconds(meatSpawnCooldown);
        canSpawnMeat = true;
        Debug.Log("Cooldown finished, can spawn meat again.");
    }
}
