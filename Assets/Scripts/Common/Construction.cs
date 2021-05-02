using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class Construction : MonoBehaviour, IInteractableInventory
{
    private FilteredInventory _inventory;
    public IInventory inventory
    {
        get => _inventory;
    }

    [Range(0, 100)]
    public float progress;
    public Building toConstruct;

    private float _totalRequiredItems;
    private float _possibleProgress = 0;
    private RequestSystem requestSystem;

    private void Start()
    {
        requestSystem = GameObject.FindGameObjectWithTag(Tag.GAME_LOGIC).GetComponent<RequestSystem>();
        
        var itemFilter = toConstruct.constructionCost.Select(x => x.item).ToList();
        _inventory = new FilteredInventory(itemFilter, Enumerable.Empty<Item>().ToList());
        _totalRequiredItems = toConstruct.constructionCost.Aggregate(0,
            (amount, next) => amount += next.amount);
        foreach (var itemQuantity in toConstruct.constructionCost)
        {
            requestSystem.RequestItems(this, itemQuantity.item, itemQuantity.amount);
        }
    }

    private void Update()
    {
        float availableItems = inventory.GetList().Aggregate(0,
            (amount, next) => amount += next.realAmount);
        _possibleProgress = availableItems / _totalRequiredItems * 100;

        if (progress <= _possibleProgress)
        {
            progress += Time.deltaTime / toConstruct.constructionTime * 100;
            var scale = progress / 111.111f + 0.1f;
            transform.localScale = new Vector3(scale, scale, scale);

            if (progress >= 100)
            {
                var component = gameObject.AddComponent(toConstruct.GetComponentType());
                toConstruct.ConfigureComponent(component);
                Destroy(this);
            }
        }
    }
}
