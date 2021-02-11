using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    List<InventoryElement> GetList();

    bool Add(Item item, int amount = 1, bool isReal = true, bool isVirutal = true);

    bool Remove(Item item, int amount = 1, bool isReal = true, bool isVirutal = true);

    bool Accepts(Item item);
    
    bool Offers(Item item);

    bool Transfer(IInventory to, Item item, int realAmount = 1, bool isReal = true, bool isVirutal = true);

    InventoryElement GetElement(Item item);
    
}