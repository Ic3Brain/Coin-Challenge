using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCollectable : MonoBehaviour, ICollectable
{   
    [SerializeField]
    public int score;

    [SerializeField]
    Collider trigger; 

    public float eatDuration = 4f;

    public bool isCollectable
    {
        get
        {
            return _isCollectable;
        }

        set
        {
            _isCollectable = value;
        }
    }

    private bool _isCollectable;

    
    public void SetTriggerActive(bool value)
    {
        trigger.enabled = value;
    }

    
    //Quand meat collected on ajoute le score et on détruit l'objet
    public void OnCollected()
    {
        CollectingMeat.instance.meatCount += score;
        Destroy(this.gameObject);
    }
}
