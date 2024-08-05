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
    
    //clique droit pour faire spawn une meat
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

    //Commence un CD entre chaque clique droit
    public IEnumerator StartCooldown()
    {
        canSpawnMeat = false;
        yield return new WaitForSeconds(meatSpawnCooldown);
        canSpawnMeat = true;
        Debug.Log("Cooldown finished, can spawn meat again.");
    }
}
