using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierManager : MonoBehaviour
{
    private List<Carrier> _workForce;

    [SerializeField]
    private List<CarrierTask> _scheduledTasks;

    private void Awake() {
        _workForce = new List<Carrier>();
        _scheduledTasks = new List<CarrierTask>();
    }

    public void Subscribe(Carrier carrier)
    {
        _workForce.Add(carrier);
    }

    public void Unsubscribe(Carrier carrier)
    {
        _workForce.Remove(carrier);
    }


    public CarrierTask DispatchTask(IInteractableInventory from, IInteractableInventory to, Item item)
    {
        var task = new CarrierTask(from, to, item);
        _scheduledTasks.Add(task);

        return task;
    }

    public List<CarrierTask> BatchTask(IInteractableInventory from, IInteractableInventory to, Item item, int amount)
    {
        List<CarrierTask> taskList = new List<CarrierTask>();
        for(int i = 0; i < amount; i++) {
            taskList.Add(DispatchTask(from, to, item));
        }

        return taskList;
    }
    
    public CarrierTask RequestTask(Vector3 position) {
        return null;
    }

    public void Update()
    {
        if (_scheduledTasks.Count > 0)
        {
            foreach (var worker in _workForce)
            {
                if (!worker.OnTask())
                {
                    var task = _scheduledTasks[0];
                    _scheduledTasks.RemoveAt(0);
                    worker.assignTask(task);

                    break;
                }
            }
        }
    }


}
