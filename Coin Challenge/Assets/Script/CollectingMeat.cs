using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectingMeat : MonoBehaviour
{
    public static CollectingMeat instance;
    public int meatCount;
    public TMP_Text meatText;

    void Awake()
    {
        instance = this;
    }
    
    void Update()
    {
        MeatCount();
    }

    public void MeatCount()
    {
        meatText.text = "Meat Count : " + meatCount.ToString();
    }
}
