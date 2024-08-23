using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour {


    public RectTransform highlight;
    public ItemSlot[] itemSlots;

    int slotIndex = 0;

    void Start() 
    {
        foreach (ItemSlot slot in itemSlots) 
        {
            slot.icon.enabled = true;
        }

    }

    void Update() 
    {
        ScrollWheelSlot();
    }

    //Permet de scroll avec la molette et etre sur le bon index
    public void ScrollWheelSlot()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0) {

            if (scroll > 0)
                slotIndex--;
            else
                slotIndex++;

            if (slotIndex > itemSlots.Length - 1)
                slotIndex = 0;
            if (slotIndex < 0)
                slotIndex = itemSlots.Length - 1;

            highlight.position = itemSlots[slotIndex].icon.transform.position;
        }
    }
    
    public int GetCurrentSlotIndex()
    {
        return slotIndex;
    }

    public MeatCollectable GetCurrentMeatItem()
    {
        if (slotIndex >= 0 && slotIndex < itemSlots.Length)
        {
            return itemSlots[slotIndex].meatItem;
        }
        return null;
    }
}

[System.Serializable]
public class ItemSlot 
{
    public Image icon;
    public MeatCollectable meatItem;
}