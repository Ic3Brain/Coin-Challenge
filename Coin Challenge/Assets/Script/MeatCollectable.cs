using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCollectable : MonoBehaviour, ICollectable
{   
    [SerializeField]
    int score;

    public bool isCollectable
    {
        get
        {
            return _isCollectable;
        }
    }

    public bool _isCollectable;


    //Quand meat collected on ajoute le score et on détruit l'objet
    public void OnCollected()
    {
        CollectingMeat.instance.meatCount += score;
        Destroy(this.gameObject);
    }
}
