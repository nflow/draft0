using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public List<Worker> workForce;

    private void Awake() {
        workForce = new List<Worker>();
    }

    public void Subscribe(Worker worker)
    {
        workForce.Add(worker);
    }

    public void Unsubscribe(Worker worker)
    {
        workForce.Remove(worker);
    }

    public Worker RequestWorker()
    {
        foreach(var worker in workForce) {
            if (!worker.HasWork()) {
                return worker;
            }
        }
        return null;
    }
    
}
