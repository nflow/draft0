using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilteredInventory : DefaultInventory
{
    private List<Item> acceptedItems;
    private List<Item> offeredItems;

    public FilteredInventory(List<Item> acceptedItems, List<Item> offeredItems) : base()
    {
        this.acceptedItems = acceptedItems;
        this.offeredItems = offeredItems;
    }

    public override bool Add(Item item, int amount = 1, bool isReal = true, bool isVirtual = true)
    {
        if (!this.Accepts(item))
        {
            return false;
        }

        return base.Add(item, amount, isReal, isVirtual);
    }

    public override bool Accepts(Item item)
    {
        return base.Accepts(item) && acceptedItems.Contains(item);
    }

    public override bool Offers(Item item)
    {
        return base.Offers(item) && offeredItems.Contains(item);
    }

    public override bool Transfer(IInventory to, Item item, int amount = 1, bool isReal = true, bool isVirtual = true)
    {
        if (!offeredItems.Contains(item))
        {
            return false;
        }
        
        if (!to.Accepts(item))
        {
            return false;
        }

        return base.Transfer(to, item, amount, isReal, isVirtual);
    }
}
