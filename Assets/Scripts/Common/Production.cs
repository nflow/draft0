using System;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour, IInteractableInventory
{
    public Recipe recipe;
    public float nextProduct;
    public int maxInventoryFactor = 1;
    public int maxCarrierJobs = 5;

    private FilteredInventory _inventory;
    public IInventory inventory
    {
        get => _inventory;
    }
    private ProgressBar progressBar;
    private Worker worker;
    private float defaultProductionTime = 5.0f;
    private WorkerManager workerManager;
    private StorageManager storageManager;
    private RequestSystem requestSystem;

    private void Awake()
    {
        var gameLogic = GameObject.FindGameObjectWithTag(Tag.GAME_LOGIC);
        workerManager = gameLogic.GetComponent<WorkerManager>();
        storageManager = gameLogic.GetComponent<StorageManager>();
        requestSystem = gameLogic.GetComponent<RequestSystem>();
    }

    private void OnEnable()
    {
        storageManager.Subscribe(this);
    }

    private void Start() {
        var offers = recipe.producedItems.ConvertAll<Item>(x => x.item);
        var accepts = recipe.requiredItems.ConvertAll<Item>(x => x.item);
        accepts.AddRange(offers);
        _inventory = new FilteredInventory(accepts, offers);

        progressBar = this.GetComponentInChildren<ProgressBar>();
    }

    private void OnDisable()
    {
        storageManager.Unsubscribe(this);
    }

    void Update()
    {

        if (worker == null)
        {
            worker = workerManager.RequestWorker();
            if (worker)
            {
                worker.workPlace = GetComponent<Production>();
                nextProduct = CalculateProductSpeed();
            }
        }
        else
        {
            RequestItems();
            if (HasRequiredItems())
            {
                nextProduct -= Time.deltaTime;
                progressBar.progress = 100 - nextProduct / CalculateProductSpeed() * 100.0f;

                if (nextProduct <= 0)
                {
                    worker.workerRating++;
                    nextProduct = CalculateProductSpeed();

                    foreach (var product in recipe.producedItems)
                    {
                        inventory.Add(product.item, product.amount);
                        ConsumeItems();
                    }
                }
            }

        }
    }

    private void RequestItems()
    {
        foreach (var itemQuantity in recipe.requiredItems)
        {
            var difference = itemQuantity.amount * 2 - inventory.GetElement(itemQuantity.item).virtualAmount;
            if (difference > 0)
            {
                requestSystem.RequestItems(this, itemQuantity.item, difference);
            }
        }
    }

    private bool HasRequiredItems()
    {
        foreach (var requirement in recipe.requiredItems)
        {
            if (inventory.GetElement(requirement.item).realAmount < requirement.amount)
            {
                return false;
            }
        }
        return true;
    }

    private void ConsumeItems()
    {
        foreach (var requirement in recipe.requiredItems)
        {
            if (!inventory.Remove(requirement.item, requirement.amount))
            {
                throw new InvalidOperationException("Consuming item from inventory faild.");
            }
        }
    }

    private float CalculateProductSpeed()
    {
        return defaultProductionTime * (1.0f / Mathf.Log10(worker.workerRating + 1));
    }
}
