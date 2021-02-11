using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryElement
{
    public Item item;
    public int realAmount;
    public int virtualAmount;

    public InventoryElement(Item item, int realAmount, int virtualAmount)
    {
        this.item = item;
        this.realAmount = realAmount;
        this.virtualAmount = virtualAmount;
    }
}
