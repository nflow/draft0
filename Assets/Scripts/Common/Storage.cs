using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour, IInteractableInventory
{
    public DefaultInventory _inventory;
    private StorageManager storageManager;
    
    public StoragePreset storagePreset;
    
    public IInventory inventory
    {
        get => _inventory;
    }

    private void Awake()
    {
        _inventory = new DefaultInventory();
        var gameLogic = GameObject.FindGameObjectWithTag("GameLogic");
        storageManager = gameLogic.GetComponent<StorageManager>();

        if (storagePreset != null) {
            foreach(var item in storagePreset.items) {
                inventory.Add(item.item, item.amount);
            }
        }
    }

    private void OnEnable()
    {
        storageManager.Subscribe(this);

    }

    private void OnDisable()
    {
        storageManager.Unsubscribe(this);
    }

}
