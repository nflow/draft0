using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemQuantity : Object
{
    public Item item;
    public int amount;

    public ItemQuantity(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}