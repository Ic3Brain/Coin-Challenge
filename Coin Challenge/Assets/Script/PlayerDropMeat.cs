using System.Collections;
using UnityEngine;

public class PlayerDropMeat : MonoBehaviour
{   
    [SerializeField]
    RandomMeatSpawner randomMeatSpawner;

    [SerializeField]
    Toolbar toolbar;

    [SerializeField]
    CollectingMeat collectingMeat;

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
        MeatCollectable selectedMeat = toolbar.GetCurrentMeatItem();

        if(Input.GetMouseButtonDown(1))
        {   
            if (!canSpawnMeat)
            {
                return;
            }
            
            if (selectedMeat == null || selectedMeat.score > CollectingMeat.instance.meatCount)
            {
                return;
            }
            
            randomMeatSpawner.SpawnMeat(currentSlotIndex, false, transform.position);
            CollectingMeat.instance.meatCount -= selectedMeat.score;
            StartCoroutine(StartCooldown());
        }   
    }

    //Commence un CD entre chaque clique droit
    public IEnumerator StartCooldown()
    {
        canSpawnMeat = false;
        yield return new WaitForSeconds(meatSpawnCooldown);
        canSpawnMeat = true;
    }
}
