using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Recipe", order = 1)]
public class Recipe : ScriptableObject
{

    public List<ItemQuantity> requiredItems;
    public List<ItemQuantity> producedItems;

    public Recipe() {
        requiredItems = new List<ItemQuantity>();
        producedItems = new List<ItemQuantity>();
    }

    public void RemoveRequirement(Item item) {
        for (int i = 0; i < requiredItems.Count; i++) {
            if (requiredItems[i].item.Equals(item)) {
                requiredItems.RemoveAt(i);
                return;
            }
        }
    }

    public void AddRequirement(Item item, int amount) {
        if (item != null && GetRequirement(item) == null) {
            requiredItems.Add(new ItemQuantity(item, amount));
        }
    }

    public ItemQuantity GetRequirement(Item item) {
        foreach (var component in requiredItems) {
           if (component.item.Equals(item)) {
               return component;
           }
        }
        return null;
    }

    public bool ContainsRequirement(Item item) {
        return GetRequirement(item) == null ? false : true;
    }

    public void RemoveProduct(Item item) {
        for (int i = 0; i < producedItems.Count; i++) {
            if (producedItems[i].item.Equals(item)) {
                producedItems.RemoveAt(i);
                return;
            }
        }
    }

    public void AddProduct(Item item, int amount) {
        if (item != null && GetProduct(item) == null) {
            producedItems.Add(new ItemQuantity(item, amount));
        }
    }

    public ItemQuantity GetProduct(Item item) {
        foreach (var component in producedItems) {
           if (component.item.Equals(item)) {
               return component;
           }
        }
        return null;
    }

    public bool ContainsProduct(Item item) {
        return GetProduct(item) == null ? false : true;
    }

}
