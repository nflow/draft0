using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : Character, IInteractableInventory
{
    private IInventory _inventory;
    public IInventory inventory
    {
        get => _inventory;
    }

    private Movement movement;
    private CarrierManager carrierManager;
    private CarrierTask currentTask;

    private void Awake() {
        
        carrierManager = GameObject.FindGameObjectWithTag(Tag.GAME_LOGIC).GetComponent<CarrierManager>();
        movement = GetComponent<Movement>();
        
        _inventory = new DefaultInventory();
    }
    
    private void OnEnable() {
        carrierManager.Subscribe(this);
        movement.OnDestinationReached += updateTask;
    }

    void Start()
    {
    }


    private void OnDisable() {
        carrierManager.Unsubscribe(this);
        movement.OnDestinationReached -= updateTask;
    }

    public bool assignTask(CarrierTask task)
    {
        if (OnTask())
            return false;

        currentTask = task;
        movement.destination = currentTask.from.transform.position;

        return true;
    }

    void updateTask(Vector3 destination)
    {
        if (OnTask())
        {
            if (currentTask.status == TaskStatus.TODO && currentTask.from.transform.position == destination)
            {
                currentTask.status = TaskStatus.IN_PROGRESS;
                if (currentTask.from.inventory.Transfer(this.inventory, currentTask.item, 1, true, false))
                {
                    movement.destination = currentTask.to.transform.position;
                }
                else
                {
                    currentTask.status = TaskStatus.COMPLETE;
                    Debug.Log("Could no transfer item!");
                }
            }
            else if (currentTask.status == TaskStatus.IN_PROGRESS && currentTask.to.transform.position == destination)
            {
                if (this.inventory.Transfer(currentTask.to.inventory, currentTask.item, 1, true, false))
                {
                    carrierRating++;
                }
                else
                {
                    Debug.Log("Could no transfer item!");
                }
                currentTask.status = TaskStatus.COMPLETE;
            }
        }
    }

    public bool OnTask()
    {
        return currentTask != null && currentTask.status != TaskStatus.COMPLETE;
    }
}
