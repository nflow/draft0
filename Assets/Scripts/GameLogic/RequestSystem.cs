using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RequestSystem : MonoBehaviour
{
    class RequestInfo
    {
        public Dictionary<Item, int> requestedItems;
        public List<CarrierTask> activeTaks;

        public RequestInfo()
        {
            requestedItems = new Dictionary<Item, int>();
            activeTaks = new List<CarrierTask>();
        }
    }

    private Dictionary<IInteractableInventory, RequestInfo> requests;
    private CarrierManager carrierManager;
    private StorageManager storageManager;

    public void RequestItems(IInteractableInventory target, Item item, int amount)
    {
        if (amount <= 0)
        {
            Debug.LogError("Illegal amount requested!");

            return;
        }

        if (!requests.ContainsKey(target))
        {
            requests.Add(target, new RequestInfo());
        }

        if (!requests[target].requestedItems.ContainsKey(item))
        {
            requests[target].requestedItems.Add(item, 0);
        }

        requests[target].requestedItems[item] = amount;
    }

    private void Awake()
    {
        carrierManager = GetComponent<CarrierManager>();
        storageManager = GetComponent<StorageManager>();
        requests = new Dictionary<IInteractableInventory, RequestInfo>();
    }

    private void Update()
    {
        // TODO: This will become a performace issue. We need to find a transactions management to avoid item glitches.
        List<IInteractableInventory> finishedRequest = new List<IInteractableInventory>();
        foreach (var requestInfo in requests)
        {
            ScheduleTasks(requestInfo.Key, requestInfo.Value);
            CheckActiveTasks(requestInfo.Value);

            if (requestInfo.Value.activeTaks.Count <= 0 && requestInfo.Value.requestedItems.Count <= 0) {
                finishedRequest.Add(requestInfo.Key);
            }
        }
        foreach(var request in finishedRequest) {
            requests.Remove(request);
        }
    }

    private void ScheduleTasks(IInteractableInventory target, RequestInfo requestInfo)
    {
        List<Item> removeItems = new List<Item>();
        List<Item> keys = new List<Item>(requestInfo.requestedItems.Keys);
        foreach (var key in keys)
        {
            var value = requestInfo.requestedItems[key];

            var nextAvailableStorage = storageManager.FindItems(target.transform, key);
            if (nextAvailableStorage != null) {
                var inStock = nextAvailableStorage.inventory.GetElement(key).virtualAmount;
                var possibleQuantity = inStock >= value ? value : inStock;
                
                if (nextAvailableStorage.inventory.Transfer(target.inventory, key, possibleQuantity, false, true)) {
                    requestInfo.activeTaks.AddRange(carrierManager.BatchTask(nextAvailableStorage, target, key, possibleQuantity));
                    requestInfo.requestedItems[key] -= possibleQuantity;

                    if (requestInfo.requestedItems[key] == 0) {
                        removeItems.Add(key);
                    } else if (requestInfo.requestedItems[key] < 0) {
                        throw new InvalidOperationException("Requested amount must not be negative!");
                    }
                } else {
                    Debug.LogWarning("Failed to schedule task for requested task due to failed transfer");
                }
            }
        }
        foreach(var item in removeItems) {
            requestInfo.requestedItems.Remove(item);
        }
    }

    private void CheckActiveTasks(RequestInfo requestInfo)
    {
        List<CarrierTask> completedTasks = new List<CarrierTask>();
        foreach (var task in requestInfo.activeTaks)
        {            
            if (task.status == TaskStatus.COMPLETE) {
                completedTasks.Add(task);
            }
        }
        foreach(var task in completedTasks) {
            requestInfo.activeTaks.Remove(task);
        }
    }
}
