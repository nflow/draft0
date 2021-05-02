using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Character
{
    private Production _workPlace;
    public Production workPlace
    {
        get => _workPlace;
        set
        {
            _workPlace = value;
            _movement.destination = value.transform.position;
            _movement.OnDestinationReached += StartWork;
        }
    }

    private Movement _movement;
    private WorkerManager _workerManager;

    private void StartWork(Vector3 dest)
    {
        if (_workPlace.transform.position == dest)
        {
            _movement.enabled = false;
            transform.position = _workPlace.transform.position + Vector3.up;
        }
    }

    public bool HasWork()
    {
        return workPlace != null;
    }

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _workerManager = GameObject.FindGameObjectWithTag(Tag.GAME_LOGIC).GetComponent<WorkerManager>();
    }

    private void OnEnable() {
        _workerManager.Subscribe(this);
        
    }
    private void Update()
    {
        if (!_movement.enabled) {
            transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 45.0f);
        }
    }
    private void OnDisable()
    {
        _workerManager.Unsubscribe(this);
    }
}

