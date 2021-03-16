using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemQuantityList : Object
{
    private Dictionary<Item, int> itemQuantities;

    public ItemQuantityList() {
        itemQuantities = new Dictionary<Item, int>();
    }

    public List<ItemQuantity> AsList() {
        List<ItemQuantity> result = new List<ItemQuantity>(itemQuantities.Count);
        foreach (var itemQuantity in itemQuantities)
        {
            result.Add(new ItemQuantity(itemQuantity.Key, itemQuantity.Value));
        }

        return result;
    }

    public int Amount(Item item) {
        if (!itemQuantities.ContainsKey(item)) {
            return 0;
        }

        return itemQuantities[item];
    }

    public void Add(Item item, int amount) {
        if (!itemQuantities.ContainsKey(item)) {
            itemQuantities.Add(item, 0);
        }
        itemQuantities[item] += amount;
    }
    public void Remove(Item item, int amount) {
        if (!itemQuantities.ContainsKey(item)) {
            return;
        }

        itemQuantities[item] -= amount;

        if (itemQuantities[item] <= 0) {
            itemQuantities.Remove(item);
        }
    }
}
