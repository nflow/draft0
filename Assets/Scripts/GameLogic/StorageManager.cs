using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    private List<IInteractableInventory> storageList;

    private void Awake()
    {
        storageList = new List<IInteractableInventory>();
    }

    public void Subscribe(IInteractableInventory storage)
    {
        storageList.Add(storage);
    }

    public void Unsubscribe(IInteractableInventory storage)
    {
        storageList.Remove(storage);
    }

    public IInteractableInventory FindItems(Transform origin, Item item, int amount = 1)
    {
        float nearestDistance = float.MaxValue;
        IInteractableInventory nearestStorage = null;
        foreach (var storage in storageList)
        {
            var distance = Vector3.Distance(origin.position, storage.transform.position);
            if (distance < nearestDistance && storage.inventory.Offers(item))
            {
                nearestDistance = distance;
                nearestStorage = storage;
            }
        }

        return nearestStorage;
    }

    public IInteractableInventory FindStorage(Transform origin)
    {
        float nearestDistance = float.MaxValue;
        IInteractableInventory nearestStorage = null;
        foreach (var storage in storageList)
        {
            var distance = Vector3.Distance(origin.position, storage.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestStorage = storage;
            }
        }

        return nearestStorage;
    }

}
