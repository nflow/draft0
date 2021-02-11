using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarrierTask
{

    public IInteractableInventory from;
    public IInteractableInventory to;
    public Item item;
    public TaskStatus status;

    public CarrierTask(IInteractableInventory from, IInteractableInventory to, Item item)
    {
        this.from = from;
        this.to = to;
        this.item = item;
        this.status = TaskStatus.TODO;
    }
}

public enum TaskStatus
{
    TODO,
    IN_PROGRESS,
    COMPLETE
}