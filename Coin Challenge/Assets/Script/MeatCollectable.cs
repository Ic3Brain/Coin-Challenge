using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCollectable : MonoBehaviour, ICollectable
{   
    [SerializeField]
    int score;

    //Quand meat collected on ajoute le score et on détruit l'objet
    public void OnCollected()
    {
        CollectingMeat.instance.meatCount += score;
        Destroy(this.gameObject);
    }
}
