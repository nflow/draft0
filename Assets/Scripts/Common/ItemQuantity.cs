using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemQuantity 
{
    public Item item;
    public int amount;

    public ItemQuantity() {}

    public ItemQuantity(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}