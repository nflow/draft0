using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefaultInventory : IInventory
{
    private StorageManager storageManager;
    private Dictionary<Item, InventoryElement> _inventory;

    public List<InventoryElement> GetList()
    {
        return _inventory.Values.ToList();
    }

    public DefaultInventory()
    {
        _inventory = new Dictionary<Item, InventoryElement>();
    }

    public virtual bool Add(Item item, int amount = 1, bool isReal = true, bool isVirutal = true)
    {
        if (!_inventory.ContainsKey(item))
        {
            _inventory.Add(item, new InventoryElement(item, 0, 0));
        }

        if (isReal)
        {
            _inventory[item].realAmount += amount;
        }
        if (isVirutal)
        {
            _inventory[item].virtualAmount += amount;
        }

        return true;
    }

    public virtual bool Remove(Item item, int amount = 1, bool isReal = true, bool isVirutal = true)
    {
        if (!_inventory.ContainsKey(item))
        {
            return false;
        }

        if (isReal && _inventory[item].realAmount - amount < 0)
        {
            return false;
        }

        if (isVirutal && _inventory[item].virtualAmount - amount < 0)
        {
            return false;
        }

        if (isReal)
        {
            _inventory[item].realAmount -= amount;
        }

        if (isVirutal)
        {
            _inventory[item].virtualAmount -= amount;
        }

        return true;
    }

    public virtual bool Accepts(Item item)
    {
        return true;
    }

    public virtual bool Offers(Item item)
    {
        if (!_inventory.ContainsKey(item))
        {
            return false;
        }

        if (_inventory[item].virtualAmount <= 0)
        {
            return false;
        }

        return true;
    }

    public virtual bool Transfer(IInventory to, Item item, int amount = 1, bool isReal = true, bool isVirutal = true)
    {
        if (!this.Remove(item, amount, isReal, isVirutal))
        {
            return false;
        }

        if (!to.Add(item, amount, isReal, isVirutal))
        {
            this.Add(item, amount, isReal, isVirutal);

            return false;
        }

        return true;
    }

    public virtual InventoryElement GetElement(Item item)
    {
        if (!_inventory.ContainsKey(item))
        {
            _inventory.Add(item, new InventoryElement(item, 0, 0));
        }

        return _inventory[item];
    }
}
