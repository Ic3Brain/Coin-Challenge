using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCollectable : MonoBehaviour, ICollectable
{   
    [SerializeField]
    int score;

    public void OnCollected()
    {
        CollectingMeat.instance.meatCount += score;
        Destroy(this.gameObject);
    }
}
