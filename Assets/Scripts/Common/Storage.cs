using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour, IInteractableInventory
{
    public DefaultInventory _inventory;
    private StorageManager storageManager;
    
    public IInventory inventory
    {
        get => _inventory;
    }

    private void Awake()
    {
        _inventory = new DefaultInventory();
        var gameLogic = GameObject.FindGameObjectWithTag("GameLogic");
        storageManager = gameLogic.GetComponent<StorageManager>();
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
