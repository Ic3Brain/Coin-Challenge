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

    //Met a jour le texte
    public void MeatCount()
    {
        meatText.text = " : " + meatCount.ToString();
    }
}
