using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectingMeat : MonoBehaviour
{

    public int meatCount;
    public TMP_Text meatText;
    void Update()
    {
        meatText.text = "Meat Count : " + meatCount.ToString();
    }
}
